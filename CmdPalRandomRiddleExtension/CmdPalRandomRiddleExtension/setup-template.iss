; Inno Setup Script for Random Riddle Extension
#define AppVersion "0.0.2.0"

[Setup]
AppId={{8A2F7B9C-3D4E-5F60-7182-93A4B5C6D7E8}}
AppName=Random Riddle Extension
AppVersion={#AppVersion}
AppPublisher=Jessica Dene Earley-Cha
DefaultDirName={autopf}\CmdPalRandomRiddleExtension
OutputDir=bin\Release\installer
OutputBaseFilename=CmdPalRandomRiddleExtension-Setup-{#AppVersion}
Compression=lzma
SolidCompression=yes
MinVersion=10.0.19041

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "bin\Release\win-x64\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\Random Riddle Extension"; Filename: "{app}\CmdPalRandomRiddleExtension.exe"

[Registry]
Root: HKCU; Subkey: "SOFTWARE\Classes\CLSID\{{7c470b82-23b1-44c4-a1dc-c093f7f493c9}}"; ValueData: "CmdPalRandomRiddleExtension"
Root: HKCU; Subkey: "SOFTWARE\Classes\CLSID\{{7c470b82-23b1-44c4-a1dc-c093f7f493c9}}\LocalServer32"; ValueData: "{app}\CmdPalRandomRiddleExtension.exe -RegisterProcessAsComServer"