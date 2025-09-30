# Command Palette Extensions

A collection of Command Palette extensions for Windows that enhance your productivity and provide entertainment through the Windows Command Palette interface.

## Available Extensions

### 🎯 Random Riddle Extension

**Get random riddles directly in your Command Palette!**

- **Description**: Fetches and displays random riddles with interactive answer reveals
- **API Source**: [riddles-api.vercel.app](https://riddles-api.vercel.app/)
- **Installation**: [Microsoft Store](https://apps.microsoft.com/detail/9ppntdcd5s8z)
- **Repository**: [CmdPalRandomRiddleExtension](./CmdPalRandomRiddleExtension/)

**Features:**
- Fetch random riddles from online API
- Interactive answer reveal with Enter key
- Fresh riddle with each activation
- Clean, distraction-free interface


### 🐱 Cat Fun Extension

**Bring bite-sized cat entertainment to your Command Palette.**

- **Description**: Provides random cat facts, photos, and playful puns directly inside the Command Palette. Photos are optionally fetched from TheCatAPI and facts/puns come from a mix of public APIs and bundled phrase lists.
- **Repository**: [CmdPalCatFunExtension](./CmdPalCatFunExtension/)
- **Installation**: [Microsoft Store](https://apps.microsoft.com/store/detail/cat-pet-extension/9NMK0M2D18CQ)

**Features:**
- Show a random cat fact
- Display a cat photo (TheCatAPI if enabled)
- Deliver playful cat puns and short phrases
- Interactive actions to reveal more content or open images


### 🐾 Cat Pet Extension

**A playful, virtual cat you can adopt and care for from the Command Palette.**

- **Description**: Virtual cat companion with interactive care system - adopt, play, feed, groom, and manage achievements all within the Command Palette
- **Installation**: [Microsoft Store](https://apps.microsoft.com/store/detail/cat-pet-extension/9P7JQZQZQZQZ) 
- **Repository**: [CmdPalCatPetExtension](./CmdPalCatPetExtension/)

**Features:**
- Adopt virtual cats with unique personalities and traits
- Interactive care system: play, feed, groom, and put to bed
- Dynamic stats tracking: Energy, Hunger, Happiness, and Hygiene
- Rich achievements system with unlockable badges
- Local-first persistence (no telemetry or external servers)
- Built for .NET 9 and Microsoft Command Palette framework

## Prerequisites

All extensions in this repository require:

- **Windows 11** or **Windows 10** (version 2004 or newer)
- **Microsoft PowerToys** with Command Palette enabled
- **Internet connection** (for extensions that fetch online content)


## Getting Started

1. Install [Microsoft PowerToys](https://apps.microsoft.com/detail/xp89dcgq3k6vld)
2. Enable Command Palette in PowerToys settings
3. Install your desired extensions (See individual extension directories for specific installation instructions)
4. Press `Ctrl + Shift + P` to open Command Palette
5. Search for and activate your installed extensions

## Repository Structure

```text
CmdPalExtensions/
├── CmdPalRandomRiddleExtension/   # Random riddle extension
│   ├── README.md                  # Extension-specific documentation
│   ├── CmdPalRandomRiddleExtension/
│   └── CmdPalRandomRiddleExtension.sln
├── CmdPalCatFunExtension/         # Cat entertainment extension
│   ├── README.md                  # Extension-specific documentation
│   ├── CmdPalCatFunExtension/
│   └── CmdPalCatFunExtension.sln
├── CmdPalCatPetExtension/         # Virtual cat pet extension
│   ├── README.md                  # Extension-specific documentation
│   ├── CmdPalCatPetExtension/
│   └── CmdPalCatPetExtension.sln
└── (future extensions will be added here)
```

## Future Extensions

This repository will continue to grow with new Command Palette extensions. Each extension will:

- Have its own directory with complete source code
- Include detailed documentation and installation instructions
- Be independently installable and maintained
- Follow Windows Command Palette development best practices

## Contributing

Contributions are welcome! Whether you want to:

- Report bugs or suggest features for existing extensions
- Contribute code improvements
- Propose new extension ideas

Please check individual extension directories for specific contribution guidelines.

## License

This project is licensed under the MIT License. See individual extension directories for specific license information.

## Author

[Jessica Dene Earley-Cha](htthttps://www.jessicadeneearley-cha.com/)  |  [https://github.com/chatasweetie](https://github.com/chatasweetie)
