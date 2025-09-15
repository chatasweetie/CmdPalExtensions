// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace CmdPalTransalationExtension;

public partial class CmdPalTransalationExtensionCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public CmdPalTransalationExtensionCommandsProvider()
    {
        DisplayName = "Text Translator";
        Icon = new("\uE8C1"); // Globe icon
        _commands = [
            new CommandItem(new CmdPalTransalationExtensionPage()) 
            { 
                Title = DisplayName,
                Subtitle = "Translate text between languages"
            },
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }
}
