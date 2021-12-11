namespace AdventOfCode;

public class Day11 : IAdventDay
{
	private int[,] InputArray { get; }
	public Day11(string input)
	{
		var temp = input.Replace("\r", "").Split("\n");
		InputArray = new int[10, 10];
		for (var i = 0; i < temp.Length; i++)
		{
			for (var j = 0; j < temp[i].Length; j++)
			{
				InputArray[i, j] = Convert.ToInt32($"{temp[i][j]}");
			}
		}
	}

	private IEnumerable<(int x, int y)> GetNeighbours((int x, int y) coordinates)
	{
		var width = InputArray.GetLength(0);
		var height = InputArray.GetLength(1);

		//orthoganal
		if (coordinates.x > 0)
			yield return (coordinates.x - 1, coordinates.y);
		if (coordinates.x < width - 1)
			yield return (coordinates.x + 1, coordinates.y);
		if (coordinates.y > 0)
			yield return (coordinates.x, coordinates.y - 1);
		if (coordinates.y < height - 1)
			yield return (coordinates.x, coordinates.y + 1);

		//diagonal
		if (coordinates.x > 0 && coordinates.y > 0)
			yield return (coordinates.x - 1, coordinates.y - 1);
		if (coordinates.x > 0 && coordinates.y < height - 1)
			yield return (coordinates.x - 1, coordinates.y + 1);
		if (coordinates.x < width - 1 && coordinates.y > 0)
			yield return (coordinates.x + 1, coordinates.y - 1);
		if (coordinates.x < width - 1 && coordinates.y < width - 1)
			yield return (coordinates.x + 1, coordinates.y + 1);

	}

	private int Flash()
	{
		var sum = 0;
		for (var i = 0; i < InputArray.GetLength(0); i++)
		{
			for (var j = 0; j < InputArray.GetLength(1); j++)
			{
				if (InputArray[i, j] > 9)
				{
					sum++;
					InputArray[i, j] = 0;

					var neighbours = GetNeighbours((i, j));

					foreach (var (x, y) in neighbours)
					{
						if (InputArray[x, y] > 0)
						{
							InputArray[x, y]++;
						}
					}
				}
			}
		}

		if (sum > 0)
			sum += Flash();

		return sum;
	}

	public string Part1()
	{
		var sum = 0;
		for (var day = 0; day < 100; day++)
		{
			for (var i = 0; i < InputArray.GetLength(0); i++)
			{
				for (var j = 0; j < InputArray.GetLength(1); j++)
				{
					InputArray[i, j]++;
				}
			}

			sum += Flash();
		}

		return sum.ToString();
	}
	public string Part2()
	{
		var day = 0;
		while (!InputArray.Cast<int>().All(a => a == 0))
		{
			day++;
			for (var i = 0; i < InputArray.GetLength(0); i++)
			{
				for (var j = 0; j < InputArray.GetLength(1); j++)
				{
					InputArray[i, j]++;
				}
			}

			Flash();
		}
		return day.ToString();
	}
}
