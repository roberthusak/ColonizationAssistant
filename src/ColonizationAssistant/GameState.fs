namespace ColonizationAssistant

type Difficulty = Discoverer = 0 | Explorer = 1 | Conquistador = 2 | Governor = 3 | Viceroy = 4

type Nation = English = 0 | French = 1 | Spanish = 2 | Dutch = 3

type NationInfo = { Nation: Nation; Explorer: string; Country: string; AI: bool }

type GameState = { Filename: string; Difficulty: Difficulty; Round: int; NationInfo: NationInfo }
