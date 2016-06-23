namespace FsWeb.Repositories

module GuitarRepository =

open Raven.Client.Document
open FsWeb.Models
open System

let store = new DocumentStore(DefaultDatabase = "Guitars", Url = "http://localhost:8080" ) ;
let myStore = store.Initialize()

type GuitarsRepository() =
    interface IDisposable with
        member x.Dispose() = printfn "Disposed"
    member x.getAllQueryable() =
        use session = myStore.OpenSession()
        query { for guitar in session.Query<Guitar>() do
                select guitar }           
    member x.GetAll() = 
        use session = myStore.OpenSession()
        query { for guitar in session.Query<Guitar>() do
                select guitar }           
        |> Seq.toList
    member x.GetByName name =
        use session = myStore.OpenSession()
        query { for g in session.Query<Guitar>() do
                where (g.Name = name)
                select g}
        |> Seq.toList
    member x.GetTop recordCount =
        use session = myStore.OpenSession()
        query { for g in session.Query<Guitar>() do
                select g
                take recordCount}
        |> Seq.toList

