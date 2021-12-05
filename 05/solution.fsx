let data =
    System.IO.File.ReadAllLines "./data/data.txt"

module String =
    let split (delimeter: string) (str: string) = str.Split(delimeter)

    let splitToTuple (delimeter: string) (str: string) =
        str.Split(delimeter)
        |> fun arr -> arr.[0], arr.[1]

    let trim (str: string) = str.Trim()

type Point = Point of int * int

type Segment = Segment of Point * Point

let log fn =
    let res = fn
    printfn "%A" res
    res

let parsePoint =
    String.splitToTuple ","
    >> (fun (x, y) -> int x, int y)
    >> Point

let parseSegment =
    String.splitToTuple "->"
    >> (fun (begin', end') -> parsePoint begin', parsePoint end')
    >> Segment

let numsBetween a b =
    if a < b then
        seq { a .. b }
    else
        seq { b .. a } |> Seq.sortDescending
    |> Array.ofSeq

let (|Horizontal|_|) (Segment (Point (x1, y1), Point (x2, y2))) =
    if y1 = y2 then
        (x1, x2)
        ||> numsBetween
        |> Array.map (fun x -> x, y1)
        |> Some
    else
        None

let (|Vertical|_|) (Segment (Point (x1, y1), Point (x2, y2))) =
    if x1 = x2 then
        (y1, y2)
        ||> numsBetween
        |> Array.map (fun y -> x1, y)
        |> Some
    else
        None

let sortTwo a b = if a < b then a, b else b, a

let (|Diagonal|_|) (Segment (Point (x1, y1), Point (x2, y2))) =
    let a = System.Math.Abs(x1 - x2)
    let b = System.Math.Abs(y1 - y2)

    if a = b then
        seq {
            let xOp = if x1 < x2 then (+) else (-)
            let yOp = if y1 < y2 then (+) else (-)
            for i in 0 .. a -> (xOp x1 i, yOp y1 i)
        }
        |> Array.ofSeq
        |> Some
    else
        None

let parseCoordinates (segment: Segment) =
    match segment with
    | Horizontal points
    | Vertical points
    | Diagonal points -> points
    | _ -> Array.empty

let countOverlappingPoints =
    Array.collect parseCoordinates
    >> Array.groupBy id
    >> Array.map (fun (grp, points) -> grp, points.Length)
    >> Array.filter (fun (_, count) -> count > 1)
    >> Array.map fst
    >> Array.length

let segments = data |> Array.map parseSegment

segments
|> countOverlappingPoints
|> printfn "Number of overlapping points: %i"
