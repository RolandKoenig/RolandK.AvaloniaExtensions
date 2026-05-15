param(
    [Parameter(Mandatory = $true)]
    [string]$Tag
)

git fetch origin main
$originMainHash = git rev-parse origin/main
$tagHash = git rev-parse $Tag

Write-Host "Commit hash on branch [main]: $originMainHash"
Write-Host "Commit hash of Tag [$Tag]: $tagHash"

if ($originMainHash -eq $tagHash) {
    Write-Host "Tag is on main"
}
else {
    Write-Host "Tag is NOT origin/main, publish canceled"
    exit 1
}