Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$artifactsFolder = Join-Path $PSScriptRoot "artifacts"
$tempFolder = Join-Path $PSScriptRoot "temp"
$sourceFolder = Join-Path $PSScriptRoot "src"

function CreateEmptyFolder($path) {
    if (Test-Path $path) {
        Remove-Item $path -recurse -Force
        Start-Sleep -milliseconds 100
    }
    New-Item $path -type directory
}

CreateEmptyFolder $tempFolder
CreateEmptyFolder $artifactsFolder

Write-Host "Getting edge.js nuget package"
& "$PSScriptRoot\tools\nuget.exe" install "Edge.js" -OutputDirectory "$tempFolder"
$edgeFolder = (Get-ChildItem -Path $tempFolder -Filter "Edge*").FullName
Copy-Item "$edgeFolder\content\edge\" "$tempFolder\edge\" -Recurse
Copy-Item "$edgeFolder\lib\*.dll" "$tempFolder\" -Recurse
Remove-Item $edgeFolder -recurse -Force

Write-Host "Installing NPM modules"
& npm install --prefix "$tempFolder" highlight.js@"^8.6.0"
& npm install --prefix "$tempFolder" markdown-it@"^4.3.0"
& npm install --prefix "$tempFolder" markdown-it-container@"^1.0.0"
& npm install --prefix "$tempFolder" markdown-it-emoji@"^1.0.0"
& npm install --prefix "$tempFolder" markdown-it-footnote@"^1.0.0"
& npm install --prefix "$tempFolder" markdown-it-sup@"^1.0.0"
Remove-Item "$tempFolder\etc" #remove npm artifact

Copy-Item "$sourceFolder\MarkdownItEngine.csx" $tempFolder

Start-Sleep -milliseconds 100
Add-Type -assembly "system.io.compression.filesystem"
[io.compression.zipfile]::CreateFromDirectory("$tempFolder", "$artifactsFolder\MarkdownItEngine.zip")

Remove-Item $tempFolder -recurse
