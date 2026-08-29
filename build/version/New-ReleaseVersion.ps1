#!/usr/bin/env pwsh

[CmdletBinding()]
param ()

$CurrentVersion = (gh release list --limit 1 --json tagName --jq '.[0].tagName')
[version]$CurrentDate = [version]::new((Get-Date -Format yyyy), (Get-Date).DayOfYear, 0)
[version]$CurrentVersion = if ([string]::IsNullOrWhiteSpace($CurrentVersion)) { '0.0.0' } else { $CurrentVersion }
[version]$IncrementVersion = [version]::new($CurrentVersion.Major, $CurrentVersion.Minor, $CurrentVersion.Build + 1)
[version]$TargetVersion = if ($CurrentDate -gt $IncrementVersion) { $CurrentDate } else { $IncrementVersion }

"VERSION=${TargetVersion}" >> $env:GITHUB_ENV