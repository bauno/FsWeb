namespace FsWeb.Models

open System

type Guitar() =
    member val Id = Guid.NewGuid() with get, set
    member val Name = "" with get,set