$ErrorActionPreference = "Stop"

$Version = "1.0.0"
$ModName = "AstroLupine"
$OutputDir = "release"
$ZipName = "$OutputDir\$ModName`_v$Version.zip"

Write-Host "🐺 Starting Astro Lupine Release Build Process..." -ForegroundColor Cyan

# 1. Build the C# DLL
Write-Host "`n[1/3] Building C# project in ExportRelease mode..." -ForegroundColor Yellow
dotnet build -c ExportRelease
if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed! Please check the errors above."
    exit $LASTEXITCODE
}

# 2. Check for required files
Write-Host "`n[2/3] Checking required files..." -ForegroundColor Yellow
$DllPath = ".godot\mono\temp\bin\ExportRelease\$ModName.dll"
$PckPath = "$ModName.pck"
$JsonPath = "$ModName.json"

$MissingFiles = @()
if (-Not (Test-Path $DllPath)) { $MissingFiles += $DllPath }
if (-Not (Test-Path $PckPath)) { $MissingFiles += $PckPath }
if (-Not (Test-Path $JsonPath)) { $MissingFiles += $JsonPath }

if ($MissingFiles.Count -gt 0) {
    Write-Host "Error: The following required files are missing:" -ForegroundColor Red
    foreach ($file in $MissingFiles) {
        Write-Host "  - $file" -ForegroundColor Red
    }
    Write-Host "Did you forget to export the PCK from Godot? Please open Godot and export AstroLupine.pck." -ForegroundColor Red
    exit 1
}

# 3. Create the Release Zip
Write-Host "`n[3/3] Creating Release Archive ($ZipName)..." -ForegroundColor Yellow

if (-Not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
}

if (Test-Path $ZipName) {
    Remove-Item $ZipName -Force
}

# Create a temporary staging directory
$TempStage = "$OutputDir\temp_stage"
if (Test-Path $TempStage) { Remove-Item -Recurse -Force $TempStage }
New-Item -ItemType Directory -Path "$TempStage\$ModName" | Out-Null

Copy-Item $DllPath -Destination "$TempStage\$ModName\"
Copy-Item $PckPath -Destination "$TempStage\$ModName\"
Copy-Item $JsonPath -Destination "$TempStage\$ModName\"

# Compress
Compress-Archive -Path "$TempStage\$ModName" -DestinationPath $ZipName -Force

# Clean up staging
Remove-Item -Recurse -Force $TempStage

Write-Host "`n✅ Successfully created release package at $ZipName!" -ForegroundColor Green
Write-Host "You can now distribute this zip file to players or upload it to mod platforms." -ForegroundColor Cyan
