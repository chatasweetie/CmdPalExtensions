using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CmdPalCatPetExtension.Models;
using CmdPalCatPetExtension.Services;

namespace CmdPalCatPetExtension.Pages
{
    internal sealed partial class AchievementsPage : ListPage
    {
        public AchievementsPage()
        {
            Icon = new("🏆");
            Title = "Achievements";
            Name = "Achievements";
        }

        public override IListItem[] GetItems()
        {
            var cat = Services.CatRepository.Load();
            var defs = AchievementsService.GetAll();
            var items = new List<IListItem>();

            if (cat is null)
            {
                items.Add(new ListItem(new AnonymousCommand(() => { })) { Title = "No cat — adopt one to earn achievements" });
                items.Add(new ListItem(new AnonymousCommand(() => { })) { Title = "❌  Back" });
                return items.ToArray();
            }

            // Ensure achievements are evaluated when the page opens (catches missed/unprocessed unlocks)
            try
            {
                var newlyUnlocked = AchievementsService.EvaluateAll(cat);
                foreach (var a in newlyUnlocked)
                {
                    new ToastStatusMessage($"🏆 New achievement unlocked! {a.Icon} {a.Title}").Show();
                }
            }
            catch { }

            foreach (var d in defs)
            {
                var unlocked = cat.Achievements != null && cat.Achievements.Contains(d.Id);
                var claimed = cat.ClaimedAchievements != null && cat.ClaimedAchievements.Contains(d.Id);

                if (!unlocked)
                {
                    var cmd = new AnonymousCommand(() =>
                    {
                        // Locked: show a hint / tease
                        var hint = d.Description ?? "Locked achievement.";
                        new ToastStatusMessage($"🔒 Locked — {hint}").Show();
                    }) { Result = CommandResult.KeepOpen() };

                    items.Add(new ListItem(cmd) { Title = $"🔒 {d.Icon}", Subtitle = "Locked — press to get a hint 🔍" });
                }
                else if (unlocked && !claimed)
                {
                    ListItem? listItem = null;
                    var cmd = new AnonymousCommand(() =>
                    {
                        // Claim the achievement: persist claim and celebrate
                        if (cat.ClaimedAchievements == null) cat.ClaimedAchievements = new HashSet<string>();
                        if (cat.AddClaim(d.Id))
                        {
                            Services.CatRepository.Save(cat);
                            // Update the list item title/subtitle in-place so the user sees the name without navigating back
                            if (listItem != null)
                            {
                                listItem.Title = $"🏅 {d.Icon} {d.Title}";
                                listItem.Subtitle = $"Claimed — {d.Description} 🎉";
                            }
                            new ToastStatusMessage($"🔓 Revealed! {d.Icon} {d.Title} — {d.Description}").Show();
                        }
                        else
                        {
                            new ToastStatusMessage($"🔓 Already revealed: {d.Title}").Show();
                        }
                    }) { Result = CommandResult.KeepOpen() };

                    listItem = new ListItem(cmd) { Title = $"🔓 {d.Icon} (Press to reveal!)", Subtitle = "Unlocked — press to reveal the name ✨" };

                    items.Add(listItem);
                }
                else // claimed
                {
                    var cmd = new AnonymousCommand(() =>
                    {
                        new ToastStatusMessage($"🏅 {d.Icon} {d.Title} — {d.Description}").Show();
                    }) { Result = CommandResult.KeepOpen() };

                    items.Add(new ListItem(cmd) { Title = $"🏅 {d.Icon} {d.Title}", Subtitle = $"Claimed — {d.Description} 🎉" });
                }
            }

            items.Add(new ListItem(new AnonymousCommand(() => { })) { Title = "❌  Back" });
            return items.ToArray();
        }
    }
}
