// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace CmdPalRandomRiddleExtension;

public partial class CmdPalRandomRiddleExtensionCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public CmdPalRandomRiddleExtensionCommandsProvider()
    {
        DisplayName = "Random Riddle";
        Icon = new("\uF142"); //❓
        _commands = [
            new CommandItem(new CmdPalRandomRiddleExtensionPage()) { Title = DisplayName },
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

}
