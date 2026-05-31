param(
    [string]$Version
)

$argList = @("build", "-c", "Release", "../../RolandK.AvaloniaExtensions.desktop.slnf")
if (![string]::IsNullOrWhiteSpace($Version)) {
    $argList += "/p:Version=$Version"
}

dotnet @argList