# definition of parameters
Param(
    [Parameter(Mandatory = $true)]
    [string]$NugetApiKey,

    [Parameter(Mandatory = $true)]
    [string]$Version
)

dotnet nuget push ../publish/RolandK.AvaloniaExtensions.$Version.nupkg --api-key $NugetApiKey --source https://api.nuget.org/v3/index.json
dotnet nuget push ../publish/RolandK.AvaloniaExtensions.DependencyInjection.$Version.nupkg --api-key $NugetApiKey --source https://api.nuget.org/v3/index.json
dotnet nuget push ../publish/RolandK.AvaloniaExtensions.ExceptionHandling.$Version.nupkg --api-key $NugetApiKey --source https://api.nuget.org/v3/index.json
dotnet nuget push ../publish/RolandK.AvaloniaExtensions.ResponsiveControls.$Version.nupkg --api-key $NugetApiKey --source https://api.nuget.org/v3/index.json