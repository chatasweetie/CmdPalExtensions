using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdPalCatPetExtension.Pages
{
    internal sealed partial class GiveUpForAdoptionPage : ListPage
    {
        public GiveUpForAdoptionPage()
        {
            Icon = new("🏠");
            Title = "Give up for adoption";
            Name = "Adoption";
        }

        public override IListItem[] GetItems()
        {
            return [
                new ListItem(new AnonymousCommand(() =>
                {
                    // No immediate action; the Result will trigger a confirmation dialog
                })
                {
                    Result = CommandResult.Confirm(new ConfirmationArgs
                    {
                        PrimaryCommand = new AnonymousCommand(() =>
                        {
                            var cat = Services.CatRepository.Load();
                            if (cat is null)
                            {
                                new ToastStatusMessage("No cat to give up — adopt one first").Show();
                                return;
                            }

                            var deleted = Services.CatRepository.Delete();
                            if (deleted)
                            {
                                new ToastStatusMessage($"{cat.Name} has been put up for adoption.").Show();
                            }
                            else
                            {
                                new ToastStatusMessage($"Failed to give {cat.Name} up for adoption.").Show();
                            }
                        }) { Name = "Confirm", Result = CommandResult.GoBack() },
                        Title = "Give up your cat?",
                        Description = "This will delete your saved cat and return the menu to Create a Cat. Are you sure?",
                    })
                }) { Title = "🏠  Give up for adoption" },

                new ListItem(new AnonymousCommand(() => { /* noop */ }) { Result = CommandResult.GoBack() }) { Title = "❌  Cancel" },
            ];
        }
    }
}
