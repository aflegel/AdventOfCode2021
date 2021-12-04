namespace AdventOfCode;

public class Day4 : IAdventDay
{
	private class Bingo
	{
		private int[][] Grid { get; init; }
		private int[] MarkedNumbers { get; set; }

		public Bingo(string input)
		{
			MarkedNumbers = new int[0];
			Grid = input.Split("\n").Select(s => s.Split(" ").Where(w => w.Length > 0).Select(ss => Convert.ToInt32(ss)).ToArray()).ToArray();
		}

		public bool Marknumber(int number)
		{
			if (Grid.SelectMany(s => s).Contains(number))
				MarkedNumbers = MarkedNumbers.Append(number).ToArray();

			return HasBingo();
		}

		public int Score => Grid.SelectMany(s => s).Except(MarkedNumbers).Sum(s => s);

		public bool HasBingo()
		{
			for (var i = 0; i < Grid.Length; i++)
			{
				if (Grid[i].All(a => MarkedNumbers.Contains(a)))
					return true;
			}

			for (var i = 0; i < Grid[0].Length; i++)
			{
				if (Grid.Select(s => s[i]).All(a => MarkedNumbers.Contains(a)))
					return true;
			}

			return false;
		}
	}

	private int[] InputArray { get; }
	private Bingo[] BingoArray { get; }

	public Day4(string input)
	{
		input = input.Replace("\r", "");

		InputArray = input[..input.IndexOf("\n")].Split(",").Select(s => Convert.ToInt32(s)).ToArray();

		BingoArray = input[input.IndexOf("\n")..].Split("\n\n").Where(w => w.Length > 0).Select(s => new Bingo(s)).ToArray();
	}

	public string Part1()
	{
		foreach (var row in InputArray)
		{
			foreach (var col in BingoArray)
			{
				if (col.Marknumber(row))
				{
					return (col.Score * row).ToString();
				}
			}
		}
		return string.Empty;
	}

	public string Part2()
	{
		foreach (var row in InputArray)
		{
			var indexes = new List<int>();
			var boards = BingoArray.Where(w => !w.HasBingo()).Select((board,i) => (board, i)).ToList();

			foreach (var col in boards)
			{
				if (col.board.Marknumber(row))
				{
					indexes.Add(col.i);
					if(indexes.Count == boards.Count)
						return (col.board.Score * row).ToString();
				}
			}
		}
		return string.Empty;
	}
}
