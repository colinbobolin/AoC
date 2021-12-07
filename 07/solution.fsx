module Array =
    let median arr =
        let middle = (arr |> Array.length |> decimal) / 2m |> int
        let sorted = arr |> Array.sort
        sorted[middle]

let data =
    System.IO.File.ReadAllLines "./data/data.txt" 
    |> Array.head

let crabPos = data.Split(",") |> Array.map int

let median = crabPos |> Array.median

crabPos
|> Array.sumBy (fun pos -> pos - median |> System.Math.Abs)
|> printfn "%A"

let triangleNumber n = (n * (n + 1))/2

seq {
    for i in 1 .. crabPos.Length do
        crabPos
        |> Array.sumBy (fun pos -> 
            (pos - i) |> System.Math.Abs |> triangleNumber)
}
|> Seq.min
|> printfn "%A"


