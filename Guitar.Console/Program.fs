// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open RavenModule
open FsWeb.Models
open FsWeb.Repositories.GuitarRepository

[<EntryPoint>]
let main argv = 
    let guitar1 = new Guitar( Name = "Qui")
    let guitar2 = new Guitar( Name = "Quo")
    let guitar3 = new Guitar( Name = "Qua")
    saveGuitar guitar1
    saveGuitar guitar2
    saveGuitar guitar3

    let repo = new GuitarsRepository()
    0 // return an integer exit code
