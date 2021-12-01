using AdventOfCode;
var lines = File.ReadAllLines("./input.txt").Select(x => (int?) int.Parse(x));

Console.WriteLine(lines.SlidingWindow(2, a => a[0] < a[1]).Count(x => x));

Console.WriteLine(lines
    .SlidingWindow(3, a => a[0] + a[1] + a[2])
    .SlidingWindow(2, a => a[0] < a[1])
    .Count(x => x));
