namespace AdventOfCode;

public class Day05 : IAdventDay
{
	private struct Line
	{
		public Coordinate Origin { get; set; }
		public Coordinate Destination { get; set; }
	}
	private record Coordinate
	{
		public int X { get; set; }
		public int Y { get; set; }
	}
	private int[,] Map { get; }
	private Line[] Lines { get; }

	public Day05(string input)
	{
		Lines = input.Replace("\r", "").Split("\n").Select(s =>
		{
			var data = s.Split("->");
			var origin = data[0].Split(",");
			var destination = data[1].Split(",");
			return new Line
			{
				Origin = new Coordinate
				{
					X = int.Parse(origin[0]),
					Y = int.Parse(origin[1])
				},
				Destination = new Coordinate
				{
					X = int.Parse(destination[0]),
					Y = int.Parse(destination[1])
				}
			};
		}).ToArray();

		Map = new int[1000, 1000];
	}

	private (int x, int y)[] GetRange(Coordinate origin, Coordinate destination)
	{
		(var dx, var dy) = (origin.X - destination.X, origin.Y - destination.Y);

		var rangex = MakeRange(dx < 0 ? (origin.X, Math.Abs(dx)) : (destination.X, dx));
		var rangey = MakeRange(dy < 0 ? (origin.Y, Math.Abs(dy)) : (destination.Y, dy));

		if (dx == 0 || dy == 0)
		{
			return rangex.SelectMany(x => rangey, (x, y) => (x, y)).ToArray();
		}
		else
		{
			var output = new List<(int, int)>();

			var inverty = dx > 0 && dy < 0;
			var invertx = dx < 0 && dy > 0;

			for (var i = 0; i < rangey.Length; i++)
			{
				output.Add((rangex[invertx ? rangey.Length - 1 - i : i], rangey[inverty ? rangey.Length - 1 - i : i]));
			}

			return output.ToArray();
		}
	}

	private int[] MakeRange((int, int) range) => Enumerable.Range(range.Item1, range.Item2 + 1).ToArray();


	public string Part1()
	{
		var twoPlus = 0;

		foreach (var line in Lines.Where(w => w.Origin.X == w.Destination.X || w.Origin.Y == w.Destination.Y))
		{
			var range = GetRange(line.Origin, line.Destination);
			foreach (var (x, y) in range)
			{
				Map[x, y]++;
			}
		}

		foreach (var thing in Map)
		{
			if (thing >= 2)
			{
				twoPlus++;
			}
		}

		return twoPlus.ToString();
	}

	public string Part2()
	{
		var twoPlus = 0;

		foreach (var line in Lines)
		{
			var range = GetRange(line.Origin, line.Destination);
			foreach (var (x, y) in range)
			{
				Map[x, y]++;
			}
		}

		foreach (var thing in Map)
		{
			if (thing >= 2)
			{
				twoPlus++;
			}
		}

		return twoPlus.ToString();
	}
}
