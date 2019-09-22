module ColonizationAssistant.SaveFormat

open System.IO

let parseSavedGame (filename:string) contents = { Filename = Path.GetFileName(filename) }
