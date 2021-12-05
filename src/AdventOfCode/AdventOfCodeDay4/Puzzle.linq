<Query Kind="Statements" />

using var input = new StreamReader(File.OpenRead(@"C:\code\github\graemefoster\AdventOfCode\src\AdventOfCode\AdventOfCodeDay4\Input.txt"));
var numberSequence = input.ReadLine().Split(',').Select(x => int.Parse(x)).ToArray();
var boards = BuildBoards(input);

var bingo = new List<(int BoardNumber, (int col, int row, int number)[] Board)>();

Enumerable.Range(1, numberSequence.Length)
	.Select(idx => new
	{
		NumbersCalled = idx,
		CalledNumber = numberSequence[idx - 1],
		BingoBoards = from board in boards.Where(b => !BoardResult(b.Board, numberSequence[..(idx - 1)]).Bingo)
					  let result = BoardResult(board.Board, numberSequence[..idx])
					  where result.Bingo
					  select new
					  {
						  BoardNumber = board.BoardNumber,
						  PuzzleAnswer = result.Unmarked.Sum() * numberSequence[idx - 1]
					  }
	})
	.Where(number => number.BingoBoards.Any()) //make output nicer
	.Dump();


List<(int BoardNumber, (int col, int row, int number)[] Board)> BuildBoards(StreamReader reader)
{
	var boards = new List<(int, (int col, int row, int number)[])>();
	do
	{
		input.ReadLine();
		boards.Add((boards.Count + 1, BuildBoard(input)));
	} while (!input.EndOfStream);
	return boards;
}

(int Col, int Row, int Number)[] BuildBoard(StreamReader reader)
{
	return Enumerable.Range(1, 5)
	.Select(e => input.ReadLine()!)
	.Where(e => !string.IsNullOrWhiteSpace(e))
	.SelectMany((row, rowIdx) =>
		row.Split(' ')
		.Where(r => r != "")
		.Select((cell, colIdx) => (col: colIdx, row: rowIdx, int.Parse(cell.Trim())))
	)
	.ToArray();

}

(bool Bingo, IEnumerable<int> Unmarked) BoardResult((int col, int row, int number)[] cells, int[] calledNumbers)
{
	var marked = calledNumbers.SelectMany(n => cells.Where(cell => cell.number == n));

	var bingoRow = new[] { 0, 1, 2, 3, 4 }.Select(row => marked.Count(markedNumber => markedNumber.row == row) == 5).Any(x => x);
	var bingoCol = new[] { 0, 1, 2, 3, 4 }.Select(column => marked.Count(markedNumber => markedNumber.col == column) == 5).Any(x => x);

	return (bingoRow || bingoCol, (cells.Where(c => !calledNumbers.Contains(c.number)).Select(c => c.number)));
}

