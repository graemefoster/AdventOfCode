using AdventOfCode;

var result = File.ReadAllLines("./input.txt")
    .Select(x => x.Split(' '))
    .Select(x => (direction: x[0], amount: int.Parse(x[1])))
    .Scan((horiz: 0, depth: 0), (val, next) => next.direction switch
        {
            "forward" => (val.horiz + next.amount, val.depth),
            "down" => (val.horiz, val.depth + next.amount),
            "up" => (val.horiz, val.depth - next.amount),
        }
    );

Console.WriteLine($"Puzzle 1: {result.depth * result.horiz}");

var result2 = File.ReadAllLines("./input.txt")
    .Select(x => x.Split(' '))
    .Select(x => (direction: x[0], amount: int.Parse(x[1])))
    .Scan((horiz: 0, depth: 0, aim:0), (val, next) => next.direction switch
        {
            "forward" => (val.horiz + next.amount, val.depth + (val.aim * next.amount), val.aim),
            "down" => (val.horiz, val.depth, val.aim + next.amount),
            "up" => (val.horiz, val.depth, val.aim - next.amount),
        }
    );

Console.WriteLine($"Puzzle 2: {result2.depth * result2.horiz}");
