param(
    [string]$Version
)

$argList = @("build", "-c", "Release", "../../RolandK.AvaloniaExtensions.slnx")
if (![string]::IsNullOrWhiteSpace($Version)) {
    $argList += "/p:Version=$Version"
}

dotnet @argList