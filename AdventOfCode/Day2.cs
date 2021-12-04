namespace AdventOfCode;

public class Day2 : IAdventDay
{
	private enum Direction
	{
		Forward,
		Down,
		Up,
	}

	private class Instruction
	{
		public Direction Direction { get; init; }
		public int Scalar { get; init; }
	}

	private List<Instruction> InputArray { get; }

	public Day2(string input) =>
		InputArray = input.Split("\n").Where(w => w.Any()).Select(s => {
			var split = s.Split(' ');
			return new Instruction
			{
				Direction = split[0] switch
				{
					"forward" => Direction.Forward,
					"down" => Direction.Down,
					"up" => Direction.Up,
					_ => throw new NotImplementedException()
				},
				Scalar = Convert.ToInt32(split[1])
			};
		}).ToList();

	public string Part1()
	{
		var position = InputArray.Where(w => w.Direction == Direction.Forward).Sum(s => s.Scalar);

		var depth = InputArray.Where(w => w.Direction != Direction.Forward).Sum(s => s.Direction == Direction.Up ? -s.Scalar : s.Scalar);

		return (position * depth).ToString();
	}

	public string Part2()
	{
		var aim = 0;
		var position = 0;
		var depth = 0;
		InputArray.ForEach(x =>
		{
			switch (x.Direction)
			{
				case Direction.Forward:
					position += x.Scalar;
					depth += aim * x.Scalar;
					break;
				case Direction.Down:
					aim += x.Scalar;
					break;
				case Direction.Up:
					aim -= x.Scalar;
					break;
			}
		});

		return (position * depth).ToString();
	}
}
