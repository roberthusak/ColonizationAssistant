namespace ColonizationAssistant

open System
open System.IO
open Avalonia
open Avalonia.Logging.Serilog

type Program() =

    static let BuildAvaloniaApp() =
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToDebug()

    static let AppMain (app:Application) (argv:string[]) =
        if argv.Length < 2 || not (Directory.Exists(argv.[0])) || not (Directory.Exists(argv.[1])) then
            printfn "Expected arguments: [path to COLONIZE folder] [path to backup folder]"
        else
            SaveManagement.watchGameSaves argv.[0] argv.[1]
            app.Run(new MainWindow())

    [<EntryPoint>]
    static let Main argv =
        BuildAvaloniaApp()
            .Start(AppMain, argv)
        0
