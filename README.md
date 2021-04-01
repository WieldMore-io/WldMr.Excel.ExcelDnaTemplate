### Build status

[![Build Status](https://dev.azure.com/WldMr/WieldMore.io/_apis/build/status/WieldMore-io.WldMr.Excel.ExcelDnaTemplate?branchName=master)](https://dev.azure.com/WldMr/WieldMore.io/_build/latest?definitionId=16&branchName=master)

# ExclDNA F# 'dotnet new' template

## General
The template include an `.fsx` script that setups the debug settings.
The script uses a shim that only detects Excel 365 64-bits for now.
It only need to be run once, and will generate `properties/launchSettings.json`

## useful tools for dev
### tool to check the template
`dotnet tool install --global sayedha.template.command`

`templates analyze -f .\working\templates\`

### testing the template
install the local template
`dotnet new -i .\working\templates`

uninstall it
`dotnet new -u .\working\templates`

`dotnet new exceldna-addin -A py -D "a test" -F net472`

### buliding the nupkg
`dotnet pack`


### testing the nupkg
install the template from the nupkg
`dotnet new -i .\bin\Debug\WldMr.Excel.ExcelDnaTemplate.0.0.1.nupkg`
check it is installed
`dotnet new list -lang 'F#'`
use it 
`dotnet new exceldna-addin -A py -D "a test" -F net472`
or use Visual Studio "New project"
uninstall it 
`dotnet new -u WldMr.Excel.ExcelDnaTemplate`



