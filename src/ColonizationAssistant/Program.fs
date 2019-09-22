namespace ColonizationAssistant

open System
open Avalonia
open Avalonia.Logging.Serilog

type Program() =

    static let BuildAvaloniaApp() =
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToDebug()

    static let AppMain (app:Application) argv = app.Run(new MainWindow())

    [<EntryPoint>]
    static let Main argv =
        BuildAvaloniaApp()
            .Start(AppMain, argv)
        0
