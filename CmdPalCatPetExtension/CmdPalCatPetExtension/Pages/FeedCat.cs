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
    internal sealed partial class FeedCat : ListPage
    {
        public FeedCat()
        {
            Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
            Title = "Feed your Cat";
            Name = "Feed";
        }

        private static readonly string[] SmallReactions = new[] { "purrs happily", "gives you a tiny headbutt", "licks their whiskers" };
        private static readonly string[] MealReactions = new[] { "rolls over for more", "purrs louder", "rubs against your leg" };
        private static readonly string[] FeastReactions = new[] { "does a happy flip", "purrs like a motor", "curls up blissfully" };

        private static string PickSmallReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "munches excitedly", "pounces on the bowl", "chases a stray crumb" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "nibbles slowly", "dozes mid-bite", "stares at you while chewing" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "grumpily eats", "sniffs then eats", "huffs while taking a bite" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "inspects then eats", "tilts head and tries it", "pokes the food curiously" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "purrs and nuzzles the bowl", "gives a grateful headbutt", "kneads before eating" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "tosses food around playfully", "makes a silly face", "eats upside down" }[Random.Shared.Next(3)],
                _ => SmallReactions[Random.Shared.Next(SmallReactions.Length)],
            };
        }

        private static string PickMealReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "buries a toy in the bowl", "purrs and flips the bowl", "does a happy spin" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "sighs contentedly", "slowly laps it up", "stretches and eats" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "grunts approvingly", "eats with a huff", "stomps away afterwards" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "samples everything first", "sniffs then devours", "finds the crunchy bits" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "purrs loudly","rubs your hand","gives a thankful meow" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "chases the food across the floor","does a little dance","tries to share with a toy" }[Random.Shared.Next(3)],
                _ => MealReactions[Random.Shared.Next(MealReactions.Length)],
            };
        }

        private static string PickFeastReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "does a joyous somersault", "purrs like a motor", "plays with leftovers" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "concise contented snooze", "stretches and flops", "dreams peacefully" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "glares then melts into purrs", "takes your spot on the couch", "sniffs then cuddles" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "dissects every bite", "discovers a surprise toy", "inspects with delight" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "rubs your face gratefully", "cuddles up close", "buries their face in your lap" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "somersaults dramatically", "makes goofy noises", "plays with a napkin like a champion" }[Random.Shared.Next(3)],
                _ => FeastReactions[Random.Shared.Next(FeastReactions.Length)],
            };
        }

        public override IListItem[] GetItems()
        {
            return [
                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to feed — adopt one first").Show();
                        return;
                    }
                    var oldHygiene = cat.Hygiene;
                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    cat.Feed(10);
                    // Eating can be messy
                    cat.Hygiene = Math.Max(0, cat.Hygiene - 3);
                    Services.CatRepository.Save(cat);

                    var reaction = PickSmallReaction(cat.Personality);
                    var hygDelta = cat.Hygiene - oldHygiene;
                    var hygText = hygDelta == 0 ? string.Empty : $" (Hygiene {(hygDelta > 0 ? "+" : string.Empty)}{hygDelta})";
                    new ToastStatusMessage($"{cat.Name} {reaction}! (+10){hygText}").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "🍪  Small snack (+10)" },

                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to feed — adopt one first").Show();
                        return;
                    }
                    var oldHygiene = cat.Hygiene;
                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    cat.Feed(25);
                    // Eating can be messy
                    cat.Hygiene = Math.Max(0, cat.Hygiene - 6);
                    Services.CatRepository.Save(cat);

                    var reaction = PickMealReaction(cat.Personality);
                    var hygDelta = cat.Hygiene - oldHygiene;
                    var hygText = hygDelta == 0 ? string.Empty : $" (Hygiene {(hygDelta > 0 ? "+" : string.Empty)}{hygDelta})";
                    new ToastStatusMessage($"{cat.Name} {reaction}! (+25){hygText}").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "🍽️  Meal (+25)" },

                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to feed — adopt one first").Show();
                        return;
                    }
                    var oldHygiene = cat.Hygiene;
                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    cat.Feed(50);
                    // Eating can be messy
                    cat.Hygiene = Math.Max(0, cat.Hygiene - 12);
                    Services.CatRepository.Save(cat);

                    // Small chance to find a toy when feasting
                    if (Random.Shared.NextDouble() < 0.12)
                    {
                        cat.Happiness = Math.Min(100, cat.Happiness + 10);
                        Services.CatRepository.Save(cat);
                        new ToastStatusMessage($"{cat.Name} found a toy in the leftovers! 🎁 +10 happiness").Show();
                    }
                    var reaction = PickFeastReaction(cat.Personality);
                    var hygDelta = cat.Hygiene - oldHygiene;
                    var hygText = hygDelta == 0 ? string.Empty : $" (Hygiene {(hygDelta > 0 ? "+" : string.Empty)}{hygDelta})";
                    new ToastStatusMessage($"{cat.Name} {reaction}! (+50){hygText}").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "🍖  Feast (+50)" },

                new ListItem(new AnonymousCommand(() => { /* noop */ }) { Result = CommandResult.GoBack() }) { Title = "❌  Cancel" },
            ];
        }
    }
}
