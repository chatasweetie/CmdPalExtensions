# Cat Fun — Command Palette Extension

A simple Command Palette extension that provides bite-sized cat entertainment — fun facts, photos, puns, and short phrases — directly inside the Command Palette.

![demo of Cat Fun in the Command Palette](./Assets/catfundemo.gif)

## Features

• Shows a random cat fact
• Displays a cat photo
• Shows a playful cat puns

## Installation

> **Note:** This extension is built as a Command Palette extension and requires [Microsoft PowerToys](https://learn.microsoft.com/en-us/windows/powertoys/command-palette/extensibility-overview) (and the Command Palette host) to be installed for use.

### Microsoft Store installation (recommended)

When published, the recommended route is via the Microsoft Store so users can install and update automatically from there.

<!--
### Alternative installation methods

**Via Winget:**

```powershell
winget install JessicaDEarleyCha.CmdPalCatFunExtension
```

**Manual MSIX installation:**

1. Download the latest MSIX package from the repository Releases.
2. Right-click the MSIX file and select "Install".
3. Follow the installation prompts.
-->

## How It Works

1. **Initialization:** The extension registers a provider name and icon used by the Command Palette host.
2. **Fetching content:** Each command page fetches content as required (fun facts, photos, or puns) using an HttpClient or local phrase list.
3. **Displaying content:** Content is presented as Markdown or richly formatted text in the Command Palette UI.
4. **Interactive actions:** The user can press Enter or select actions to reveal additional content (for example, reveal a longer fact or show a photo in a panel).
5. **Subsequent activations:** Each activation requests fresh content or rotates through the bundled phrase lists so the experience stays fresh.

## APIs and Data Sources

This extension may use public cat-related APIs and local phrase lists. Examples:

- TheCatAPI (https://thecatapi.com/) — for photos (when enabled)
- Public cat-fact endpoints (various public APIs) — for quick facts

Note: API usage in production should follow the API provider's terms and include any required attribution or API keys.

## Code Structure

- `Pages/CmdPalCatFunExtensionFunFactsPage.cs` — fetch and render short cat facts
- `Pages/CmdPalCatFunExtensionPhotosPage.cs` — fetch and render cat photos (or show a placeholder)
- `Pages/CmdPalCatFunExtensionPunPage.cs` — deliver cat puns and playful one-liners
- `Pages/CmdPalCatFunExtensionPage.cs` — top-level command page wiring (menu + routing)
- `Pages/CmdPalCatFunPhrases.cs` — local list of phrases, puns, and canned facts
- `CmdPalCatFunExtension.cs` / `Program.cs` — extension bootstrap and registration

## Dependencies

- .NET 9
- Microsoft.CommandPalette.Extensions
- Microsoft.WindowsAppSDK (if using WinAppSDK features)
- Microsoft.Web.WebView2 (only if showing rich photo panels)
- System.Net.Http
- System.Text.Json or Newtonsoft.Json (for JSON API responses)

## Customization

- Change the source of content by editing the API endpoints in the fetch methods.
- Edit or extend the local phrase list in `Pages/CmdPalCatFunPhrases.cs` to add more puns and facts.
- Modify the SVGs in `Assets/` to update the visual identity (I included a consistent orange-cat motif across SVGs).

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Author

[Jessica Dene Earley-Cha](htthttps://www.jessicadeneearley-cha.com/)  |  [https://github.com/chatasweetie](https://github.com/chatasweetie)

