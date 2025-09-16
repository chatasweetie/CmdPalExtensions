// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CmdPalCatFunExtension.Pages;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CmdPalCatFunExtension;

internal sealed partial class CmdPalCatFunExtensionPage : ListPage
{

    public CmdPalCatFunExtensionPage()
    {
        Icon = new("😺"); ;
        Title = "You fun cat options";
        Name = "Open";
    }
    public override IListItem[] GetItems()
    {
        return [
            new ListItem(new CmdPalCatFunExtensionPunPage()) { Title = "Cat Puns" },
            new ListItem(new CmdPalCatFunExtensionFunFactsPage()) { Title = "Fun Facts" },
            new ListItem(new CmdPalCatFunExtensionPhotosPage()) { Title = "Cat Photos" }
        ];
    }
}
