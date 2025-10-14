param(
    [string]$Configuration = "Release",
    [string]$Version = "0.0.2.0",
    [string]$Platform = "x64"
)

$ErrorActionPreference = "Stop"

Write-Host "Building Random Riddle Extension EXE installer..." -ForegroundColor Green
Write-Host "Version: $Version" -ForegroundColor Yellow

$ProjectDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ProjectFile = "$ProjectDir\CmdPalRandomRiddleExtension.csproj"

# Clean previous builds
Write-Host "Cleaning previous builds..." -ForegroundColor Yellow
if (Test-Path "$ProjectDir\bin") { 
    Remove-Item -Path "$ProjectDir\bin" -Recurse -Force -ErrorAction SilentlyContinue 
}
if (Test-Path "$ProjectDir\obj") { 
    Remove-Item -Path "$ProjectDir\obj" -Recurse -Force -ErrorAction SilentlyContinue 
}

# Restore packages
Write-Host "Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore $ProjectFile

# Build and publish
Write-Host "Building and publishing application..." -ForegroundColor Yellow
dotnet publish $ProjectFile `
    --configuration $Configuration `
    --runtime "win-$Platform" `
    --self-contained true `
    --output "$ProjectDir\bin\$Configuration\win-$Platform\publish"

if ($LASTEXITCODE -ne 0) { 
    throw "Build failed with exit code: $LASTEXITCODE" 
}

# Check if files were published
$publishDir = "$ProjectDir\bin\$Configuration\win-$Platform\publish"
$fileCount = (Get-ChildItem -Path $publishDir -Recurse -File).Count
Write-Host "✅ Published $fileCount files to $publishDir" -ForegroundColor Green

# Update version in setup.iss
Write-Host "Updating installer script version..." -ForegroundColor Yellow
$setupTemplate = Get-Content "$ProjectDir\setup-template.iss" -Raw
$setupScript = $setupTemplate -replace '#define AppVersion ".*"', "#define AppVersion `"$Version`""
$setupScript | Out-File -FilePath "$ProjectDir\setup.iss" -Encoding UTF8

# Create installer with Inno Setup
Write-Host "Creating installer with Inno Setup..." -ForegroundColor Yellow
$InnoSetupPath = "${env:ProgramFiles(x86)}\Inno Setup 6\iscc.exe"
if (-not (Test-Path $InnoSetupPath)) { 
    $InnoSetupPath = "${env:ProgramFiles}\Inno Setup 6\iscc.exe" 
}

if (Test-Path $InnoSetupPath) {
    & $InnoSetupPath "$ProjectDir\setup.iss"
    
    if ($LASTEXITCODE -eq 0) {
        $installer = Get-ChildItem "$ProjectDir\bin\$Configuration\installer\*.exe" | Select-Object -First 1
        if ($installer) {
            $sizeMB = [math]::Round($installer.Length / 1MB, 2)
            Write-Host "✅ Created installer: $($installer.Name) ($sizeMB MB)" -ForegroundColor Green
        }
    } else {
        throw "Inno Setup failed with exit code: $LASTEXITCODE"
    }
} else {
    Write-Warning "Inno Setup not found at expected locations"
}

Write-Host "🎉 Build completed successfully!" -ForegroundColor Green