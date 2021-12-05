<Query Kind="Statements">
  <Namespace>System.Drawing</Namespace>
</Query>

var inputRegex = new Regex(@"^(\d+),(\d+) -> (\d+),(\d+)$");

var lines = ParseInput(File.ReadAllLines(@"C:\code\github\graemefoster\AdventOfCode\src\AdventOfCode\Day5\input.txt"));

lines
.Where(IsStraightLine)
.SelectMany(PointsOnLine)
.GroupBy(p => p)
.Where(p => p.Count() > 1)
.Count()
.Dump("Crossing points on straight lines");

lines
.SelectMany(PointsOnLine)
.GroupBy(p => p)
.Where(p => p.Count() > 1)
.Count()
.Dump("Crossing points on all lines");

IEnumerable<Point> PointsOnLine((Point from, Point to) line)
{
	var xDirection = line.from.X == line.to.X ? 0 : line.to.X > line.from.X ? 1 : -1;
	var yDirection = line.from.Y == line.to.Y ? 0 : line.to.Y > line.from.Y ? 1 : -1;
	var distance = Math.Max(Math.Abs(line.from.X - line.to.X), Math.Abs(line.from.Y - line.to.Y)) + 1;

	return Enumerable.Range(0, distance).Select(e => new Point(line.from.X + (e * xDirection), line.from.Y + (e * yDirection)));
}

bool IsStraightLine((Point from, Point to) line)
{
	return line.from.X == line.to.X || line.from.Y == line.to.Y;
}

IEnumerable<(Point from, Point to)> ParseInput(string[] input)
{
	return from line in input
		   let match = inputRegex.Match(line)
		   select (
			   new Point(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)),
			   new Point(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value))
			);
}