namespace AdventOfCode;

public class Day8 : IAdventDay
{
	private record SegmentDisplay
	{
		public string[] Encoding { get; set; }
		public string[] Display { get; set; }
	}

	private SegmentDisplay[] InputArray { get; }

	public Day8(string input) => InputArray = input.Replace("\r", "").Split("\n").Select(s =>
	{
		var record = s.Split("|");
		return new SegmentDisplay
		{
			Encoding = record[0].Split(" ").Where(w => w.Length > 0).ToArray(),
			Display = record[1].Split(" ").Where(w => w.Length > 0).ToArray()
		};
	}).ToArray();

	private readonly int[] uniqueDigits = new int[] { 2, 3, 4, 7 };
	public string Part1() => InputArray.SelectMany(w => w.Display)
		.Count(w => uniqueDigits.Contains(w.Length)).ToString();

	public string Part2()
	{
		var sum = 0;
		foreach(var item in InputArray)
		{
			var groups = item.Encoding.GroupBy(w => w.Length).ToDictionary(key => key.Key, values => values.ToArray());

			var mapping = new string[10];
			mapping[1] = groups[2].FirstOrDefault() ?? string.Empty;
			mapping[4] = groups[4].FirstOrDefault() ?? string.Empty;
			mapping[7] = groups[3].FirstOrDefault() ?? string.Empty;
			mapping[8] = groups[7].FirstOrDefault() ?? string.Empty;

			mapping[9] = groups[6].FirstOrDefault(w => mapping[4].All(a => w.Contains(a))) ?? string.Empty;
			mapping[0] = groups[6].FirstOrDefault(w => mapping[1].All(a => w.Contains(a)) && w != mapping[9]) ?? string.Empty;
			mapping[6] = groups[6].FirstOrDefault(w => w != mapping[0] && w != mapping[9]) ?? string.Empty;

			mapping[3] = groups[5].FirstOrDefault(w => mapping[1].All(a => w.Contains(a))) ?? string.Empty;
			mapping[5] = groups[5].FirstOrDefault(w => w.All(a => mapping[6].Contains(a))) ?? string.Empty;
			mapping[2] = groups[5].FirstOrDefault(w => w != mapping[3] && w != mapping[5]) ?? string.Empty;

			int toNumber(string num) => Array.IndexOf(mapping, mapping.First(f => f.Length == num.Length && f.Intersect(num).Count() == f.Length));

			sum += Convert.ToInt32($"{toNumber(item.Display[0])}{toNumber(item.Display[1])}{toNumber(item.Display[2])}{toNumber(item.Display[3])}");
		}

		return sum.ToString();
	}
}
