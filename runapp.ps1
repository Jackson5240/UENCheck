# ============================================================
# PowerShell Script: Build, Test, and Launch ASP.NET Core App
# ============================================================

# Stop immediately if any command fails
$ErrorActionPreference = "Stop"

# Define project folders
$mainProj = ".\UENValidateProj"
$testProj = ".\UENValidateProj.Tests"

# ------------------------------------------------------------
# 1️⃣ Clear NuGet caches (optional but ensures clean restore)
# ------------------------------------------------------------
Write-Host "=== Clearing NuGet caches ===" -ForegroundColor Yellow
dotnet nuget locals all --clear

# ------------------------------------------------------------
# 2️⃣ Remove bin/obj folders if they exist
# ------------------------------------------------------------
Write-Host "=== Cleaning up bin/obj folders ===" -ForegroundColor Yellow

$foldersToDelete = @(
    "$mainProj\bin",
    "$mainProj\obj",
    "$testProj\bin",
    "$testProj\obj"
)

foreach ($folder in $foldersToDelete) {
    if (Test-Path $folder) {
        Write-Host "Deleting $folder ..." -ForegroundColor DarkGray
        Remove-Item -Recurse -Force $folder
    }
    else {
        Write-Host "Skipping (not found): $folder" -ForegroundColor DarkGray
    }
}

# ------------------------------------------------------------
# 3️⃣ Clean and build solution
# ------------------------------------------------------------
Write-Host "=== Cleaning solution ===" -ForegroundColor Cyan
dotnet clean

Write-Host "=== Building solution ===" -ForegroundColor Cyan
dotnet build

# ------------------------------------------------------------
# 4️⃣ Run unit tests
# ------------------------------------------------------------
Write-Host "=== Running tests ===" -ForegroundColor Cyan
dotnet test --logger "console;verbosity=normal"

# ------------------------------------------------------------
# 5️⃣ Start the main ASP.NET app
# ------------------------------------------------------------
Write-Host "=== Starting application ===" -ForegroundColor Cyan

# Start the app (in background)
Start-Process -FilePath "dotnet" -ArgumentList "run --project .\UENValidateProj\UENValidateProj.csproj"

# Wait a few seconds for app to boot up
Start-Sleep -Seconds 5

# ------------------------------------------------------------
# 6️⃣ Launch Chrome browser to the app URL
# ------------------------------------------------------------
Write-Host "=== Launching Chrome ===" -ForegroundColor Cyan
# Adjust port number if your launchSettings.json uses a different one
Start-Process "chrome.exe" "http://localhost:5003"

# ------------------------------------------------------------
# ✅ Completion message
# ------------------------------------------------------------
Write-Host "=== All steps completed successfully! ===" -ForegroundColor Green
