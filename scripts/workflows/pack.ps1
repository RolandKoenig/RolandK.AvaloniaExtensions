param(
	[string]$Version
)

# Clear previous artifacts
if (Test-Path "../../publish")
{
	"Clearing ../../publish"
	Remove-Item -Path "../../publish/*" -Recurse -Force;
}

# Build nuget packages
$argList = @("pack", "-c", "Release", "-o", "../../publish", "../../RolandK.AvaloniaExtensions.desktop.slnf", "/p:ContinuousIntegrationBuild=true", "/p:IncludeSymbols=true", "/p:EmbedUntrackedSources=true", "-p:SymbolPackageFormat=snupkg")
if (![string]::IsNullOrWhiteSpace($Version)) {
	$argList += "/p:Version=$Version"
}

dotnet @argList