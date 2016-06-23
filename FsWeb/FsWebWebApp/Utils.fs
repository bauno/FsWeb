module Utils

let NullCheck = function
    | v when v <> null -> Some v
    | _ -> None
