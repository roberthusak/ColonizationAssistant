module ColonizationAssistant.SaveFormat

open System.IO
open System.Text

let roundPos = 0x1e     // TODO: Check the dates after 1600 and 1700

let difficultyPos = 0x36

let nationsStart = 0x9e
let nationExplorerLength = 0x18
let nationCountryOffset = nationExplorerLength
let nationCountryLength = 0x18                      // TODO: Guessed, better check
let nationAiOffset = 0x31
let nationLength = 0x34

let encoding = Encoding.ASCII

let parseBool (value:byte) =
    if value = 0uy then false else true

let parseCString (contents:byte[]) start maxLength =
    // Handle \0
    let maxLength =
        Seq.tryFind (fun i -> contents.[start + i] = 0uy) (seq { 0 .. maxLength - 1 })
        |> Option.defaultValue maxLength

    encoding.GetString(contents, start, maxLength)
    
let parseNationInfo contents index =
    let nationStart = nationsStart + (index * nationLength)
    {
        Nation = enum index;
        Explorer = parseCString contents nationStart nationExplorerLength;
        Country = parseCString contents (nationStart + nationCountryOffset) nationCountryLength;
        AI = parseBool contents.[nationStart + nationAiOffset];
    }
    
let parseSavedGame (filename:string) (contents:byte[]) =
    let round = contents.[roundPos] |> int
    let (difficulty:Difficulty) = contents.[difficultyPos] |> int |> enum

    // TODO: Consider support for AI-only or multiplayer in case of .SAV hacking
    let nationInfo =
        [0..3]
        |> List.map (parseNationInfo contents)
        |> List.where (fun n -> not n.AI)
        |> List.exactlyOne

    {
        Filename = Path.GetFileName(filename);
        Difficulty = difficulty;
        Round = round;
        NationInfo = nationInfo;
    }
