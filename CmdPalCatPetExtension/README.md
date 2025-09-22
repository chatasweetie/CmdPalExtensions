# Cat Pet — Virtual Cat for the Command Palette

A playful, virtual cat you can adopt and care for from the Command Palette. You can adopt a cat, play, feed, groom, put it to bed, view status and achievements — all without leaving the palette.

Key highlights

- Local-first: pet state persists on disk (no telemetry or external servers by default).
- Lightweight WinUI extension built for .NET 9 and the Microsoft Command Palette framework.
- Actions: adopt/create, play, feed, groom, sleep, view status, manage achievements, and give up for adoption.
- Personality system and achievements to make each cat feel unique.



1. Open the Command Palette and choose the "Pet Cat" extension.
2. Create (adopt) a new cat and give it a name.
3. Use actions from the palette: Play, Feed, Groom, Put to Bed, View Status, View Achievements, or Give Up for Adoption.
4. The cat's stats (Energy, Hunger, Happiness, Hygiene) change based on actions and passively over time. Progress and achievements are saved locally.

Features

- Adopt and name a virtual cat with randomized personality and initial stats.
- Play with your cat (increases Happiness, decreases Energy and Hygiene over time).
- Feed your cat (reduces Hunger, increases Happiness/Energy depending on food).
- Put the cat to bed (restore Energy, small Hunger increase).
- Groom to restore Hygiene.
- View Cat Status (Energy, Hunger, Happiness, Hygiene, personality, counters).
- Achievements system with many unlockable badges (first play, first feed, multi-day ownership, stat milestones, personality badges, etc.).
- Persistent storage: cat data saved under LocalApplicationData so your pet is available across sessions.
- Small, focused UI surfaces — designed to be operated entirely from the Command Palette.

Where pet data is stored

Saved file (JSON):
%LOCALAPPDATA%\CmdPalCatPetExtension\virtualcat.json

If you need to remove a saved cat during testing, see `Tools/DeleteVirtualCat.ps1` or delete the file above.

Project structure (key files)

- `CmdPalCatPetExtension/`
  - `CmdPalCatPetExtensionCommandsProvider.cs` — registers the top-level "Pet Cat" command.
  - `CmdPalCatPetExtension.cs` / `Program.cs` — extension bootstrap and registration.
  - `Pages/`
    - `CmdPalCatPetExtensionPage.cs` — top-level page that routes to other pages.
    - `CreateCatPage.cs` — adopt / create a new cat and set its name.
    - `FeedCat.cs` — feeding UI and logic.
    - `PlayWithCat.cs` — play interactions and timers.
    - `PutCatToBed.cs` — sleeping logic to restore energy.
    - `Groom.cs` — grooming action to restore hygiene.
    - `GiveUpForAdoptionPage.cs` — remove the current cat (delete save).
    - `CatStatusPage.cs` — view current stats and counters.
    - `AchievementsPage.cs` — view and claim achievements.
  - `Models/VirtualCat.cs` — in-memory model for a cat (Name, Energy, Hunger, Happiness, Hygiene, counters, personality).
  - `Services/`
    - `CatRepository.cs` — local JSON persistence and change notifications.
    - `AchievementsService.cs` — achievement definitions and unlock logic.
    - `CatJsonContext.cs`, `CatRepository.cs` — storage utilities for managing saved data.
  - `Assets/` — cat images and icons used by the UI.
  - `Tools/DeleteVirtualCat.ps1` — helper script for test cleanup.

Requirements

- Windows 10 / Windows 11 with the Windows App SDK supported version (this project targets net9.0-windows10.0.22621.0).
- .NET 9 SDK
- Microsoft Command Palette runtime (the extension targets the Microsoft.CommandPalette.Extensions framework).


How to use (common commands)

- Open the Command Palette > Pet Cat
- Create a cat: "Adopt" or "Create Cat"
- Feed: choose a food item to feed the cat
- Play: select playtime length to increase happiness
- Groom: restore hygiene
- Put to Bed: choose rest duration to restore energy
- View Achievements: see unlocked achievements and badges
- Give Up for Adoption: delete saved cat

Privacy

All cat state is stored locally in user-local application data. No external services are contacted by default.

Contributing

Contributions, issues, and suggestions are welcome. Tips:

- Open an issue for feature requests or bugs.
- For changes, fork the repo, create a branch, and submit a pull request with a clear description.
- Keep changes small and focused; update or add unit tests where applicable.

License

This project is licensed under the MIT License — see the LICENSE file for details.

Author

Jessica Dene Earley-Cha — [https://github.com/chatasweetie](https://github.com/chatasweetie)