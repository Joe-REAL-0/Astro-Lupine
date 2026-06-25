$ErrorActionPreference = "Stop"

$Version = "1.2.2"
# 从 changelog.txt 中读取更新日志，并将换行符替换为 JSON 格式的 \n
$ChangelogPath = "changelog.txt"
if (Test-Path $ChangelogPath) {
    $RawChangelog = Get-Content $ChangelogPath -Raw -Encoding UTF8
    $UpdateMessage = $RawChangelog.Trim() -replace '"', '\"' -replace '\r?\n', '\n'
}
else {
    $UpdateMessage = "常规更新与修复。"
}

$ModName = "AstroLupine"
$OutputDir = "release"
$ZipName = "$OutputDir\$ModName`_v$Version.zip"

Write-Host "🐺 Starting Astro Lupine Release Build Process..." -ForegroundColor Cyan

$Utf8NoBom = New-Object System.Text.UTF8Encoding $False

# 0. Sync version and update message
Write-Host "`n[1/6] Updating version and update message in configuration files..." -ForegroundColor Yellow

$ManifestPath = "$ModName.json"
if (Test-Path $ManifestPath) {
    $ManifestFull = (Get-Item $ManifestPath).FullName
    $ManifestContent = [System.IO.File]::ReadAllText($ManifestFull)
    $ManifestContent = $ManifestContent -replace '"version":\s*".*?"', "`"version`": `"$Version`""
    [System.IO.File]::WriteAllText($ManifestFull, $ManifestContent, $Utf8NoBom)
    Write-Host "  -> Updated $ManifestPath version to $Version"
}

$WorkshopJsonPath = "ModUploader-win-x64\NewModWorkspace\workshop.json"
if (Test-Path $WorkshopJsonPath) {
    $WorkshopFull = (Get-Item $WorkshopJsonPath).FullName
    $WorkshopContent = [System.IO.File]::ReadAllText($WorkshopFull)
    $WorkshopContent = $WorkshopContent -replace '"changeNote":\s*".*?"', "`"changeNote`": `"v$Version - $UpdateMessage`""
    [System.IO.File]::WriteAllText($WorkshopFull, $WorkshopContent, $Utf8NoBom)
    Write-Host "  -> Updated $WorkshopJsonPath changeNote"
}

# 1. Build the C# DLL
Write-Host "`n[2/6] Building C# project in ExportRelease mode..." -ForegroundColor Yellow
dotnet build -c ExportRelease
if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed! Please check the errors above."
    exit $LASTEXITCODE
}

# 2. Check for required files
Write-Host "`n[3/6] Checking required files..." -ForegroundColor Yellow
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
Write-Host "`n[4/6] Creating Release Archive ($ZipName)..." -ForegroundColor Yellow

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

# 4. Copy to ModUploader workspace
Write-Host "`n[5/6] Copying files to ModUploader workspace..." -ForegroundColor Yellow
$UploaderContentDir = "ModUploader-win-x64\NewModWorkspace\content"

if (-Not (Test-Path $UploaderContentDir)) {
    New-Item -ItemType Directory -Path $UploaderContentDir -Force | Out-Null
}

Copy-Item $DllPath -Destination $UploaderContentDir -Force
Copy-Item $PckPath -Destination $UploaderContentDir -Force
Copy-Item $JsonPath -Destination $UploaderContentDir -Force

Write-Host "`n✅ Successfully created release package at $ZipName!" -ForegroundColor Green
Write-Host "✅ Successfully copied files to ModUploader workspace!" -ForegroundColor Green

# 5. Upload to Steam Workshop
Write-Host "`n[6/6] Uploading Mod to Steam Workshop..." -ForegroundColor Yellow
$UploaderExe = ".\ModUploader-win-x64\ModUploader.exe"
$WorkspaceDir = "ModUploader-win-x64\NewModWorkspace"

if (Test-Path $UploaderExe) {
    Write-Host "Running ModUploader..." -ForegroundColor Cyan
    # Use Invoke-Expression or & call operator
    & $UploaderExe "upload" "-w" $WorkspaceDir
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Mod upload failed! Please check the output above."
        exit $LASTEXITCODE
    }
    Write-Host "`n✅ Successfully uploaded mod to Steam Workshop!" -ForegroundColor Green
}
else {
    Write-Warning "ModUploader.exe not found at $UploaderExe. Skipping upload step."
}

Write-Host "`n🐺 Release build and upload process completed!" -ForegroundColor Cyan
