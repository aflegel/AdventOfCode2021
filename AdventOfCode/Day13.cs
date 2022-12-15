namespace AdventOfCode;

public class Day13 : IAdventDay
{
	private enum Fold
	{
		X,
		Y,
	}
	private (int x, int y)[] InputArray { get; }
	private (Fold fold, int i)[] FoldArray { get; }

	public Day13(string input)
	{
		var split = input.Replace("\r", "").Split("\n\n");
		InputArray = split[0].Split("\n").Select(s =>
		{
			var temp = s.Split(",");
			return (Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1]));
		}).ToArray();

		FoldArray = split[1].Split("\n").Select(s =>
		{
			var temp = s.Split("=");
			return (temp[0].Contains('x') ? Fold.X : Fold.Y, Convert.ToInt32(temp[1]));
		}).ToArray();

		Console.WriteLine($"X: {InputArray.Max(m => m.x)} - {CalculatePageSize(Fold.X)}");
		Console.WriteLine($"Y: {InputArray.Max(m => m.y)} - {CalculatePageSize(Fold.Y)}");
	}

	private bool[,] BuildPage()
	{
		var map = new bool[CalculatePageSize(Fold.X), CalculatePageSize(Fold.Y)];

		foreach (var (x, y) in InputArray)
		{
			map[x, y] = true;
		}

		return map;
	}

	private int CalculatePageSize(Fold fold) => FoldArray.Where(w => w.fold == fold).Max(m => m.i) * 2 + 1;

	private bool[,] FoldPage(bool[,] page, Fold fold, int position)
	{
		var oldX = page.GetLength(0);
		var oldY = page.GetLength(1);

		var newX = fold == Fold.X ? position : oldX;
		var newY = fold == Fold.Y ? position : oldY;

		var newPage = new bool[newX, newY];

		for (var j = 0; j < newY; j++)
		{
			for (var i = 0; i < newX; i++)
			{
				newPage[i, j] = page[i, j] | page[fold == Fold.X ? oldX - i - 1 : i, fold == Fold.Y ? oldY - j - 1 : j];
			}
		}

		return newPage;
	}

	private void Render(bool[,] page)
	{
		for (var i = 0; i < page.GetLength(1); i++)
		{
			for (var j = 0; j < page.GetLength(0); j++)
			{
				Console.Write(page[j, i] ? "#" : " ");
			}
			Console.WriteLine();
		}
	}

	public string Part1()
	{
		var page = BuildPage();
		var fold = FoldArray.First();
		page = FoldPage(page, fold.fold, fold.i);
		return page.Cast<bool>().Count(s => s == true).ToString();
	}

	public string Part2()
	{
		var page = BuildPage();
		foreach (var fold in FoldArray)
			page = FoldPage(page, fold.fold, fold.i);

		Render(page);
		return page.Cast<bool>().Count(s => s == true).ToString();
	}
}
