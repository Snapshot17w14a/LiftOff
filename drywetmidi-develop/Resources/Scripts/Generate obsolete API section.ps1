﻿$statusContent = Get-Content -Path "Docs\obsolete\status.json" | ConvertFrom-Json
$idsProperties = $statusContent.PSObject.Properties

$template = Get-Content -Path "Docs\obsolete\template.md" -Raw

$obsoleteApiSection = ""

foreach ($idProperty in $idsProperties)
{
    $id = $idProperty.Name
    $directory = "Docs\obsolete\$id"

    Write-Host "Generating info for $id..."

    $section = $template -replace '\$ID\$',"$id"

    $inLibrary = $idProperty.Value.InLibrary
    if ($inLibrary -eq $false)
    {
        $removedFromVersion = $idProperty.Value.RemovedFromVersion
        Write-Host "    removed by $removedFromVersion"
        $nl = [Environment]::NewLine
        $section = $section -replace '\$REMOVED\$',"> [!IMPORTANT]$nl> API removed from the library by $removedFromVersion release."
    }
    else
    {
        $section = $section -replace '\$REMOVED\$',""
    }

    $obsoleteFromVersion = $idProperty.Value.ObsoleteFromVersion
    $section = $section -replace '\$OBSOLETE_FROM_VERSION\$',"$obsoleteFromVersion"

    $description = Get-Content -Path "$directory\description.md" -Raw
    $section = $section -replace '\$DESCRIPTION\$',"$description"

    $oldApiContent = Get-Content -Path "$directory\old.md" -Raw
    $section = $section -replace '\$OLD_API\$',"$oldApiContent"

    $newApiContent = Get-Content -Path "$directory\new.md" -Raw
    $section = $section -replace '\$NEW_API\$',"$newApiContent"

    $obsoleteApiSection = $obsoleteApiSection + "`r`n`r`n$section"

    Write-Host "OK"
}

$overviewContent = Get-Content -Path "Docs\obsolete\obsolete.md" -Raw
$overviewContent = $overviewContent -replace '\$OBSOLETE_API\$',"$obsoleteApiSection"
Set-Content -Path "Docs\obsolete\obsolete.md" -Value $overviewContent.Trim() -NoNewline