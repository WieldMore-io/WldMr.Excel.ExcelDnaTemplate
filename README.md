### Build status

[![Build Status](https://dev.azure.com/WldMr/WieldMore.io/_apis/build/status/WieldMore-io.WldMr.Excel.ExcelDnaTemplate?branchName=master)](https://dev.azure.com/WldMr/WieldMore.io/_build/latest?definitionId=16&branchName=master)

# ExclDNA F# 'dotnet new' template


## useful
### tools to check the template
`dotnet tool install --global sayedha.template.command`

`templates analyze -f .\working\templates\`

### testing the template
`dotnet new -i .\working\templates`

`dotnet new -u .\working\templates`

`dotnet new exceldna-addin -A py -D "a test" -F net472`

### buliding the nupkg
`dotnet pack`

