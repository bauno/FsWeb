namespace FsWeb.Controllers
open System.Web.Mvc
open System
open FsWeb.Models
open FsWeb.Repositories


[<HandleError>]
type GuitarsController(context: IDisposable, ?repository) =
    inherit Controller()

    let asACtionResult result = result :> ActionResult

    let fromRepository = 
        match repository with
        | Some v -> v
        | _ -> (context :?> GuitarsRepository).getAllQueryable()
            |> Repository.get    
    new() = new GuitarsController(new GuitarsRepository())
    member this.Index () =
        // The sequence is hardcoded for example purposes
        // It will be switched out in a future example.
        Repository.getAll() 
        |> fromRepository
        |> this.View
    override x.Dispose disposing =
        context.Dispose()
        base.Dispose disposing
    [<HttpGet>]
    member this.Create() =
        this.View();
    [<HttpPost>]
    member this.Create(guitar: Guitar) : ActionResult =
        let isNameValid = Utils.NullCheck(guitar.Name).IsSome &&
                          not (guitar.Name.Contains("broken"))
        match base.ModelState.IsValid, isNameValid with
        | false,false| true,false| false,true -> 
            guitar |> this.View |> asACtionResult
        | _ -> upcast base.RedirectToAction("Index")
