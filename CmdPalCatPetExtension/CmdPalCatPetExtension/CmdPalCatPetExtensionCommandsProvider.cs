// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using CmdPalCatPetExtension.Services;

namespace CmdPalCatPetExtension;

public partial class CmdPalCatPetExtensionCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public CmdPalCatPetExtensionCommandsProvider()
    {
        DisplayName = "CmdPalCatFunExtension";
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        _commands = [
            new CommandItem(new CmdPalCatPetExtensionPage()) { Title = DisplayName },
        ];

        // Refresh top-level commands when cat data is created or deleted
        CatRepository.CatChanged += (change) =>
        {
            if (change == CatChangeType.Created || change == CatChangeType.Deleted)
            {
                RaiseItemsChanged();
            }
        };
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

}
