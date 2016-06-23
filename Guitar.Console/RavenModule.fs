module RavenModule

open Raven.Client.Document
open FsWeb.Models

let store = new DocumentStore(DefaultDatabase = "Guitars", Url = "http://localhost:8080" ) ;
let myStore = store.Initialize()


let saveGuitar guitar =
    use session = myStore.OpenSession()
    session.Store guitar
    session.SaveChanges()
    
    