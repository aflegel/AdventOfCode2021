namespace AdventOfCode;

public class Day15 : IAdventDay
{
	private int[,] InputArray { get; }

	public Day15(string input)
	{
		var rows = input.Replace("\r", "").Split("\n");

		InputArray = new int[rows.Length, rows.First().Length];

		for (var i = 0; i < rows.Length; i++)
		{
			for (var j = 0; j < rows[i].Length; j++)
			{
				InputArray[i, j] = Convert.ToInt32($"{rows[i][j]}");
			}
		}
	}

	private class Tile : IEquatable<Tile>
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Cost { get; set; }
		public int TotalCost => Cost + Parent?.TotalCost ?? 0;
		public Tile Parent { get; set; }

		public bool Equals(Tile? other) => GetHashCode() == other?.GetHashCode();
		public override int GetHashCode() => X.GetHashCode() ^ (Y.GetHashCode() * 1000);
		public override bool Equals(object obj) => Equals(obj as Tile);
	}

	private static List<Tile> GetNextTiles(int[,] input, Tile currentTile)
	{
		var maxX = input.GetLength(0) - 1;
		var maxY = input.GetLength(1) - 1;

		var possibleTiles = new List<Tile>()
		{
			new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile },
			new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile },
			new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile },
			new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile },
		}
			.Where(tile => tile.X >= 0 && tile.X <= maxX)
			.Where(tile => tile.Y >= 0 && tile.Y <= maxY)
			.ToList();

		possibleTiles.ForEach(tile => tile.Cost = input[tile.X, tile.Y]);

		return possibleTiles;
	}

	private static int AStar(int[,] input)
	{
		var start = new Tile
		{
			X = 0,
			Y = 0,
			Cost = 0
		};

		var finish = new Tile
		{
			X = input.GetLength(0) - 1,
			Y = input.GetLength(1) - 1,
		};

		var activeTiles = new List<Tile>
		{
			start
		};
		var visitedTiles = new List<Tile>();

		while (activeTiles.Any())
		{
			var checkTile = activeTiles.OrderBy(x => x.TotalCost).First();

			if (checkTile.Equals(finish))
			{
				return checkTile.TotalCost;
			}

			visitedTiles.Add(checkTile);
			activeTiles.Remove(checkTile);

			var nextTiles = GetNextTiles(input, checkTile);

			foreach (var next in nextTiles)
			{
				if (visitedTiles.Any(a => a.Equals(next)))
					continue;

				if (activeTiles.Any(x => x.Equals(next)))
				{
					var existingTile = activeTiles.First(x => x.Equals(next));
					if (existingTile.TotalCost > next.TotalCost)
					{
						activeTiles.Remove(existingTile);
						activeTiles.Add(next);
					}
				}
				else
				{
					activeTiles.Add(next);
				}
			}
		}

		return -1;
	}

	public string Part1() => AStar(InputArray).ToString();

	public string Part2()
	{
		var oldX = InputArray.GetLength(0);
		var oldY = InputArray.GetLength(1);
		var largeInput = new int[oldX * 5, oldY * 5];

		for (var i = 0; i < largeInput.GetLength(0); i++)
		{
			for (var j = 0; j < largeInput.GetLength(1); j++)
			{
				largeInput[i, j] = InputArray[i % oldX, j % oldY] + i / oldX + j / oldY;
				if (largeInput[i, j] >= 10)
					largeInput[i, j] -= 9;
			}
		}

		return AStar(largeInput).ToString();
	}

	private static void RenderPath(Tile tile)
	{
		if (tile.Parent == null)
			return;
		Console.WriteLine($"{tile.X},{tile.Y} - {tile.Cost}");
		RenderPath(tile.Parent);
		return;
	}

	private void Render(int[,] page)
	{
		for (var i = 0; i < page.GetLength(1); i++)
		{
			for (var j = 0; j < page.GetLength(0); j++)
			{
				Console.Write(page[i, j]);
			}
			Console.WriteLine();
		}
	}
}
