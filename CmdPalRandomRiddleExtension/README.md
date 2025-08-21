# Random Riddle for Command Palette

A simple [Command Palette extension](https://learn.microsoft.com/en-us/windows/powertoys/command-palette/extensibility-overview) for .NET 9 that fetches and displays a random riddle, allowing users to reveal the answer interactively.

![gif of Command Palette using extension](.\assets\random-riddle.gif)

## Features

• Fetches a random riddle from an online API
• Displays riddles in the Command Palette interface  
• Interactive answer reveal with Enter key
• Fresh riddle with each new request
• Clean, distraction-free riddle experience

## Installation

> **Note:** This extension requires [Microsoft PowerToys](https://apps.microsoft.com/detail/xp89dcgq3k6vld) to be installed.

### Microsoft Store installation (recommended)

[Get it from Microsoft Store](https://apps.microsoft.com/detail/9ppntdcd5s8z)

<!-- ### Alternative installation methods

**Via Winget:**

```powershell
winget install JessicaDEarleyCha.CmdPalRandomRiddleExtension
```

**Manual MSIX installation:**

1. Download the latest MSIX package from [Releases](https://github.com/chatasweetie/CmdPalExtensions/releases)
2. Right-click the MSIX file and select "Install"
3. Follow the installation prompts -->

## How It Works

1. **Initialization:** Extension sets up the icon, title, and name for the command palette entry
2. **Fetching a Riddle:** First activation fetches a random riddle from [riddles-api.vercel.app](https://riddles-api.vercel.app/) using HttpClient
3. **Displaying the Riddle:** Riddle text is shown as Markdown content with "Press Enter for the answer" prompt
4. **Revealing the Answer:** Pressing Enter shows a confirmation dialog with the riddle's answer
5. **New Riddle:** After viewing an answer, the next activation fetches a fresh riddle

## API

This extension uses the public API: <https://riddles-api.vercel.app/>

## Code Structure

- **CmdPalRandomRiddleExtensionPage.cs:** Main logic for fetching, displaying, and interacting with riddles
- **Riddle.cs:** Data model for the riddle and answer

## Dependencies

- .NET 9
- Microsoft.CommandPalette.Extensions
- System.Net.Http
- System.Text.Json

## Customization

- To change the riddle source, update the API URL in `GetRiddleAsync()`
- To add more commands or actions, modify the Commands array in `GetContent()`

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Author

[Jessica Dene Earley-Cha](https://github.com/chatasweetie)