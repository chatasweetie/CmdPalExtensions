using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdPalCatPetExtension.Pages
{
    internal sealed partial class PlayWithCat : ListPage
    {
        public PlayWithCat()
        {
            Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
            Title = "My sample extension";
            Name = "Open";
        }
        public override IListItem[] GetItems()
        {
            return [
                new ListItem(new NoOpCommand()) { Title = "TODO: Implement your extension here" }
            ];
        }
    }
}
