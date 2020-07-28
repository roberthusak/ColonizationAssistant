// MainWindow.fs
namespace ColonizationAssistant

open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml

type MainWindow (backupPath:string) as this =
    inherit Window()

    do
        this.InitializeComponent()
#if DEBUG
        this.AttachDevTools()
#endif

    member this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)
        this.Refresh()

    member this.Refresh() =
        this.DataContext <-
            SaveManagement.loadSavedGames backupPath
            |> Array.filter (fun (_, contents) -> contents.Length > 0)
            |> Array.map (Utils.uncurry2 SaveFormat.parseSavedGame)
