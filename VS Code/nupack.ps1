param (
    [String]$projectName, [String]$destination=""
)

if ($destination -eq "") { $destination = "$projectName\publish\" }

Write-Host "Initiating package creation for project '$projectName'. Destination folder is '$destination'"

if (-not (Test-Path -LiteralPath $projectName)) {
    Write-Error "Couldn't create package: project '$projectName' not found..."

    Return $false    
}

if (-not (Test-Path -LiteralPath $destination)) { 
    try {
        New-Item $destination -ItemType Directory -ErrorAction Stop | Out-Null #-Force
    }
    catch {
        Write-Error "Unable to create directory '$destination'. Error was: $_"

        Return $false
    }
}

$initialFolder = (Get-Item -Path ".\").FullName

Set-Location $projectName

try
{
    nuget pack
}
catch {
    Write-Error "Unable to create package from '$projectName'. Error was: $_"

    Return $false
}

Set-Location $initialFolder

try {
    Move-Item "$projectName\*.nupkg" $destination -Force
}
catch {
    Write-Error "Unable to move package '$projectName'. Error was: $_"

    Return $false
}

Return $true