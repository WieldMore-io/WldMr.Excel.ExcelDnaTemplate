
#r "nuget: FSharpPlus"
#r "nuget: System.Text.Json"  // can nuget be avoided without adding the reference to the project?

open System.IO
open FSharpPlus
open System.Text.Json
open Microsoft.Win32
open System.Runtime.InteropServices

type SCS =  
 | SCS_NONE = -1
 | SCS_32BIT_BINARY = 0
 | SCS_64BIT_BINARY = 6
 | SCS_DOS_BINARY = 1
 | SCS_OS216_BINARY = 5
 | SCS_PIF_BINARY = 3
 | SCS_POSIX_BINARY = 4
 | SCS_WOW_BINARY = 2

[<DllImport(@"kernel32.dll", CallingConvention = CallingConvention.Winapi)>]
extern bool GetBinaryType(string applicationName, SCS& binaryType)

type Arch = X64 | X86
module Arch =
  let toSuffix = function X64 -> "64" | _ -> ""

// TODO: FIX me
// DONE: You're fixed
let excelKey = "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\excel.exe"
let excelPath = Registry.LocalMachine.OpenSubKey(excelKey).GetValue("").ToString()

// TODO: improve me
// DONE: You're improved
let findArch = 
  let mutable scs : SCS = SCS.SCS_NONE
  GetBinaryType(foo, &scs) |> ignore
  match scs with
    | SCS.SCS_64BIT_BINARY -> Some(Arch.X64)
    | SCS.SCS_32BIT_BINARY -> Some(Arch.X86)
    | _ -> None

let launchSettingsContent excelPath projectName args =
  sprintf """{
  "profiles": {
    "%s": {
      "commandName": "Executable",
      "executablePath": "%s",
      "commandLineArgs": "/x %s"
    }
  }
}
""" projectName excelPath args 

let getPaths () =
  let projectPath = __SOURCE_DIRECTORY__ |> Path.GetDirectoryName
  let projectName = projectPath |> Path.GetFileName
  let excelPath = findExcel () |> Option.defaultValue "UNKNOWN_PATH_TO_EXCEL"
  let relativeXllPath = "\\bin\\Debug\\net461"
  let xllName = projectName + (excelPath |> findArch  |> Arch.toSuffix) + ".xll"
  {|
    ProjectPath = projectPath
    ProjectName = projectName
    ExcelPath = excelPath
    XllPath = projectPath + relativeXllPath + "\\" + xllName
  |}


let fileContent () = 
  let escapeString (s: string) = JsonEncodedText.Encode(s).ToString()
  let paths = getPaths()
  launchSettingsContent (escapeString paths.ExcelPath) (escapeString paths.ProjectName) (escapeString paths.XllPath)


let outputPath = """\Properties\launchSettings.json"""

let writeLaunchSettings () =
  let paths = getPaths ()
  paths.ProjectPath + "\\Properties" |> System.IO.Directory.CreateDirectory |> ignore
  let templateFullPath = paths.ProjectPath + outputPath
  printfn "Writing to %s" templateFullPath
  File.WriteAllText(templateFullPath, fileContent())
  "It seems the script terminated without error!" + "\n" +
    "The file should have been created at:" + "\n" +
    $"{templateFullPath}" |> printfn "%s"
