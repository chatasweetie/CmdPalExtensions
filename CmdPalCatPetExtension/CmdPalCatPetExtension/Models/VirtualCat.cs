using System;

namespace CmdPalCatPetExtension.Models
{
    public sealed class VirtualCat
    {
        public string Name { get; set; }
        public int Energy { get; set; } // 0-100
        public int Hunger { get; set; } // 0-100 (higher = more hungry)
        public int Happiness { get; set; } // 0-100

        // UTC timestamp of last saved state
        public DateTime LastUpdatedUtc { get; set; }

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
            // Assign a random personality on creation
            Personality = (CatPersonality)Random.Shared.Next(Enum.GetValues<CatPersonality>().Length);
        }

        private static int Clamp(int v) => Math.Max(0, Math.Min(100, v));

        public void Sleep(TimeSpan duration)
        {
            var minutes = (int)duration.TotalMinutes;
            Energy = Clamp(Energy + minutes / 2); // regain 0.5 energy per minute
            Hunger = Clamp(Hunger + minutes / 10); // get slightly more hungry
        }

        public void Feed(int amount)
        {
            Energy = Clamp(Energy + amount / 3);
            Hunger = Clamp(Hunger + amount);
            Happiness = Clamp(Happiness + amount / 5);
        }

        public void Play(TimeSpan duration)
        {
            var minutes = (int)duration.TotalMinutes;
            Energy = Clamp(Energy - minutes); // -1 energy per minute
            Hunger = Clamp(Hunger + minutes / 5);
            Happiness = Clamp(Happiness + minutes / 2);
        }

        public void Tick(TimeSpan elapsed)
        {
            var minutes = Math.Max(1, (int)elapsed.TotalMinutes);
            Energy = Clamp(Energy - minutes);
            Hunger = Clamp(Hunger + minutes);
            Happiness = Clamp(Happiness - minutes / 2);
        }
    }
}
