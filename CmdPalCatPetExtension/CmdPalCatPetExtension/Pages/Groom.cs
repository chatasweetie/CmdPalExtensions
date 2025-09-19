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
    internal sealed partial class Groom : ListPage
    {
        private static readonly string[] SelfReactions = new[] { "grooms quietly", "licks a paw and looks satisfied", "gives a tiny purr" };
        private static readonly string[] BrushReactions = new[] { "enjoys the brushing", "purrs and relaxes", "leans into the brush" };
        private static readonly string[] BathReactions = new[] { "shakes off water dramatically", "looks refreshed but annoyed", "glows like a clean floof" };

        private static string PickSelfReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "grooms and then chases a loose fur", "grooms between play bursts", "purrs while preening" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "grooms slowly and dozes", "half-heartedly licks a paw", "snuggles after grooming" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "grooms with a stare", "grumbles while cleaning", "girds their whiskers grumpily" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "inspects a fluff then grooms", "tilts head while grooming", "examines a paw carefully" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "grooms then nuzzles you", "purrs while grooming near you", "leans in for a cuddle after" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "grooms with flair", "folds into silly positions while grooming", "makes a funny face mid-groom" }[Random.Shared.Next(3)],
                _ => SelfReactions[Random.Shared.Next(SelfReactions.Length)],
            };
        }

        private static string PickBrushReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "chases the brush at first then settles", "jumps around before purring", "bites playfully while being brushed" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "sinks into the brush and relaxes", "purrs softly while being brushed", "stretches and enjoys it" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "grumbles but keeps still", "tolerates the brush with a scowl", "gives a single slow purr" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "watches the bristles curiously", "inspects the brush between strokes", "seems fascinated by the motion" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "leans into the brush and purrs", "gives a thankful headbutt", "brings you a thankful nuzzle" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "rolls over dramatically", "makes silly noises while being brushed", "tries to play with the brush" }[Random.Shared.Next(3)],
                _ => BrushReactions[Random.Shared.Next(BrushReactions.Length)],
            };
        }

        private static string PickBathReaction(VirtualCat.CatPersonality p)
        {
            return p switch
            {
                VirtualCat.CatPersonality.Playful => new[] { "splashes around then zooms", "tries to play in the water", "emerges dripping and energetic" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Lazy => new[] { "tolerates it and naps afterward", "slowly accepts the bath", "is placidly cleaned" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Grumpy => new[] { "huffs and glares the whole time", "shakes off loudly when finished", "plots revenge while drying" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Curious => new[] { "investigates the water carefully", "watches droplets with fascination", "studies the tub after finishing" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Affectionate => new[] { "cuddles close after drying", "accepts the bath for your sake", "purrs vulnerably after" }[Random.Shared.Next(3)],
                VirtualCat.CatPersonality.Silly => new[] { "messes up and makes silly faces", "shakes and flings water everywhere", "does a dramatic soggy dance" }[Random.Shared.Next(3)],
                _ => BathReactions[Random.Shared.Next(BathReactions.Length)],
            };
        }

        public Groom()
        {
            Icon = new("🧼");
            Title = "Groom your Cat";
            Name = "Groom";
        }

        public override IListItem[] GetItems()
        {
            return [
                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to groom — adopt one first").Show();
                        return;
                    }

                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    // Self groom: small happiness bump, small energy cost and tiny hunger increase
                    cat.Happiness = Math.Min(100, cat.Happiness + 5);
                    cat.Hygiene = Math.Min(100, cat.Hygiene + 8);
                    cat.Energy = Math.Max(0, cat.Energy - 2);
                    cat.Hunger = Math.Min(100, cat.Hunger + 1);
                    Services.CatRepository.Save(cat);

                    var reaction = PickSelfReaction(cat.Personality);
                    new ToastStatusMessage($"{cat.Name} {reaction}! (+5)").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "🪶  Self groom (+5)" },

                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to groom — adopt one first").Show();
                        return;
                    }

                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    // Brush fur: moderate happiness, moderate energy cost and slight hunger increase
                    cat.Happiness = Math.Min(100, cat.Happiness + 12);
                    cat.Hygiene = Math.Min(100, cat.Hygiene + 20);
                    cat.Energy = Math.Max(0, cat.Energy - 5);
                    cat.Hunger = Math.Min(100, cat.Hunger + 3);
                    Services.CatRepository.Save(cat);

                    var reaction = PickBrushReaction(cat.Personality);
                    new ToastStatusMessage($"{cat.Name} {reaction}! (+12)").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "🪮  Brush fur (+12)" },

                new ListItem(new AnonymousCommand(() =>
                {
                    var cat = Services.CatRepository.Load();
                    if (cat is null)
                    {
                        new ToastStatusMessage("No cat to groom — adopt one first").Show();
                        return;
                    }

                    var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
                    cat.Tick(elapsed);
                    // Full bath: larger happiness bump, larger energy cost and noticeable hunger increase
                    cat.Happiness = Math.Min(100, cat.Happiness + 18);
                    cat.Hygiene = Math.Min(100, cat.Hygiene + 45);
                    cat.Energy = Math.Max(0, cat.Energy - 12);
                    cat.Hunger = Math.Min(100, cat.Hunger + 8);
                    Services.CatRepository.Save(cat);

                    var reaction = PickBathReaction(cat.Personality);
                    new ToastStatusMessage($"{cat.Name} {reaction}! (+18)").Show();
                }) { Result = CommandResult.GoBack() }) { Title = "🛁  Full bath (+18)" },

                new ListItem(new AnonymousCommand(() => { /* noop */ }) { Result = CommandResult.GoBack() }) { Title = "❌  Cancel" },
            ];
        }
    }
}
