open System

let data =
    System.IO.File.ReadAllLines "./data/data.txt"

module String =
    let split (delimeter: string) (str: string) = str.Split(delimeter)

    let trim (str: string) = str.Trim()

    let isNullOrWhiteSpace (str: string) = String.IsNullOrWhiteSpace(str)

module Queue =
    /// The queue we will use to draw
    let create numString =
        numString |> String.split "," |> Array.map int

    /// Returns (next * queue)
    let pop (queue: int []) =
        queue |> Array.head, queue |> Array.skip 1

type Board = Board of (int * bool) [] []

module Board =
    /// Create an array of boards
    let createMany (numRows: string []) =
        numRows
        |> Array.skipWhile String.isNullOrWhiteSpace
        |> Array.chunkBySize 6 // this should include a trailing blank line
        |> Array.map (fun boardWithTrailing ->
            boardWithTrailing
            |> Array.map String.trim // ensure white space is removed
            |> Array.except [ String.Empty ] // remove empty line
            |> Array.map (fun row ->
                row.Split()
                |> Array.except [| String.Empty |]
                |> Array.map int) // map to int []
            |> Array.map (Array.map (fun i -> (i, false))) // add bools
            |> Board)

    let markNum (num: int) (Board board) =
        board
        |> Array.map (fun row ->
            row
            |> Array.map (fun (thisNum, marked) ->
                if thisNum = num then
                    (thisNum, true) // mark true
                else
                    (thisNum, marked)))
        |> Board

    let calculateScore (num: int) (Board board) =
        board
        |> Array.reduce Array.append // flatten
        |> Array.sumBy (fun (thisNum, isMarked) -> if isMarked then 0 else num * thisNum)

    let checkWin (Board board) =
        let boardChecks = board |> Array.map (Array.map snd)

        let horizontalWin (board': bool [] []) =
            board'
            |> Array.map (Array.contains false >> not) // if a row is all true
            |> Array.contains true // if one row is true

        let verticalWin (board': bool [] []) =
            board'
            |> Array.transpose
            // check for horizontal
            |> horizontalWin

        boardChecks |> horizontalWin
        || boardChecks |> verticalWin


let rec continueGame (queue: int []) (boards: Board []) =
    match boards with
    | [||] -> ()
    | boards ->
        let num, newQueue = Queue.pop queue
        let newBoards = boards |> Array.map (Board.markNum num)

        newBoards
        |> Array.map (fun board ->
            let win = Board.checkWin board

            if win then
                Board.calculateScore num board |> printfn "%i"

            board, win)
        |> Array.filter (fun (_, win) -> not win)
        |> Array.map fst
        |> continueGame newQueue

let initQueue = Queue.create (data |> Array.head)
let initBoards = Board.createMany (data |> Array.skip 1)
continueGame initQueue initBoards
