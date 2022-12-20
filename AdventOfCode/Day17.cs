namespace AdventOfCode;

public class Day17 : IAdventDay
{
	private (int x1, int x2, int y1, int y2) TargetArea { get; }

	public Day17(string input)
	{
		var xStart = input.IndexOf('x') + 2;
		var xEnd = input.IndexOf(',');
		var x = input[xStart..xEnd].Split("..");

		var yStart = input.IndexOf('y') + 2;
		var y = input[yStart..].Split("..");

		TargetArea = (Convert.ToInt32(x[0]), Convert.ToInt32(x[1]), Convert.ToInt32(y[0]), Convert.ToInt32(y[1]));
	}

	private record Probe
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Dx { get; set; }
		public int Dy { get; set; }
	}

	private Probe Advance(Probe probe)
	{
		var newProbe = new Probe
		{
			X = probe.X + probe.Dx,
			Y = probe.Y + probe.Dy,
			Dx = probe.Dx + (probe.Dx > 0 ? -1 : probe.Dx < 0 ? 1 : 0),
			Dy = probe.Dy - 1
		};

		//Console.WriteLine($"({probe.X},{probe.Y})");

		return newProbe;
	}

	private enum Potential
	{
		Continue,
		Arrived,
		Terminal
	}

	private Potential TestLocation(Probe probe)
	{
		if (probe.X >= TargetArea.x1 && probe.X <= TargetArea.x2 && probe.Y >= TargetArea.y1 && probe.Y <= TargetArea.y2)
			return Potential.Arrived;

		if (probe.Dx > 0 && probe.X > TargetArea.x2)
			return Potential.Terminal;
		else if (probe.Dx == 0 && (probe.X > TargetArea.x2 || probe.X < TargetArea.x1))
			return Potential.Terminal;

		if (probe.Dy <= 0 && probe.Y < TargetArea.y1)
			return Potential.Terminal;

		return Potential.Continue;
	}

	private int FiringSolution(Probe probe)
	{
		var maxHeight = 0;
		Potential result;
		do
		{
			probe = Advance(probe);
			result = TestLocation(probe);
			//Console.WriteLine($"{probe.X},{probe.Y}");
			maxHeight = probe.Y > maxHeight ? probe.Y : maxHeight;

		} while (result == Potential.Continue);

		return result == Potential.Terminal ? -1 : maxHeight;
	}

	private int MinimumX()
	{
		var position = 0;
		var velocity = 0;
		while (position < TargetArea.x1)
		{
			position += ++velocity;
		}
		return velocity;
	}

	private IEnumerable<(int, int, int)> FiringSolutions()
	{
		var xMin = MinimumX();
		var xRange = Enumerable.Range(xMin, TargetArea.x2 - xMin + 1);

		foreach (var x in xRange)
		{
			//I'm not proud of this
			for (var y = TargetArea.y1; y < 1000; y++)
			{
				var probe = new Probe
				{
					X = 0,
					Y = 0,
					Dx = x,
					Dy = y,
				};

				var result = FiringSolution(probe);
				if (result >= 0)
				{
					yield return (x, y, result);
				}
			}
		}
	}

	public string Part1() => FiringSolutions().Max(s => s.Item3).ToString();

	public string Part2() => FiringSolutions().Count().ToString();

	private void Render(Probe probe)
	{
		Console.WriteLine($"Pos: {probe.X},{probe.Y}");
		Console.WriteLine($"Vel: {probe.Dx},{probe.Dy}");
	}
}
