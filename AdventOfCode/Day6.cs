namespace AdventOfCode;

public class Day6 : IAdventDay
{
	private int[] InputArray { get; }
	public Day6(string input) => InputArray = input.Replace("\r", "").Split(",").Select(s => Convert.ToInt32(s)).ToArray();

	private long SimulateFish(int dayCount)
	{
		var fishBuckets = new List<long>();

		for (var i = 0; i < 9; i++)
		{
			fishBuckets.Add(InputArray.Where(s => s == i).Count());
		}

		for (var day = 0; day < dayCount; day++)
		{
			var newFish = fishBuckets.First();
			fishBuckets = fishBuckets.Skip(1).ToList();
			fishBuckets[6] += newFish;
			fishBuckets.Add(newFish);
		}
		
		return fishBuckets.Sum(s => s);
	}

	public string Part1() => SimulateFish(80).ToString();
	public string Part2() => SimulateFish(256).ToString();
}
