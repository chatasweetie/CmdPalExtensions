using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using CmdPalCatPetExtension.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdPalCatPetExtension.Pages
{
    internal sealed partial class PlayWithCat : ListPage
    {
        private static readonly string[] WandReactions = new[] { "chases the feather", "does a tiny pounce", "batts it in the air" };
        private static readonly string[] LaserReactions = new[] { "zooms after the dot", "does a crazy sprint", "skitters playfully" };
        private static readonly string[] CatnipReactions = new[] { "rolls blissfully", "kicks their legs happily", "lets out a contented trill" };

        private static string PickWandReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "chases like a tornado", "pounces with flair", "bounces around" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "half-heartedly bats it", "dozes between swats", "gives a slow paw" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "mocks the toy with a stare", "grumbles then pounces", "reluctantly plays" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "inspects every swish", "tilts head and attacks", "studies then chases" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "brings you the toy happily", "purrs mid-play", "nudges your hand" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "does a goofy leap", "chases its own tail", "plays backwards" }[Random.Shared.Next(3)],
                _ => WandReactions[Random.Shared.Next(WandReactions.Length)],
            };
        }

        private static string PickLaserReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "sprints joyfully", "zooms like a rocket", "chirps and chases" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "eyes the dot lazily", "a slow stalk", "eventual pounce" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "snaps once then ignores it", "glares then chases", "grumpy sprint" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "analyzes the dot's path", "strategic pounce", "follows it dutifully" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "chases then returns the toy", "looks at you proudly", "nuzzles after play" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "prances after the dot", "does a dramatic leap", "falls over mid-chase" }[Random.Shared.Next(3)],
                _ => LaserReactions[Random.Shared.Next(LaserReactions.Length)],
            };
        }

        private static string PickCatnipReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "grows extra bouncy", "throws toys in the air", "does a happy spin" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "slow blissful rolls", "a comfy sigh", "soft purrs" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "sneers then relaxes", "softens and yawns", "reluctant delight" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "investigates scent", "sniffs, then flops", "examines then bliss" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "rolls to your feet", "invites a cuddle", "purrs loudly" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "tumbles and somersaults", "silly squeak", "wiggles joyfully" }[Random.Shared.Next(3)],
                _ => CatnipReactions[Random.Shared.Next(CatnipReactions.Length)],
            };
        }

        public PlayWithCat()
        {
            Icon = new("🧶");
            Title = "Play with your Cat";
            Name = "Play";
        }

        public override IListItem[] GetItems()
        {
            return [
                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to play with — adopt one first").Show();
                        return;
                    }

                    var oldHygiene = cat.Hygiene;
                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    var duration = TimeSpan.FromMinutes(5);
                    cat.Play(duration);
                    Services.CatRepository.Save(cat);

                    var reaction = PickWandReaction(cat.Personality);
                    var hygDelta = cat.Hygiene - oldHygiene;
                    var hygText = hygDelta == 0 ? string.Empty : $" (Hygiene {(hygDelta > 0 ? "+" : string.Empty)}{hygDelta})";
                    new ToastStatusMessage($"{cat.Name} {reaction}! (Wand toy — 5m){hygText}").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "🧶  Quick play (5m)" },

                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to play with — adopt one first").Show();
                        return;
                    }

                    var oldHygiene = cat.Hygiene;
                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    var duration = TimeSpan.FromMinutes(15);
                    cat.Play(duration);
                    // Laser play should end with a tangible toy to avoid frustration
                    cat.Happiness = Math.Min(100, cat.Happiness + 10);
                    Services.CatRepository.Save(cat);

                    var reaction = PickLaserReaction(cat.Personality);
                    var hygDelta = cat.Hygiene - oldHygiene;
                    var hygText = hygDelta == 0 ? string.Empty : $" (Hygiene {(hygDelta > 0 ? "+" : string.Empty)}{hygDelta})";
                    new ToastStatusMessage($"{cat.Name} {reaction}! (Laser — 15m). You gave a toy at the end! 🎁 +10 happiness{hygText}").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "🔦  Laser pointer (15m)" },

                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to play with — adopt one first").Show();
                        return;
                    }

                    var oldHygiene = cat.Hygiene;
                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    var duration = TimeSpan.FromMinutes(30);
                    cat.Play(duration);
                    // Catnip gives an extra happiness boost
                    cat.Happiness = Math.Min(100, cat.Happiness + 15);
                    Services.CatRepository.Save(cat);

                    var reaction = PickCatnipReaction(cat.Personality);
                    var hygDelta = cat.Hygiene - oldHygiene;
                    var hygText = hygDelta == 0 ? string.Empty : $" (Hygiene {(hygDelta > 0 ? "+" : string.Empty)}{hygDelta})";
                    new ToastStatusMessage($"{cat.Name} {reaction}! (Catnip — 30m) +15 happiness{hygText}").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "🧸  Catnip mouse (30m)" },

                new ListItem(new AnonymousCommand(() => { }) { Result = CommandResult.GoBack() }) { Title = "❌  Cancel" },
            ];
        }
    }
}
