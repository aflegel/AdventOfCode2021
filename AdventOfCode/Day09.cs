namespace AdventOfCode;

public class Day09 : IAdventDay
{
	private int[,] InputArray { get; }

	public Day09(string input)
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

	public string Part1() => GetLowPoints().Sum(s => InputArray[s.x, s.y] + 1).ToString();

	private IEnumerable<(int x, int y)> GetLowPoints()
	{
		for (var i = 0; i < InputArray.GetLength(0); i++)
		{
			for (var j = 0; j < InputArray.GetLength(1); j++)
			{
				var neighbours = GetNeighbours((i, j));

				if (neighbours.All(a => InputArray[a.x, a.y] > InputArray[i, j]))
					yield return (i, j);
			}
		}
	}

	private IEnumerable<(int x, int y)> GetNeighbours((int x, int y) coordinates)
	{
		var width = InputArray.GetLength(0);
		var height = InputArray.GetLength(1);

		if (coordinates.x > 0)
			yield return (coordinates.x - 1, coordinates.y);
		if (coordinates.x < width - 1)
			yield return (coordinates.x + 1, coordinates.y);
		if (coordinates.y > 0)
			yield return (coordinates.x, coordinates.y - 1);
		if (coordinates.y < height - 1)
			yield return (coordinates.x, coordinates.y + 1);
	}

	public string Part2()
	{
		var lowPoints = GetLowPoints();

		var basins = lowPoints.Select(s => GetBasins(Array.Empty<(int x, int y)>(), s)).OrderByDescending(o => o.Length).Take(3).Select(s => s.Length);

		return basins.Aggregate((sum, next) => sum *= next).ToString();
	}

	private (int x, int y)[] GetBasins((int x, int y)[] coordinateList, (int x, int y) coordinate)
	{
		var neighbours = GetNeighbours(coordinate).Where(i => InputArray[i.x, i.y] != 9).ToList();

		var current = coordinateList.Union(neighbours).ToArray();
		foreach(var i in neighbours.Except(coordinateList))
		{
			var recusion = GetBasins(current, i);

			current = current.Union(recusion).ToArray();
		}

		return current;
	}
}
