namespace AdventOfCode;

public class Day1 : IAdventDay
{
	private int[] InputArray { get; }

	public Day1(string input) => InputArray = input.Split("\n").Select(s => Convert.ToInt32(s)).ToArray();

	public int SolvePart1()
		=> InputArray.Select((item, index) => (item, index))
			//skip 0
			.Skip(1)
			.Where(w => InputArray[w.index - 1] < w.item).Count();

	public int SolvePart2()
	{
		var sums = InputArray.Select((item, index) => (item, index)).Where(w => w.index < InputArray.Length - 2)
			 .Select(w => InputArray[w.index] + InputArray[w.index + 1] + InputArray[w.index + 2]).ToArray();

		var increases = sums.Select((item, index) => (item, index))
			//skip 0
			.Skip(1)
			.Where(w => sums[w.index - 1] < w.item).Count();

		return increases;
	}

	public async Task<string> SolveAsync()
	{
		var movingIncreases = SolvePart2();

		return movingIncreases.ToString();
	}
}
