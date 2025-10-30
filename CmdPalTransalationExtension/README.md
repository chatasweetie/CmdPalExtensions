# CmdPal Translation Extension

A Command Palette extension that provides quick translation and language tools directly inside the Windows Command Palette.

This extension exposes translation helpers and language data (local phrases and Adaptive Card templates) so users can translate short text snippets and access language information without leaving the palette.

## Features
- Translate short text snippets between supported languages using the built-in `Translator` helper.
- Provide language data and small adaptive-card templates for rich rendering in the palette.
- Local-first language data via `LanguageData.cs` (no external services required unless a network translator is wired in).

## Development / build
- Requires .NET 9 SDK and a Windows 10/11 SDK compatible with `net9.0-windows10.0.22621.0`.
- The project uses MSIX tooling (`EnableMsixTooling` in the project) and can be packaged with the Single-project MSIX Packaging Tools in Visual Studio.

To build locally:
1. Restore packages: `dotnet restore` (run from the repository root or project folder).
2. Build: `dotnet build -c Release`.


## Files of interest
- `Translator.cs` — translation helper and wiring for translation actions.
- `LanguageData.cs` — local language/phrase data used by the extension.
- `AdaptiveCardTemplates.cs` — adaptive card templates used to render rich content.
- `CmdPalTransalationExtensionCommandsProvider.cs` — registers the extension commands with the host.

## Published status
- This project is not published to the Microsoft Store or WinGet. It is present in this repository and can be packaged locally for testing, but there is no active store listing or installer YAML in this folder.

