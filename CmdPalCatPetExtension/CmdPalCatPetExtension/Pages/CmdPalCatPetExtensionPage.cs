// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using CmdPalCatPetExtension.Pages;
using CmdPalCatPetExtension.Services;

namespace CmdPalCatPetExtension;

internal sealed partial class CmdPalCatPetExtensionPage : ListPage
{
    public CmdPalCatPetExtensionPage()
    {
        Icon = new("🐈");
        Title = "Your Cat's Home";
        Name = "Your Cat's Home";

        // Refresh this page's items when cat data changes
        CatRepository.CatChanged += (change) =>
        {
            if (change == CatChangeType.Created || change == CatChangeType.Deleted)
            {
                RaiseItemsChanged();
            }
        };
    }

    public override IListItem[] GetItems()
    {
        if (CatRepository.Load() is not null)
        {
            return [
                new ListItem(new CatStatusPage()) { Title = "😺  View Cat Status" },
                new ListItem(new FeedCat()) { Title = "🍽️  Feed your Cat" },
                new ListItem(new PlayWithCat()) { Title = "🧶  Play with your Cat" },
                new ListItem(new PutCatToBed()) { Title = "🛏️  Put your Cat to Bed" },
                new ListItem(new Groom()) { Title = "🧼  Groom your Cat" },
                new ListItem(new GiveUpForAdoptionPage()) { Title = "🏠  Give up your cat for adoption" },
            ];
        }
        return [
            new ListItem(new CreateCatPage()) { Title = "🐣  Create a Cat" },
        ];
    }
}
