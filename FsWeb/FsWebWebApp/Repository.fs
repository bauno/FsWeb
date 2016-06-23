namespace FsWeb.Repositories

module Repository =
    open System.Linq
    open CacheAgent

    let withCacheKeyOf key = Some key

    let doNotUseCache = None
    
    let get (source: IQueryable<_>) queryFn  cacheKey =
        let cacheResult =
            match cacheKey with
            | Some key -> CacheAgent.get<'a list> key
            | None -> None
        match cacheResult, cacheKey with
        | Some result, _ -> result
        | None, Some cacheKey ->
            let result = queryFn source |> Seq.toList
            CacheAgent.set cacheKey cacheResult
            result
        | _, _ -> queryFn source |> Seq.toList
        

    let getAll() =
        fun s -> query {for x in s do select x}

    let find filterPredFn =
        filterPredFn
        |> fun fn s -> query {for x in s do where (fn()) }

    let getTop recordCount =
        recordCount
        |> fun cnt s -> query {for x in s do take cnt}
