using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdPalCatFunExtension.Pages
{
    internal sealed partial class CmdPalCatFunExtensionPhotosPage : ContentPage
    {
        public CmdPalCatFunExtensionPhotosPage()
        {
            Icon = new("📸");
            Title = "Cat Photos";
            Name = "Open";
        }
        private static string GetRandomCatImageUrl()
        {
            // Use a simple image provider that returns a random cat image.
            // We add a timestamp query to try and avoid aggressive caching so repeated opens look different.
            var ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            return $"https://cataas.com/cat?timestamp={ts}";
        }

        public override IContent[] GetContent()
        {
            var imageUrl = GetRandomCatImageUrl();

            // Build a friendly markdown block with emoji, image, fact, pun and mood meter
            var md = new StringBuilder();
            md.AppendLine("## Cat Photo — " + CmdPalCatFunPhrases.GetRandomPhrase() + "!");
            md.AppendLine("<img src=\"" + imageUrl + "\" width=\"320\" alt=\"Random cat\" />");
            md.AppendLine();

            return [
                new MarkdownContent(md.ToString())
            ];
        }
    }
}
