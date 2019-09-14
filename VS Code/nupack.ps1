param([String]$projectName, [String]$destination="")

if ($destination -eq "") { $destination = "$projectName\publish\" }

Write-Host "Initiating package creation for project '$projectName'. Destination folder is '$destination'"

if (-not (Test-Path -LiteralPath $projectName)) {
    Write-Error "Couldn't create package: project '$projectName' not found..." -ErrorAction Stop
}

if (-not (Test-Path -LiteralPath $destination)) { 
    try {
        New-Item -Path $destination -ItemType Directory -ErrorAction Stop | Out-Null #-Force
    }
    catch {
        Write-Error -Message "Unable to create directory '$destination'. Error was: $_" -ErrorAction Stop
    }
}

$initialFolder = (Get-Item -Path ".\").FullName

Set-Location $projectName

try
{
    nuget pack
}
catch {
    Write-Error -Message "Unable to create package from '$projectName'. Error was: $_" -ErrorAction Stop
}

Set-Location $initialFolder

Move-Item "$projectName\*.nupkg" $destination -Force