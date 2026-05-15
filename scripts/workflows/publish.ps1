# definition of parameters
Param(
    [Parameter(Mandatory = $false)]
    [string]$NugetApiKey,

    [Parameter(Mandatory = $true)]
    [string]$Version
)

if ([string]::IsNullOrWhiteSpace($NugetApiKey)) {
    $NugetApiKey = $env:NUGET_API_KEY
}

if ([string]::IsNullOrWhiteSpace($NugetApiKey)) {
    Write-Error "NuGet API key was not provided. Pass -NugetApiKey or set the NUGET_API_KEY environment variable."
    exit 1
}

dotnet nuget push ../../publish/*.nupkg --api-key $NugetApiKey --source https://api.nuget.org/v3/index.json