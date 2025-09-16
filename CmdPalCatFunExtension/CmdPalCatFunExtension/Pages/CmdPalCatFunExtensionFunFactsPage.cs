using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CmdPalCatFunExtension.Pages
{
    internal sealed partial class CmdPalCatFunExtensionFunFactsPage : ContentPage
    {
        private static readonly Random _rand = new();
        public CmdPalCatFunExtensionFunFactsPage()
        {
            Icon = Icon = new("🐾");
            Title = "Cat Fun Fats";
            Name = "Open";
        }

        public static async Task<string> GetCatFactAsync()
        {
            using var client = new HttpClient();
            try
            {
                // Use a real cat facts API; fall back to a local fact on any failure
                var response = await client.GetStringAsync("https://catfact.ninja/fact");
                var fact = TryExtractFactFromJson(response);
                if (!string.IsNullOrEmpty(fact))
                {
                    return fact!;
                }
            }
            catch
            {
                // ignore and fall through to local facts
            }

            // Local fallback fact selected randomly
            return _localFacts[_rand.Next(_localFacts.Length)];
        }

        private static readonly string[] _localFacts = new[]
    {
        "Cats sleep for 12-16 hours a day — true masters of rest.",
        "A group of kittens is called a kindle.",
        "Cats can make over 100 different vocal sounds.",
        "Adult cats meow to communicate primarily with humans, not other cats.",
        "The hearing of the average domestic cat is at least five times keener than that of a human adult.",
    };

        private static string? TryExtractFactFromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            const string key = "\"fact\":\"";
            var idx = json.IndexOf(key, StringComparison.Ordinal);
            if (idx < 0)
                return null;

            var start = idx + key.Length;
            var sb = new StringBuilder();
            for (int i = start; i < json.Length; i++)
            {
                var ch = json[i];
                if (ch == '"')
                    break;
                sb.Append(ch);
            }

            var raw = sb.ToString();
            // Unescape common JSON escape sequences
            try
            {
                return Regex.Unescape(raw);
            }
            catch
            {
                return raw;
            }
        }

        public override IContent[] GetContent()
        {
            // Gather lively bits we want to show in the palette
            var catFact = GetCatFactAsync().GetAwaiter().GetResult();

            // Build a friendly markdown block with emoji, image, fact, pun and mood meter
            var md = new StringBuilder();
            md.AppendLine("## 😺 Cat Fun — " + CmdPalCatFunPhrases.GetRandomPhrase() + "!");
            md.AppendLine();
            md.AppendLine("**Cat Fact:** " + catFact);
            md.AppendLine();

            return [
                new MarkdownContent(md.ToString())
            ];
        }
    }
}
