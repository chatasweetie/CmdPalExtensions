param(
    [switch]$RemoveFolder
)

Write-Host "Searching for virtualcat.json under $env:LOCALAPPDATA (this may take a few seconds)..."

# Find all matches under LocalApplicationData
$matches = Get-ChildItem -Path $env:LOCALAPPDATA -Recurse -Filter "virtualcat.json" -ErrorAction SilentlyContinue -Force | Select-Object -ExpandProperty FullName -ErrorAction SilentlyContinue

if (-not $matches) {
    Write-Host "No virtualcat.json files found under $env:LOCALAPPDATA"
    exit 0
}

foreach ($path in $matches) {
    try {
        Remove-Item -Path $path -Force -ErrorAction Stop
        Write-Host "Deleted: $path"

        if ($RemoveFolder) {
            $parent = Split-Path -Path $path -Parent
            try {
                Remove-Item -Path $parent -Recurse -Force -ErrorAction Stop
                Write-Host ("Removed folder: {0}" -f $parent)
            }
            catch {
                Write-Host ("Failed removing folder {0}: {1}" -f $parent, $_)
            }
        }
    }
    catch {
        Write-Host ("Failed deleting {0}: {1}" -f $path, $_)
    }
}

Write-Host "Done."
