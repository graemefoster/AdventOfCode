<Query Kind="Statements" />

//var initial = new int[] { 3, 4, 3, 1, 2 };
var initial = new int[] { 3, 1, 5, 4, 4, 4, 5, 3, 4, 4, 1, 4, 2, 3, 1, 3, 3, 2, 3, 2, 5, 1, 1, 4, 4, 3, 2, 4, 2, 4, 1, 5, 3, 3, 2, 2, 2, 5, 5, 1, 3, 4, 5, 1, 5, 5, 1, 1, 1, 4, 3, 2, 3, 3, 3, 4, 4, 4, 5, 5, 1, 3, 3, 5, 4, 5, 5, 5, 1, 1, 2, 4, 3, 4, 5, 4, 5, 2, 2, 3, 5, 2, 1, 2, 4, 3, 5, 1, 3, 1, 4, 4, 1, 3, 2, 3, 2, 4, 5, 2, 4, 1, 4, 3, 1, 3, 1, 5, 1, 3, 5, 4, 3, 1, 5, 3, 3, 5, 4, 2, 3, 4, 1, 2, 1, 1, 4, 4, 4, 3, 1, 1, 1, 1, 1, 4, 2, 5, 1, 1, 2, 1, 5, 3, 4, 1, 5, 4, 1, 3, 3, 1, 4, 4, 5, 3, 1, 1, 3, 3, 3, 1, 1, 5, 4, 2, 5, 1, 1, 5, 5, 1, 4, 2, 2, 5, 3, 1, 1, 3, 3, 5, 3, 3, 2, 4, 3, 2, 5, 2, 5, 4, 5, 4, 3, 2, 4, 3, 5, 1, 2, 2, 4, 3, 1, 5, 5, 1, 3, 1, 3, 2, 2, 4, 5, 4, 2, 3, 2, 3, 4, 1, 3, 4, 2, 5, 4, 4, 2, 2, 1, 4, 1, 5, 1, 5, 4, 3, 3, 3, 3, 3, 5, 2, 1, 5, 5, 3, 5, 2, 1, 1, 4, 2, 2, 5, 1, 4, 3, 3, 4, 4, 2, 3, 2, 1, 3, 1, 5, 2, 1, 5, 1, 3, 1, 4, 2, 4, 5, 1, 4, 5, 5, 3, 5, 1, 5, 4, 1, 3, 4, 1, 1, 4, 5, 5, 2, 1, 3, 3 };

var after80Days = initial.SimulateLaternFish().Take(256).Last().Sum(x => x.Value).Dump("After 80");


public static class EnumerableEx
{
	public static IEnumerable<Dictionary<int, long>> SimulateLaternFish(this int[] seed)
	{
		var next = seed.GroupBy(s => s).ToDictionary(s => s.Key, g => (long)g.Count());
		var iterations = 0;
		while (true)
		{
			iterations++;
			next = Next(next);
			yield return next;
		}
	}

	public static Dictionary<int, long> Next(Dictionary<int, long> current)
	{
		long Current(int day)
		{
			return current.ContainsKey(day) ? current[day] : 0;
		}

		return new Dictionary<int, long>()
		{
			[0] = Current(1),
			[1] = Current(2),
			[2] = Current(3),
			[3] = Current(4),
			[4] = Current(5),
			[5] = Current(6),
			[6] = Current(7) + Current(0),
			[7] = Current(8),
			[8] = Current(0)
		};
	}
}