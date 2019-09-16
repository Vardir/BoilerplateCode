param (
    [String[]] $projects, [String] $path
)

$packedProjects = @()

foreach ($project in $projects) {
    if (& "$PSScriptRoot\nupack.ps1" $project) {
        $packedProjects += $project
    }
}

& "$PSScriptRoot\nupush.ps1" $packedProjects $path