namespace ColonizationAssistant

type Difficulty = Discoverer = 0 | Explorer = 1 | Conquistador = 2 | Governor = 3 | Viceroy = 4

type Nation = English = 0 | French = 1 | Spanish = 2 | Dutch = 3

type NationInfo = { Nation: Nation; Explorer: string; Country: string; AI: bool }

type Season = Spring = 0 | Summer = 1 | Autumn = 2 | Winter = 4

type Date = { Season: Season; Year: int }

module Date =
    let private startYear = 1492
    let private doublingFirstYear = 1600
    let private quadruplingFirstYear = 1700

    let fromRound round =
        if round < (doublingFirstYear - startYear) then
            { Season = Season.Spring; Year = round + startYear }
        else if round < (doublingFirstYear - startYear + 200) then
            let roundOffset = round - (doublingFirstYear - startYear)
            let season =
                match roundOffset % 2 with
                | 0 -> Season.Spring
                | 1 -> Season.Autumn
                | _ -> failwith "Unreachable"
            let year = doublingFirstYear + (roundOffset / 2)
            { Season = season; Year = year }
        else
            let roundOffset = round - (doublingFirstYear - startYear + 200)
            let season =
                match roundOffset % 4 with
                | 0 -> Season.Spring
                | 1 -> Season.Summer
                | 2 -> Season.Autumn
                | 3 -> Season.Winter
                | _ -> failwith "Unreachable"
            let year = quadruplingFirstYear + (roundOffset / 4)
            { Season = season; Year = year }

type Position = { X: int; Y: int }

type Colony =
    {
        Name: string;
        Nation: Nation;
        Position: Position;
        Population: int;
    }

type GameState =
    {
        Filename: string;
        Difficulty: Difficulty;
        Round: int;
        Date: Date;
        NationInfo: NationInfo;
        Colonies: Colony[];
    }
    with

    member this.GameText =
        sprintf "%A %s of the %A, %A %d" this.Difficulty this.NationInfo.Explorer this.NationInfo.Nation this.Date.Season this.Date.Year
