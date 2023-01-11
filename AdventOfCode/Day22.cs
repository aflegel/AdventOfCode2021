using System.Numerics;

namespace AdventOfCode;

public class Day22 : IAdventDay
{
	private List<Instruction> InputArray { get; }

	public Day22(string input) => InputArray = input.Replace("\r", "").Split("\n")
		.Select(s =>
		{
			var on = s.Contains("on");


			var sections = s.Split(',');

			var x = Extract(sections[0]);
			var y = Extract(sections[1]);
			var z = Extract(sections[2]);

			return new Instruction
			{
				State = on ? State.On : State.Off,
				Start = new Vector3(x.Item1, y.Item1, z.Item1),
				End = new Vector3(x.Item2, y.Item2, z.Item2)
			};
		}).ToList();

	private static (int, int) Extract(string input)
	{
		var split = input.Split("..");

		return (Convert.ToInt32(split[0][(split[0].IndexOf("=") + 1)..]), Convert.ToInt32(split[1]));
	}

	private enum State
	{
		On,
		Off,
	}

	private record Instruction
	{
		public State State { get; set; }
		public Vector3 Start { get; init; }
		public Vector3 End { get; init; }

		public long Volume => (State == State.On ? 1 : -1) * ((long)End.X - (long)Start.X + 1) * ((long)End.Y - (long)Start.Y + 1) * ((long)End.Z - (long)Start.Z + 1);
	}

	private static Instruction? Intersect(Instruction existing, Instruction current)
	{
		var x1 = Math.Max(existing.Start.X, current.Start.X);
		var x2 = Math.Min(existing.End.X, current.End.X);
		var y1 = Math.Max(existing.Start.Y, current.Start.Y);
		var y2 = Math.Min(existing.End.Y, current.End.Y);
		var z1 = Math.Max(existing.Start.Z, current.Start.Z);
		var z2 = Math.Min(existing.End.Z, current.End.Z);

		return x1 > x2 || y1 > y2 || z1 > z2
			? null
			: new Instruction
			{
				State = existing.State == State.On ? State.Off : State.On,
				Start = new Vector3(x1, y1, z1),
				End = new Vector3(x2, y2, z2),
			};
	}

	private static long Process(IEnumerable<Instruction> instructions)
	{
		var intersects = new List<Instruction>();

		foreach (var instruction in instructions)
		{
			var merge = new List<Instruction>();

			if (instruction.State == State.On)
				merge.Add(instruction);

			foreach (var intersect in intersects)
			{
				var adjustment = Intersect(intersect, instruction);
				if (adjustment != null)
					merge.Add(adjustment);
			}

			intersects.AddRange(merge);
		}

		return intersects.Sum(s => s.Volume);
	}

	public string Part1()
	{
		var grid = Process(InputArray.Where(w => w.Start.X >= -50 && w.Start.X <= 50 && w.Start.Y >= -50 && w.Start.Y <= 50 && w.Start.Z >= -50 && w.Start.Z <= 50));

		return grid.ToString();
	}


	public string Part2()
	{
		var grid = Process(InputArray);

		return grid.ToString();
	}
}

