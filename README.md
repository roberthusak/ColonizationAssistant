# ColonizationAssistant
A tool to assist with the gameplay of Sid Meier's Colonization

## Features
Keep *ColonizationAssistant* running in the background while you play. Each time you save normally within the game, *ColonizationAssistant* will create a revision in a directory you specify. Every SAV file gets it's own set of revisions, which means you can confidently keep re-using the same save game slot. You also now have no real limit on the number of saves, instead of only having the eight saves allowed by the game.

In addition, *ColonizationAssistant* shows provides a GUI for navigating the backups. It will visualize the data in each save by showing a map of your colonies when you select each file.

## Usage

*ColonizationAssistant* is written in F#, so you will need [dotnet SDK](https://dotnet.microsoft.com/download). Type `dotnet --info` into your terminal to confirm installation. In  to confirming that `dotnet` works, you'll need to be familiar with these three directories:

1. **REPO**: The directory where you download this repository. For example: "C:\Users\USER\Documents\GitHub\ColonizationAssistant"
1. **COLONIZE**: You'll need to locate the Colonization Save Directory. If you are using GOG you can go to "Manage Installation" and then "Show Folder." For Steam, you can go to "Properties" and then "Local Files." You are looking for a ../COLONIZE/ directory and it should have COLONY00.SAV - COLONY09.SAV (generally just those ten files).
1. **BACKUP**: You can pick any directory you want for the backups, but you do need to create it first.

When you are ready, you can start *ColonizationAssistant* this way:

dotnet run --project **REPO**/src/ColonizationAssistant/ColonizationAssistant.fsproj **COLONIZE** **BACKUP**
