param(
    [string]$Configuration = "Release"
)
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$zipFilePath = Join-Path $PSScriptRoot "Pretzel.MarkdownItEngine.zip"

# create ZIP archive
Add-Type -Assembly "System.IO.Compression.Filesystem"
if (Test-Path $zipFilePath) {
    Remove-Item $zipFilePath
}
[System.IO.Compression.ZipFile]::CreateFromDirectory("$PSScriptRoot\src\Pretzel.MarkdownIt\bin\$Configuration", $zipFilePath)

# remove unneeded files from archive
$zipArchive = [System.IO.Compression.ZipFile]::Open($zipFilePath, [System.IO.Compression.ZipArchiveMode]::Update)
$entriesToDelete = $zipArchive.Entries | Where-Object { $_.FullName -eq "Pretzel.MarkdownIt.dll.config" -or $_.FullName -eq "Pretzel.MarkdownIt.pdb" } 
$entriesToDelete | % { $_.Delete() }
$zipArchive.Dispose()

