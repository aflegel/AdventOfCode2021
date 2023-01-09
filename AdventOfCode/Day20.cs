namespace AdventOfCode;

public class Day20 : IAdventDay
{
	private bool[] InputArray { get; }
	private bool[,] InputImage { get; }

	public Day20(string input)
	{
		var test = input.Replace("\r", "")
			.Split("\n\n");

		InputArray = test.First().Select(s => s == '#').ToArray();

		var image = test[1].Split("\n");

		InputImage = new bool[image.Length, image[0].Length];

		for (var i = 0; i < image.Length; i++)
		{
			for (var j = 0; j < image[i].Length; j++)
			{
				InputImage[i, j] = image[i][j] == '#';
			}
		}
	}

	private class World
	{
		public bool Background { get; init; }

		public List<Coordinate> Coordinates { get; init; }

		public bool Get(int x, int y) => Coordinates.Contains(new Coordinate(x, y)) ? !Background : Background;

		public (int x1, int y1, int x2, int y2) BoundingBox
		{
			get
			{
				var x1 = Coordinates.Min(s => s.X);
				var x2 = Coordinates.Max(s => s.X);
				var y1 = Coordinates.Min(m => m.Y);
				var y2 = Coordinates.Max(m => m.Y);

				return (x1, y1, x2, y2);
			}
		}

	}

	private record Coordinate(int X, int Y);

	private static IEnumerable<bool> ReadIndex(World input, int x, int y)
	{
		yield return input.Get(x - 1, y - 1);
		yield return input.Get(x - 1, y);
		yield return input.Get(x - 1, y + 1);

		yield return input.Get(x, y - 1);
		yield return input.Get(x, y);
		yield return input.Get(x, y + 1);

		yield return input.Get(x + 1, y - 1);
		yield return input.Get(x + 1, y);
		yield return input.Get(x + 1, y + 1);
	}

	private static int GetIndex(World input, int x, int y)
	{
		var index = 0;

		var test = ReadIndex(input, x, y);

		foreach (var bit in test)
		{
			index <<= 1;
			index ^= Convert.ToInt32(bit);
		}

		return index;
	}


	private World Process(World world)
	{
		var box = world.BoundingBox;

		var backgroundIndex = GetIndex(world, -1000, -1000);

		var world2 = new World
		{
			Background = InputArray[backgroundIndex],
			Coordinates = new List<Coordinate>(),
		};


		for (var x = box.x1 - 1; x < box.x2 + 2; x++)
		{
			for (var y = box.y1 - 1; y < box.y2 + 2; y++)
			{
				var index = GetIndex(world, x, y);
				if (InputArray[index] != world2.Background)
					world2.Coordinates.Add(new Coordinate(x, y));
			}
		}

		return world2;
	}

	public string Part1()
	{
		var world = new World
		{
			Background = false,
			Coordinates = new List<Coordinate>(),
		};

		for (var i = 0; i < InputImage.GetLength(0); i++)
		{
			for (var j = 0; j < InputImage.GetLength(1); j++)
			{
				if (InputImage[i, j])
					world.Coordinates.Add(new Coordinate(i, j));
			}
		}

		world = Process(world);
		world = Process(world);

		return world.Coordinates.Count().ToString();
	}

	public string Part2()
	{
		var world = new World
		{
			Background = false,
			Coordinates = new List<Coordinate>(),
		};

		for (var i = 0; i < InputImage.GetLength(0); i++)
		{
			for (var j = 0; j < InputImage.GetLength(1); j++)
			{
				if (InputImage[i, j])
					world.Coordinates.Add(new Coordinate(i, j));
			}
		}

		for(var i = 0; i < 50; i++)
		{
			world = Process(world);
			Console.WriteLine(i);
		}

		return world.Coordinates.Count().ToString();
	}
}
