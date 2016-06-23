module Repository
    open System.Linq
    
    let get (source: IQueryable<_>) queryFn =
        queryFn source |> Seq.toList

    let getAll() =
        fun s -> query {for x in s do select x}

    let find filterPredFn =
        filterPredFn
        |> fun fn s -> query {for x in s do where (fn()) }

    let getTop recordCount =
        recordCount
        |> fun cnt s -> query {for x in s do take cnt}
