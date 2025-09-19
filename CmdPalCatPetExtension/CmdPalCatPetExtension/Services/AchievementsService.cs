using System;
using System.Collections.Generic;
using System.Linq;
using CmdPalCatPetExtension.Models;

namespace CmdPalCatPetExtension.Services
{
    public sealed class AchievementDef
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Icon { get; init; }

        public AchievementDef(string id, string title, string description, string icon = "🏆")
        {
            Id = id;
            Title = title;
            Description = description;
            Icon = icon;
        }
    }

    public static class AchievementsService
    {
        // Public event raised when an achievement is newly unlocked for a cat
        public static event Action<VirtualCat, AchievementDef>? AchievementUnlocked;

        static AchievementsService()
        {
            // When the repository notifies of changes, evaluate achievements for the current cat
            CatRepository.CatChanged += (change) =>
            {
                try
                {
                    var cat = CatRepository.Load();
                    if (cat != null)
                    {
                        // EvaluateAll will call TryUnlock which persists and raises AchievementUnlocked
                        EvaluateAll(cat);
                    }
                }
                catch
                {
                    // swallow exceptions to avoid breaking repo notifications
                }
            };
        }

        private static readonly List<AchievementDef> _definitions = new()
        {
            new AchievementDef("adopted", "Adopted", "Welcome to your new friend!", "🎉"),
            new AchievementDef("first_feed", "First Meal", "You fed your cat for the first time.", "🍪"),
            new AchievementDef("first_play", "First Play", "You played with your cat for the first time.", "🧶"),
            new AchievementDef("first_sleep", "First Nap", "Your cat took their first nap.", "😴"),
            new AchievementDef("happy_100", "Pure Joy", "Your cat reached 100 happiness!", "❤️"),
            new AchievementDef("energy_100", "Energizer", "Your cat reached 100 energy.", "⚡"),
            new AchievementDef("hygiene_100", "Spa Day", "Your cat is perfectly clean.", "🛁"),
            new AchievementDef("7_days_owned", "Week Together", "You've had your cat for 7 days.", "📅"),
            new AchievementDef("30_days_owned", "One Month", "30 days together — time flies!", "🏅"),
            new AchievementDef("plays_50", "Playtime Legend", "You've played 50 times with your cat.", "🎯"),
            new AchievementDef("feeds_100", "Never Hungry", "You've fed your cat 100 times.", "🍖"),
            new AchievementDef("happiness_80", "Friendly", "Your cat's happiness reached 80.", "😊"),
            new AchievementDef("sleeps_10", "Restful", "Your cat has napped 10 times.", "💤"),
            new AchievementDef("named_custom", "Named", "You gave your cat a custom name.", "✍️"),
            new AchievementDef("playful_personality", "Playmate", "You own a playful cat.", "🐾"),
            new AchievementDef("grumpy_personality", "Grumpy Club", "You own a grumpy cat.", "😾"),
            new AchievementDef("curious_personality", "Curious Mind", "You own a curious cat.", "🔍"),
            new AchievementDef("affectionate_personality", "Lovable", "You own an affectionate cat.", "❤️"),
            new AchievementDef("silly_personality", "Silly Goose", "You own a silly cat.", "🤪"),
            new AchievementDef("100_days_owned", "Centennial Friend", "100 days together — what a bond!", "🎉")
        };

        public static IReadOnlyList<AchievementDef> GetAll() => _definitions;

        public static AchievementDef? GetById(string id) => _definitions.FirstOrDefault(a => a.Id == id);

        /// <summary>
        /// Attempt to unlock a specific achievement for a cat. Returns the AchievementDef when newly unlocked, otherwise null.
        /// </summary>
        public static AchievementDef? TryUnlock(VirtualCat cat, string achievementId)
        {
            if (cat is null) return null;
            var def = GetById(achievementId);
            if (def == null) return null;

            var added = cat.AddAchievement(achievementId);
            if (added)
            {
                // Persist change
                CatRepository.Save(cat);
                AchievementUnlocked?.Invoke(cat, def);
                return def;
            }

            return null;
        }

        /// <summary>
        /// Evaluate all achievement conditions for a cat and unlock those that match.
        /// </summary>
        public static List<AchievementDef> EvaluateAll(VirtualCat cat)
        {
            var unlocked = new List<AchievementDef>();
            if (cat is null) return unlocked;

            // adopt (cat exists)
            if (!cat.Achievements.Contains("adopted"))
            {
                if (TryUnlock(cat, "adopted") is AchievementDef a) unlocked.Add(a);
            }

            // name
            if (!cat.Achievements.Contains("named_custom") && !string.IsNullOrWhiteSpace(cat.Name) && cat.Name != "Unnamed")
            {
                if (TryUnlock(cat, "named_custom") is AchievementDef a) unlocked.Add(a);
            }

            // basic counters
            if (cat.FeedCount >= 1 && !cat.Achievements.Contains("first_feed")) if (TryUnlock(cat, "first_feed") is AchievementDef a1) unlocked.Add(a1);
            if (cat.PlayCount >= 1 && !cat.Achievements.Contains("first_play")) if (TryUnlock(cat, "first_play") is AchievementDef a2) unlocked.Add(a2);
            if (cat.SleepCount >= 1 && !cat.Achievements.Contains("first_sleep")) if (TryUnlock(cat, "first_sleep") is AchievementDef a3) unlocked.Add(a3);

            if (cat.PlayCount >= 50 && !cat.Achievements.Contains("plays_50")) if (TryUnlock(cat, "plays_50") is AchievementDef a4) unlocked.Add(a4);
            if (cat.FeedCount >= 100 && !cat.Achievements.Contains("feeds_100")) if (TryUnlock(cat, "feeds_100") is AchievementDef a5) unlocked.Add(a5);
            if (cat.SleepCount >= 10 && !cat.Achievements.Contains("sleeps_10")) if (TryUnlock(cat, "sleeps_10") is AchievementDef a6) unlocked.Add(a6);

            // thresholds
            if (cat.Happiness >= 100 && !cat.Achievements.Contains("happy_100")) if (TryUnlock(cat, "happy_100") is AchievementDef a7) unlocked.Add(a7);
            if (cat.Energy >= 100 && !cat.Achievements.Contains("energy_100")) if (TryUnlock(cat, "energy_100") is AchievementDef a8) unlocked.Add(a8);
            if (cat.Hygiene >= 100 && !cat.Achievements.Contains("hygiene_100")) if (TryUnlock(cat, "hygiene_100") is AchievementDef a9) unlocked.Add(a9);
            if (cat.Happiness >= 80 && !cat.Achievements.Contains("happiness_80")) if (TryUnlock(cat, "happiness_80") is AchievementDef a10) unlocked.Add(a10);

            // personality
            if (cat.Personality == VirtualCat.CatPersonality.Playful && !cat.Achievements.Contains("playful_personality")) if (TryUnlock(cat, "playful_personality") is AchievementDef a11) unlocked.Add(a11);
            if (cat.Personality == VirtualCat.CatPersonality.Grumpy && !cat.Achievements.Contains("grumpy_personality")) if (TryUnlock(cat, "grumpy_personality") is AchievementDef a12) unlocked.Add(a12);
            if (cat.Personality == VirtualCat.CatPersonality.Curious && !cat.Achievements.Contains("curious_personality")) if (TryUnlock(cat, "curious_personality") is AchievementDef a13) unlocked.Add(a13);
            if (cat.Personality == VirtualCat.CatPersonality.Affectionate && !cat.Achievements.Contains("affectionate_personality")) if (TryUnlock(cat, "affectionate_personality") is AchievementDef a14) unlocked.Add(a14);
            if (cat.Personality == VirtualCat.CatPersonality.Silly && !cat.Achievements.Contains("silly_personality")) if (TryUnlock(cat, "silly_personality") is AchievementDef a15) unlocked.Add(a15);

            // time-based
            var ownedDays = (DateTime.UtcNow - cat.CreatedUtc).TotalDays;
            if (ownedDays >= 7 && !cat.Achievements.Contains("7_days_owned")) if (TryUnlock(cat, "7_days_owned") is AchievementDef a16) unlocked.Add(a16);
            if (ownedDays >= 30 && !cat.Achievements.Contains("30_days_owned")) if (TryUnlock(cat, "30_days_owned") is AchievementDef a17) unlocked.Add(a17);
            if (ownedDays >= 100 && !cat.Achievements.Contains("100_days_owned")) if (TryUnlock(cat, "100_days_owned") is AchievementDef a18) unlocked.Add(a18);

            return unlocked;
        }

        public static IReadOnlyList<AchievementDef> GetUnlockedDefinitions(VirtualCat cat)
        {
            if (cat is null) return Array.Empty<AchievementDef>();
            return _definitions.Where(d => cat.Achievements.Contains(d.Id)).ToList();
        }
    }
}
