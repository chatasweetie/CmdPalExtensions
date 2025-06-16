# CmdPalRandomRiddleExtension

A simple [Command Palette extension](https://learn.microsoft.com/en-us/windows/powertoys/command-palette/extensibility-overview) for .NET 9 that fetches and displays a random riddle, allowing users to reveal the answer interactively.

![gif of Command Palette using extension](.\assets\random-riddle.gif)

## Features

- Fetches a random riddle from an online API.
- Displays the riddle in the command palette.
- Lets the user reveal the answer by pressing Enter.
- Fetches a new riddle each time the user requests the answer.

## How It Works

1. Initialization
When the extension page is created, it sets up the icon, title, and name for the command palette entry.
2. Fetching a Riddle:
   - The first time the page is shown, it fetches a random riddle  from riddles-api.vercel.app using HttpClient.
   - The riddle and its answer are cached in a private field (_currentRiddle) to ensure consistency between the displayed riddle and the answer dialog.
3. Displaying the Riddle
   - The riddle text is shown as Markdown content in the command palette.
   - The user is prompted to "Press Enter for the answer".
4. Revealing the Answer
   - When the user presses Enter (selects the command), a confirmation dialog is shown with the riddle's answer.
   - After the answer is shown, the cached riddle is cleared, so the next time the user interacts, a new riddle is fetched.

## API

This extension uses the following public API to fetch riddles: https://riddles-api.vercel.app/ 

## Code Structure

- **CmdPalRandomRiddleExtensionPage.cs** : Main logic for fetching, displaying, and interacting with riddles.
- **Riddle.cs**: Data model for the riddle and answer.

## How to get it Random Riddle in Command Palette

1.
2.

## Dependencies

- .NET 9
- Microsoft.CommandPalette.Extensions
- System.Net.Http
- System.Text.Json

## Customization

- To change the riddle source, update the API URL in GetRiddleAsync().
- To add more commands or actions, modify the Commands array in GetContent().

## License

This project is licensed under the MIT License. See the LICENSE file for details.