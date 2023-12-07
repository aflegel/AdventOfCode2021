using System.Numerics;

namespace AdventOfCode;

public class Day23 : IAdventDay
{
	private class Hallway
	{
		public char[] RoomA { get; set; }
		public char[] RoomB { get; set; }
		public char[] RoomC { get; set; }
		public char[] RoomD { get; set; }

		public char[] HallwaySpaces { get; set; }
	}
	private Hallway Input { get; }

	public Day23(string input)
	{
		var data = input.ReplaceLineEndings("\n").Split("\n").ToList();

		Input = new Hallway
		{
			HallwaySpaces = Enumerable.Repeat(' ', 11).ToArray(),
			RoomA = new[] { data[2][3], data[3][3] },
			RoomB = new[] { data[2][5], data[3][5] },
			RoomC = new[] { data[2][7], data[3][7] },
			RoomD = new[] { data[2][9], data[3][9] }
		};
	}

	public string Part1()
	{
		throw new NotImplementedException();
	}

	public string Part2()
	{
		throw new NotImplementedException();
	}
}

