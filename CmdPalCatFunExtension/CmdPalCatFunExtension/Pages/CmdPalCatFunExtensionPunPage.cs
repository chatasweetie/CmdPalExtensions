using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdPalCatFunExtension.Pages
{
    internal sealed partial class CmdPalCatFunExtensionPunPage : ContentPage
    {
        private static readonly Random _rand = new();
        private static readonly string[] _puns = new[]
        {
            "Pawsitive vibes only!",
            "I'm feline good!",
            "Meowtastic!",
            "Purrfection achieved!",
            "Stay paw-sitive!",
            "You're the cat's pajamas!",
            "You've got to be kitten me right meow!",
            "Claw-some choice!",
            "Time to take a cat nap 😴",
        };
        public CmdPalCatFunExtensionPunPage()
        {
            Icon = new("😺");
            Title = "Cat Pun";
            Name = "Open";
        }
        public static string GetRandomPun()
        {
            int index = _rand.Next(_puns.Length);
            return _puns[index];
        }

        public override IContent[] GetContent()
        {
            // Gather lively bits we want to show in the palette
            var pun = GetRandomPun();

            // Build a friendly markdown block with emoji, image, fact, pun and mood meter
            var md = new StringBuilder();
            md.AppendLine("## 😺 Cat Puns — " + CmdPalCatFunPhrases.GetRandomPhrase() + "!");
            md.AppendLine();
            md.AppendLine("**Pun:** " + pun);
            md.AppendLine();

            return [
                new MarkdownContent(md.ToString())
            ];
        }
    }
}
