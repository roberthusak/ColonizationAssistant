module ColonizationAssistant.SaveFormat

open System.IO
open System.Text

let roundPos = 0x1e

let colonyCountPos = 0x2e

let difficultyPos = 0x36

let nationsStart = 0x9e
let nationExplorerLength = 0x18
let nationCountryOffset = nationExplorerLength
let nationCountryLength = 0x18                      // TODO: Guessed, better check
let nationAiOffset = 0x31
let nationLength = 0x34

let coloniesStart = 0x186
let colonyPositionOffset = 0x0
let colonyNameOffset = 0x2
let colonyNameLength = 0x16
let colonyNationOffset = 0x1a
let colonyPopulationOffset = 0x1f
let colonyLength = 0xca

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

let parsePosition (contents:byte[]) index =
    let x = contents.[index] |> int
    let y = contents.[index + 1] |> int
    { X = x; Y = y }

let parseColony contents index =
    let colonyStart = coloniesStart + (index * colonyLength)
    let pos = parsePosition contents (colonyStart + colonyPositionOffset)
    let name = parseCString contents (colonyStart + colonyNameOffset) colonyNameLength
    let (nation:Nation) = contents.[colonyStart + colonyNationOffset] |> int |> enum
    let population = contents.[colonyStart + colonyPopulationOffset] |> int
    {
        Name = name;
        Nation = nation;
        Position = pos;
        Population = population;
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

    let colonyCount = contents.[colonyCountPos] |> int
    let colonies =
        [0 .. colonyCount - 1]
        |> List.map (parseColony contents)
        |> List.toArray

    {
        Filename = Path.GetFileName(filename);
        Difficulty = difficulty;
        Round = round;
        Date = Date.fromRound round;
        NationInfo = nationInfo;
        Colonies = colonies;
    }
