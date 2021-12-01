let data = System.IO.File.ReadAllLines "data/data.txt" 

data
|> Array.map int
|> Array.pairwise
|> Array.filter (fun (x,y) -> y > x)
|> Array.length
|> printfn "Day 1 Result Part 1: %i"

data
|> Array.map int
|> Array.windowed 3
|> Array.map Array.sum
|> Array.pairwise
|> Array.filter (fun (x,y) -> y > x)
|> Array.length
|> printfn "Day 1 Result Part 2: %i"