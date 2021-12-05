<Query Kind="Statements" />

using var input = new StreamReader(File.OpenRead(@"C:\code\github\graemefoster\AdventOfCode\src\AdventOfCode\AdventOfCodeDay4\Input.txt"));
var numberSequence = input.ReadLine().Split(',').Select(x => int.Parse(x)).ToArray();
var boards = BuildBoards(input);

var bingo = new List<(int boardNumber, (int col, int row, int number)[] board)>();

Enumerable.Range(1, numberSequence.Length)
	.SelectMany(idx => (
		from board in boards.Where(b => !IsBingo(b.board, numberSequence[..(idx - 1)]).bingo)
		let bingo = IsBingo(board.board, numberSequence[..idx])
		where bingo.bingo
		select (boardNumber: board.boardNumber, lastNumber: numberSequence[idx - 1], answer: bingo.unmarked.Sum() * numberSequence[idx - 1]))
		)
		.Dump();


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
	return Enumerable.Range(1, 5)
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

