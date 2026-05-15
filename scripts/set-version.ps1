param(
    [Parameter(Mandatory = $true)]
    [string]$Version
)

$FilePath = "../Directory.Build.props";

$content = Get-Content $FilePath -Raw
$updatedContent = $content -replace '<VersionPrefix>.*?</VersionPrefix>', "<VersionPrefix>$Version</VersionPrefix>"

Set-Content -Path $FilePath -Value $updatedContent -Encoding UTF8

Write-Host "VersionPrefix updated to $Version in $FilePath"