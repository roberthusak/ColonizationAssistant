namespace ColonizationAssistant

open Avalonia.Data.Converters
open Avalonia.Media

type CellToScreenConverter() =
    
    static member Instance = CellToScreenConverter()

    interface IValueConverter with
        member this.Convert(value, targetType, parameter, culture) =
            upcast (value :?> int |> (*) 32)

        member this.ConvertBack(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            raise (System.NotSupportedException())

type NationToColorConverter() =

    static member Instance = NationToColorConverter()

    interface IValueConverter with
        member this.Convert(value, targetType, parameter, culture) =
            let color =
                match value :?> Nation with
                | Nation.English -> Brushes.Red
                | Nation.French -> Brushes.Blue
                | Nation.Spanish -> Brushes.Yellow
                | Nation.Dutch -> Brushes.Orange
                | _ -> failwith "Unreachable"
            upcast color

        member this.ConvertBack(value: obj, targetType: System.Type, parameter: obj, culture: System.Globalization.CultureInfo): obj = 
            raise (System.NotSupportedException())
