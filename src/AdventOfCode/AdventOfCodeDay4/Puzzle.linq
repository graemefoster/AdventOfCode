<Query Kind="Statements" />

// See https://aka.ms/new-console-template for more information


using var input = new StreamReader(File.OpenRead(@"C:\code\github\graemefoster\AdventOfCode\src\AdventOfCode\AdventOfCodeDay4\Input.txt"));
var numberSequence = input.ReadLine().Split(',').Select(x => int.Parse(x)).ToArray();
var boards = BuildBoards(input);

var bingo = new List<(int boardNumber, (int col, int row, int number)[] board)>();
for (var idx = 1; idx <= numberSequence.Length; idx++)
{
	foreach(var board in boards.Except(bingo)) 
	{
		var result = IsBingo(board.board, numberSequence[..idx]);
		if (result.bingo)
		{
			(result.unmarked.Sum(num => num) * numberSequence[idx - 1]).Dump($"BINGO board {board.boardNumber}!");
			bingo.Add(board);
			break;
		}
	}
}

for (var idx = 1; idx <= numberSequence.Length; idx++)
{
	bool keepGoing = true;
	foreach (var board in boards)
	{
		var result = IsBingo(board.board, numberSequence[..idx]);
		if (result.bingo)
		{
			(result.unmarked.Sum(num => num) * numberSequence[idx - 1]).Dump("Puzzle 1");
			keepGoing = false;
			break;
		}
	}
	if (!keepGoing) break;
}


List<(int boardNumber, (int col, int row, int number)[] board)> BuildBoards(StreamReader reader)
{
	var boards = new List<(int, (int col, int row, int number)[])>();
	do
	{
		input.ReadLine();
		boards.Add((boards.Count + 1, BuildBoard(input)));
	} while (!input.EndOfStream);
	return boards;
}

(int col, int row, int number)[] BuildBoard(StreamReader reader) 
{
	return Enumerable.Range(1,5)
	.Select(e => input.ReadLine()!)
	.SelectMany((row, rowIdx) => row.Split(' ').Where(r => r.Trim() != "").Select((cell, colIdx) => (col: colIdx, row: rowIdx, int.Parse(cell.Trim())))).ToArray();
		
}

(bool bingo, IEnumerable<int> unmarked) IsBingo((int col, int row, int number)[] cells, int[] calledNumbers) 
{
	var marked = calledNumbers.SelectMany(n => cells.Where(cell => cell.number == n));
	var bingoRow = new[] { 0, 1, 2, 3, 4 }.Select(row => marked.Count(markedNumber => markedNumber.row == row) == 5).Any(x => x);
	var bingoCol = new[] { 0, 1, 2, 3, 4 }.Select(column => marked.Count(markedNumber => markedNumber.col == column) == 5).Any(x => x);

	return (bingoRow || bingoCol, (cells.Where(c => !calledNumbers.Contains(c.number)).Select(c => c.number)));
}

