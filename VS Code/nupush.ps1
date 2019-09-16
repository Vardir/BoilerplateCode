param (
    [String[]] $projects, [String] $path
)

#functions
function push ([String] $origin, [String] $destination, [String] $destinationLocation = "local") {
    Write-Host "Pushing '$origin'..."
    
    switch ($destinationLocation) {
        "web" {
            Write-Error "Couldn't push '$origin' package: web push is not implemented yet..." -ErrorAction Ignore
        }
        "local" {
            try {
                Copy-Item $origin $destination -Force
            }
            catch {
                Write-Error "Unable to push package from '$origin'. Error was: $_" -ErrorAction Ignore
            }
        }
        Default {
            Write-Error "Couldn't push '$origin' package: invalid path..." -ErrorAction Ignore
        }
    }
}

function isWebUrl ([String] $address) {
    $uri = $address -as [System.URI]

	Return $null -ne $uri.AbsoluteURI -and $uri.Scheme -match '[http|https]'
}
#functions

$pathKind = "local"

Write-Host "Sending projects' packages to '$path'"

if (isWebUrl($path)) {
    $pathKind = "web"
}
elseif (-not (Test-Path -LiteralPath $path)) {
    Write-Error "Couldn't push '$project' package: project not found..." -ErrorAction Stop
}

foreach ($project in $projects) {
    if (-not (Test-Path -LiteralPath $project)) {
        Write-Error "Couldn't push '$project' package: project not found..." -ErrorAction Ignore

        Continue
    }

    push "$project\publish\*.nupkg" $path $pathKind
}