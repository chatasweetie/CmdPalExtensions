# Cat Pet — Virtual Cat Pet for Command Palette

A playful virtual cat pet built as a Command Palette extension for .NET 9. Users can adopt a cat, play with it, feed it, and put it to bed — all from the Command Palette UI.

## Features

• Adopt a virtual cat with a name and basic stats (hunger, happiness, energy)
• Play with the cat to increase happiness
• Feed the cat to decrease hunger
• Put the cat to bed to restore energy
• Persistent state between activations (local storage)
• Cat personalities (e.g. playful, lazy, curious) that alter behavior and stat changes

## Installation

> **Note:** This extension requires [Microsoft PowerToys](https://apps.microsoft.com/detail/xp89dcgq3k6vld) with Command Palette enabled.

### Microsoft Store (recommended)

When published, the Microsoft Store is the recommended install route for automatic updates.

### Manual MSIX installation (for testing)

1. Build or download the latest MSIX from Releases.
2. Right‑click the MSIX file and select "Install".
3. Follow the installer prompts.

## How It Works

1. **Adopt:** The user runs the extension and chooses to adopt a new cat. The extension initializes a pet profile with default stats.
2. **Play / Feed / Sleep:** Each action adjusts the pet's stats. For example, feeding reduces hunger and playing increases happiness but lowers energy.
3. **Persistent State:** Pet state is saved locally so returning to the extension resumes where the user left off.
4. **UI:** Uses simple Markdown/cards in the Command Palette for status and action buttons. Optional image panels show cat photos when enabled.

## Code Structure

- `Pages/CmdPalCatPetExtensionPage.cs` — top-level UI and command routing
- `Pages/AdoptCatPage.cs` — adopt and name a new cat
- `Pages/PlayCatPage.cs` — play interactions and mini-games
- `Pages/FeedCatPage.cs` — food options and feeding logic
- `Pages/BedCatPage.cs` — sleep/energy mechanics
- `Services/PetStateService.cs` — persistent storage and state management
- `Models/Pet.cs` — pet model (name, hunger, happiness, energy, lastActive)
- `CmdPalCatPetExtension.cs` / `Program.cs` — bootstrap and registration

## Dependencies

- .NET 9
- Microsoft.CommandPalette.Extensions
- System.Text.Json
- (Optional) [TheCatAPI](https://thecatapi.com) for photos

## Customization

- Adjust stat change values and timers in `PetStateService.cs` to tune gameplay
- Add new actions (groom, teach trick) by adding pages and updating the pet model
- Replace or add images in `Assets/` to change the cat's appearance

## Privacy

All pet state is stored locally. No external servers are contacted by default unless the user enables optional photo fetching from third‑party APIs.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Author

[Jessica Dene Earley-Cha](https://github.com/chatasweetie)