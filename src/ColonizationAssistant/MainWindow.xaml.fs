// MainWindow.fs
namespace ColonizationAssistant

open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml

type MainWindow () as this =
    inherit Window()

    do
        this.InitializeComponent()
#if DEBUG
        this.AttachDevTools()
#endif

    member this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)
