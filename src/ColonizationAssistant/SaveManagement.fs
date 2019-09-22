module ColonizationAssistant.SaveManagement

open System.IO

let backupSavedGame (savePath:string) backupPath =
    let saveName = Path.GetFileNameWithoutExtension(savePath)

    let lastRevisionPathOpt =
        let revisions = Directory.GetFiles(backupPath, saveName + ".*.SAV")
        if revisions.Length = 0 then
            None
        else
            revisions
            |> Array.max
            |> Option.Some

    let nextRevisionNumber =
        match lastRevisionPathOpt with
        | Some lastRevisionPath ->
            Path.GetFileNameWithoutExtension(lastRevisionPath).Split('.').[1]
            |> int
            |> (+) 1
        | None ->
            0
    
    // Solves the problem with FileSystemWatcher - it fires the Changed event twice after a file creation
    if Option.isNone lastRevisionPathOpt || File.GetLastWriteTime(savePath) <> File.GetLastWriteTime(Option.get lastRevisionPathOpt) then
        let trgFile = Path.Combine(backupPath, sprintf "%s.%04d.SAV" saveName nextRevisionNumber)
        File.Copy(savePath, trgFile)


let watchGameSaves gamePath backupPath =
    // TODO: Handle its lifetime (disposal)
    let watcher = new FileSystemWatcher()
    watcher.Path <- gamePath
    watcher.Filter <- "*.SAV"
    watcher.EnableRaisingEvents <- true

    watcher.Changed.Add(fun e -> backupSavedGame e.FullPath backupPath)
    ()

let loadSavedGames backupPath =
    Directory.GetFiles(backupPath, "*.SAV")
    |> Array.map (fun filename -> (filename, File.ReadAllBytes(filename)))
