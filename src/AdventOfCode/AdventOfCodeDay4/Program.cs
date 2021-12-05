// See https://aka.ms/new-console-template for more information


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using var input = new StreamReader(File.OpenRead("Input-Sample.txt"));
var line = default(string);
var numberSequence = input.ReadLine().Split(',').Select(x => int.Parse(x)).ToArray();
var boards = new List<BingoBoard>();
do
{
    input.ReadLine();
    boards.Add(BingoBoard.Parse(
        input.ReadLine()!,
        input.ReadLine()!,
        input.ReadLine()!,
        input.ReadLine()!,
        input.ReadLine()!
    ));
} while (!input.EndOfStream);

Console.WriteLine($"Loaded {boards.Count()} boards");

for (var idx = 1; idx <= numberSequence.Length; idx++)
{
    foreach (var board in boards)
    {
        if (board.Bingo(numberSequence[..idx]))
        {
            Console.WriteLine("BINGO!");
        }
    }
}

class BingoBoard
{
    private int[][] _lines;
    private List<(int, int)> _marked;

    public BingoBoard(int[][] lines)
    {
        _lines = lines;
    }

    public static BingoBoard Parse(params string[] lines)
    {
        return new BingoBoard(lines.Select(line => line.Split(' ').Where(x => x.Trim() != "").Select(num => int.Parse(num.Trim())).ToArray()).ToArray());
    }

    public bool Bingo(int[] calledNumbers)
    {
        var marked =
            calledNumbers.SelectMany(calledNumber =>
                _lines
                    .SelectMany((line, idx1) =>
                        line.Select((num, idx2) =>
                            num == calledNumber ? (idx1, idx2) : (int.MinValue, int.MinValue))
                            .Where(x => x.Item1 != int.MinValue)
                            .ToArray())).ToArray();

        return calledNumbers.Length > 4 && new[] { 0, 1, 2, 3, 4 }.Select(x =>
                marked.All(markedNumber => markedNumber.Item1 == x || markedNumber.Item2 == x))
            .Any(x => x);
        
        return false;
    }
}