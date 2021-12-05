let data = System.IO.File.ReadAllLines "./data/data.txt"
let dataInt = data |> Array.map (Seq.toArray >> Array.map (string >> int))

let count = data.Length
let length = data.[0].Length

let toDecimal bin = System.Convert.ToInt64(bin, 2)

let gamma =
    dataInt
    |> Array.fold (fun acc ele -> Array.map2 (+) acc ele) (Array.zeroCreate length)
    |> Array.map (fun sum -> if sum > (count/2) then '1' else '0')
    |> System.String

let epsilon =
    dataInt
    |> Array.fold (fun acc ele -> Array.map2 (+) acc ele) (Array.zeroCreate length)
    |> Array.map (fun sum -> if sum > (count/2) then '0' else '1')
    |> System.String

printfn $"gamma: %s{gamma}"
printfn $"epsilon: %s{epsilon}"

(gamma |> toDecimal) * (epsilon |> toDecimal)
|> printfn "Part 1 solution: %i"

let getCountForIndex (index:int) (bins: int[][]) =
    bins
    |> Array.map (fun bins -> bins.[index])
    |> Array.countBy id

let oxygenRating =
    let mutable index = 0
    let mutable rating = dataInt

    while rating.Length = 1 do
        let counts = rating |> getCountForIndex index
        let max = counts |> Array.map snd |> Array.max
        let candidates = counts |> Array.filter (fun (_, cnt) -> cnt = max) |> Array.map fst
        let keep = if candidates |> Array.contains 1 then 1 else 0
        
        rating <- 
            rating
            |> Array.filter (fun binary ->
                binary.[index] = keep)
        index <- index + 1        
    System.String.Join("", rating |> Array.head)

printfn "oxygen generator rating: %s" oxygenRating
    

let scrubRating =
    let mutable index = 0
    let mutable rating = dataInt

    while rating.Length = 1 do
        let counts = rating |> getCountForIndex index
        let min = counts |> Array.map snd |> Array.min
        let candidates = counts |> Array.filter (fun (_, cnt) -> cnt = min) |> Array.map fst
        let keep = if candidates |> Array.contains 0 then 0 else 1
        
        rating <- 
            rating
            |> Array.filter (fun binary ->
                binary.[index] = keep)
        index <- index + 1
    System.String.Join("", rating |> Array.head)

printfn "CO2 Scrubber Rating: %s" scrubRating

(scrubRating |> toDecimal) * (oxygenRating |> toDecimal)
|> printfn "Life Support Rating: %i"