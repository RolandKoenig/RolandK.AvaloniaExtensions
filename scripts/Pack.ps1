# Clear previous artifacts
if (Test-Path "../publish")
{
	"Clearing ../publish"
	Remove-Item -Path "../publish/*" -Recurse -Force;
}

# Build nuget packages
dotnet pack -c Release -o ../publish "../src/RolandK.AvaloniaExtensions.sln"  /p:ContinuousIntegrationBuild=true /p:IncludeSymbols=true /p:EmbedUntrackedSources=true -p:SymbolPackageFormat=snupkg