let data = System.IO.File.ReadAllLines "./data/data.txt" 

let [<Literal>] Forward = "forward"
let [<Literal>] Down = "down"
let [<Literal>] Up = "up"

data
// split the instruction from the quantity
|> Array.map (fun instn -> instn.Split(' '))
// convert to tuples
|> Array.map (fun instnArr -> instnArr.[0], (instnArr.[1] |> int))
// accumulate
|> Array.fold (fun (horPos, depth) ele ->  
    match ele with
    | Down, qnty -> (horPos, depth + qnty)
    | Up, qnty -> (horPos, depth - qnty)
    | Forward, qnty -> (horPos + qnty, depth)
    | _ -> (horPos, depth)
    ) (0, 0)
|> fun (horPos, depth) -> horPos * depth
|> printfn "Day 2 Part 1 Solution: %i"

//-------------------------------------------------------------------------------------

data
// split the instruction from the quantity
|> Array.map (fun instn -> instn.Split(' '))
// convert to tuples
|> Array.map (fun instnArr -> instnArr.[0], (instnArr.[1] |> int))
// accumulate
|> Array.fold (fun (horPos, depth, aim) ele ->  
    match ele with
    | Down, qnty -> (horPos, depth, aim + qnty)
    | Up, qnty -> (horPos, depth, aim - qnty)
    | Forward, qnty -> (horPos + qnty, depth + aim * qnty, aim)
    | _ -> (horPos, depth, aim)
    ) (0, 0, 0)
|> fun (horPos, depth, _) -> horPos * depth
|> printfn "Day 2 Part 2 Solution: %i"
