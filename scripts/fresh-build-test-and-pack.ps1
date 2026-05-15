param(
    [Parameter(Mandatory = $true)]
    [string]$Version
)

# Cleanup first
pwsh -WorkingDirectory ./workflows ./workflows/cleanup.ps1

# Build and test
pwsh -WorkingDirectory ./workflows ./workflows/restore.ps1
pwsh -WorkingDirectory ./workflows ./workflows/build.ps1 -Version $Version
pwsh -WorkingDirectory ./workflows ./workflows/test.ps1
pwsh -WorkingDirectory ./workflows ./workflows/pack.ps1 -Version $Version