using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmdPalCatPetExtension.Models;

namespace CmdPalCatPetExtension.Pages
{
    internal sealed partial class PutCatToBed : ListPage
    {
        private static readonly string[] SofaReactions = new[] { "dozes on the couch", "kneads the blanket", "purrs softly" };
        private static readonly string[] SunReactions = new[] { "stretches luxuriously", "basks in the warmth", "floats into dreamland" };
        private static readonly string[] BedReactions = new[] { "curls up like a burrito", "sleeps like a kitten", "snores in tiny bursts" };

        public PutCatToBed()
        {
            Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
            Title = "Put your Cat to Bed";
            Name = "Sleep";
        }

        public override IListItem[] GetItems()
        {
            return [
                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to put to bed — adopt one first").Show();
                        return;
                    }

                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    var duration = TimeSpan.FromMinutes(20);
                    cat.Sleep(duration);
                    cat.Happiness = Math.Min(100, cat.Happiness + 5);
                    Services.CatRepository.Save(cat);

                    var reaction = PickSofaReaction(cat.Personality);
                    new ToastStatusMessage($"{cat.Name} {reaction}. (Sofa nap — 20m) +5 happiness").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "🛋️  Sofa nap (20m)" },

                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to put to bed — adopt one first").Show();
                        return;
                    }

                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    var duration = TimeSpan.FromMinutes(60);
                    cat.Sleep(duration);
                    cat.Happiness = Math.Min(100, cat.Happiness + 8);
                    Services.CatRepository.Save(cat);

                    var reaction = PickSunReaction(cat.Personality);
                    new ToastStatusMessage($"{cat.Name} {reaction}. (Sunbeam snooze — 60m) +8 happiness").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "☀️  Sunbeam snooze (60m)" },

                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to put to bed — adopt one first").Show();
                        return;
                    }

                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    var duration = TimeSpan.FromMinutes(120);
                    cat.Sleep(duration);
                    cat.Happiness = Math.Min(100, cat.Happiness + 15);
                    Services.CatRepository.Save(cat);

                    // Small chance for a dreamy special reaction
                    if (Random.Shared.NextDouble() < 0.12)
                    {
                        new ToastStatusMessage($"{cat.Name} is dreaming of catnip fields... 😺").Show();
                    }

                    var reaction = PickBedReaction(cat.Personality);
                    new ToastStatusMessage($"{cat.Name} {reaction}. (Cuddle bed — 120m) +15 happiness").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "🛏️  Cuddle bed (120m)" },

                new ListItem(new AnonymousCommand(() => { }) { Result = CommandResult.GoBack() }) { Title = "❌  Cancel" },
            ];
        }

        private static string PickSofaReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "dozes briefly between play sessions", "kneads the blanket then chases a dust mote", "purrs and dreams of zoomies" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "melts into the cushion", "snoozes without moving", "a deep, contented purr" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "grumbles then naps", "claims the comfiest spot", "glares, then snoozes" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "keeps one eye open and studies the room", "tenses then relaxes into sleep", "dreams of exploring" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "cuddles your sweater", "nuzzles you before sleeping", "stays close and purrs" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "flops dramatically", "sleeps with a paw in the air", "snores in a funny rhythm" }[Random.Shared.Next(3)],
                _ => SofaReactions[Random.Shared.Next(SofaReactions.Length)],
            };
        }

        private static string PickSunReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "dozes then springs up to swat a sunbeam", "chases a dust mote in the light", "rolls blissfully" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "folds into a sunny loaf", "stretches slowly and sleeps", "a long lazy purr" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "stares at the sun, unimpressed", "reluctantly naps and grumbles", "guards the sunny spot" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "watches the light patterns", "chases sunbeams like a hunter", "dreams of warm windowsills" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "purrs in the sun and leans on you", "rolls close to your feet", "gives tiny kisses in sleep" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "snoozes in a silly pose", "spreads out like a starfish", "makes soft playful noises in sleep" }[Random.Shared.Next(3)],
                _ => SunReactions[Random.Shared.Next(SunReactions.Length)],
            };
        }

        private static string PickBedReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "dreams of chasing friends", "paws in their sleep", "twitches with happy dreams" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "sleeps deeply and peacefully", "soft snores fill the room", "stretches luxuriously in dreamland" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "grumbles in their sleep then sighs contentedly", "takes over your pillow", "rolls and mutters" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "chases dream-butterflies", "paws the blankets in sleep", "explores in dreams" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "snuggles up next to you", "purrs warmly into your arm", "nuzzles in sleep" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "sleep-dances", "snores in a rhythm", "makes funny sleep faces" }[Random.Shared.Next(3)],
                _ => BedReactions[Random.Shared.Next(BedReactions.Length)],
            };
        }
    }
}
