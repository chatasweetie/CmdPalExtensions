using System;
using System.Collections.Generic;

namespace CmdPalCatPetExtension.Models
{
    public sealed class VirtualCat
    {
        public string Name { get; set; }
        public int Energy { get; set; } // 0-100
        public int Hunger { get; set; } // 0-100 (higher = more hungry)
        public int Happiness { get; set; } // 0-100
        public int Hygiene { get; set; } // 0-100 (higher = cleaner)

        // UTC timestamp of last saved state
        public DateTime LastUpdatedUtc { get; set; }

        // UTC timestamp when the cat was created/adopted
        public DateTime CreatedUtc { get; set; }

        // Simple counters to support achievements
        public int PlayCount { get; set; }
        public int FeedCount { get; set; }
        public int SleepCount { get; set; }

        // Persisted list of unlocked achievement ids
        public HashSet<string> Achievements { get; set; }

        // Persisted list of claimed achievement ids
        public HashSet<string> ClaimedAchievements { get; set; }

        // Personality affects responses to interactions
        public CatPersonality Personality { get; set; }

        public enum CatPersonality
        {
            Playful,
            Lazy,
            Grumpy,
            Curious,
            Affectionate,
            Silly
        }

        public VirtualCat(string name, int energy = 80, int hunger = 20, int happiness = 60)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Unnamed" : name;
            Energy = Clamp(energy);
            Hunger = Clamp(hunger);
            Happiness = Clamp(happiness);
            LastUpdatedUtc = DateTime.UtcNow;
            CreatedUtc = DateTime.UtcNow;
            // Assign a random personality on creation
            Personality = (CatPersonality)Random.Shared.Next(Enum.GetValues<CatPersonality>().Length);
            // Assign a random hygiene value on creation (40-100)
            Hygiene = Random.Shared.Next(40, 101);

            // Initialize counters and achievements
            PlayCount = 0;
            FeedCount = 0;
            SleepCount = 0;
            Achievements = new HashSet<string>();
            ClaimedAchievements = new HashSet<string>();
        }

        private static int Clamp(int v) => Math.Max(0, Math.Min(100, v));

        public void Sleep(TimeSpan duration)
        {
            var minutes = (int)duration.TotalMinutes;
            Energy = Clamp(Energy + minutes / 2); // regain 0.5 energy per minute
            Hunger = Clamp(Hunger + minutes / 10); // get slightly more hungry
            SleepCount++;
        }

        public void Feed(int amount)
        {
            Energy = Clamp(Energy + amount / 3);
            Hunger = Clamp(Hunger + amount);
            Happiness = Clamp(Happiness + amount / 5);
            FeedCount++;
        }

        public void Play(TimeSpan duration)
        {
            var minutes = (int)duration.TotalMinutes;
            Energy = Clamp(Energy - minutes); // -1 energy per minute
            Hunger = Clamp(Hunger + minutes / 5);
            Happiness = Clamp(Happiness + minutes / 2);
            // Playing makes the cat a bit messier
            Hygiene = Clamp(Hygiene - minutes / 4);
            PlayCount++;
        }

        public void Tick(TimeSpan elapsed)
        {
            var minutes = Math.Max(1, (int)elapsed.TotalMinutes);
            Energy = Clamp(Energy - minutes);
            Hunger = Clamp(Hunger + minutes);
            Happiness = Clamp(Happiness - minutes / 2);
            // Gradual hygiene decay over time
            Hygiene = Clamp(Hygiene - (minutes / 10));
        }

        /// <summary>
        /// Adds an achievement id to the cat's unlocked set. Returns true when the id was newly added.
        /// </summary>
        public bool AddAchievement(string achievementId)
        {
            if (string.IsNullOrWhiteSpace(achievementId)) return false;
            return Achievements.Add(achievementId);
        }

        /// <summary>
        /// Marks an achievement as claimed/viewed for the cat. Returns true when newly claimed.
        /// </summary>
        public bool AddClaim(string achievementId)
        {
            if (string.IsNullOrWhiteSpace(achievementId)) return false;
            return ClaimedAchievements.Add(achievementId);
        }
    }
}
