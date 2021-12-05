<Query Kind="Statements" />

// See https://aka.ms/new-console-template for more information


using var input = new StreamReader(File.OpenRead(@"C:\code\github\graemefoster\AdventOfCode\src\AdventOfCode\AdventOfCodeDay4\Input.txt"));
var numberSequence = input.ReadLine().Split(',').Select(x => int.Parse(x)).ToArray();
var boards = new List<BingoBoard>();
do
{
	input.ReadLine();
	boards.Add(BingoBoard.Parse(
		boards.Count + 1,
		input.ReadLine()!,
		input.ReadLine()!,
		input.ReadLine()!,
		input.ReadLine()!,
		input.ReadLine()!
	));
} while (!input.EndOfStream);

Console.WriteLine($"Loaded {boards.Count()} boards");

var boardsStillPlaying = boards.ToList();
for (var idx = 1; idx <= numberSequence.Length; idx++)
{
	foreach (var board in boards.Where(b => !b.IsBingo))
	{
		var markedBoard = board.Bingo(numberSequence[..idx]);
		if (board.IsBingo)
		{
			boardsStillPlaying.Remove(board);
			if (board.BoardNumber == 62)
			{
				int i = 0;
			}
			var unmarked = board.Unmarked(numberSequence[..idx]);
			var puzzleAnswer = unmarked.Sum(u => u.number) * numberSequence[idx - 1];
			markedBoard.Dump($"Board in after {idx} numbers! Board {board.BoardNumber}. Puzzle answer:{puzzleAnswer}");
		}
	}
}


class BingoBoard
{
	private (int col, int row, int number)[] _cells;
	public int BoardNumber { get;}
	public bool IsBingo {get; private set;}

	public BingoBoard(int boardNumber, int[][] lines)
	{
		_cells = lines.SelectMany((row, rowIdx) => row.Select((cell, colIdx) => (colIdx, rowIdx, cell))).ToArray();
		BoardNumber = boardNumber;
	}

	public static BingoBoard Parse(int boardNumber, params string[] lines)
	{
		return new BingoBoard(boardNumber, lines.Select(line => line.Split(' ').Where(x => x.Trim() != "").Select(num => int.Parse(num.Trim())).ToArray()).ToArray());
	}

	public IEnumerable<(int col, int row, int number)> Bingo(int[] calledNumbers)
	{
		var marked = calledNumbers.SelectMany(n => _cells.Where(cell => cell.number == n));

		var bingoRow = new[] { 0, 1, 2, 3, 4 }.Select(row => marked.Count(markedNumber => markedNumber.row == row) == 5).Any(x => x);
		var bingoCol = new[] { 0, 1, 2, 3, 4 }.Select(column => marked.Count(markedNumber => markedNumber.col == column) == 5).Any(x => x);

		IsBingo = bingoRow || bingoCol;
		return marked;
	}

	public IEnumerable<(int col, int row, int number)> Unmarked(int[] calledNumbers)
	{
		return _cells.Except(Bingo(calledNumbers));
	}

}