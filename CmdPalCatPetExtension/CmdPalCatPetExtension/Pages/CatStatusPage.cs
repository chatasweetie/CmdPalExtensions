using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using CmdPalCatPetExtension.Models;
using CmdPalCatPetExtension.Services;

namespace CmdPalCatPetExtension.Pages
{
    internal sealed partial class CatStatusPage : ContentPage
    {
        public CatStatusPage()
        {
            Icon = new("🐾");
            Title = "Cat Status";
            Name = "Cat Status";
            // Add a Back command so Enter will navigate back to the parent page
            Commands = [
                new CommandContextItem(new AnonymousCommand(() => { }) { Result = CommandResult.GoBack() }) { Title = "Back" },
            ];
        }

        public override IContent[] GetContent()
        {
            var cat = CatRepository.Load();
            if (cat is null)
            {
                var mdNone = "# No cat yet 😿\n\nYou don't have a cat — select **Create a Cat** from the previous menu to adopt one!";
                return [ new MarkdownContent(mdNone) ];
            }

            // Create a copy for display so we don't persist Tick effects immediately
            var displayCat = new VirtualCat(cat.Name, cat.Energy, cat.Hunger, cat.Happiness);
            var elapsed = DateTime.UtcNow - cat.LastUpdatedUtc;
            displayCat.Tick(elapsed);

            static string Bar(int value, bool invert = false)
            {
                const int segments = 10;
                // Keep bar color consistent: always show filled segments in green for clarity
                var displayValue = invert ? (100 - value) : value; // keep for future use if needed
                string fillEmoji = "🟩";

                int filled = Math.Max(0, Math.Min(segments, (int)Math.Round(value * segments / 100.0)));
                var sb = new System.Text.StringBuilder();
                // No status emoji/text here; keep the bar consistent and presented in a table
                for (int i = 0; i < filled; i++) sb.Append(fillEmoji);
                for (int i = filled; i < segments; i++) sb.Append('⬜');
                sb.Append(' ').Append(' ').Append(value).Append("/100");
                return sb.ToString();
            }

            var energyBar = Bar(displayCat.Energy);
            var hungerBar = Bar(displayCat.Hunger, invert: true);
            var happinessBar = Bar(displayCat.Happiness);

            string md = $@"# 😺 {displayCat.Name}

```
Energy:     {energyBar}
Hunger:     {hungerBar}
Happiness:  {happinessBar}
```

Press Enter to go back
";

            return [ new MarkdownContent(md) ];
        }
    }
}
