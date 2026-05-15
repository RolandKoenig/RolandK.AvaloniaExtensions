# Cleanup first
pwsh -WorkingDirectory ./workflows ./workflows/cleanup.ps1

# Build and test
pwsh -WorkingDirectory ./workflows ./workflows/restore.ps1
pwsh -WorkingDirectory ./workflows ./workflows/build.ps1
pwsh -WorkingDirectory ./workflows ./workflows/test.ps1