namespace AdventOfCode;

public class Day14 : IAdventDay
{
	private string InputArray { get; }
	private Dictionary<string, char> Insertion { get; }

	public Day14(string input)
	{
		var split = input.Replace("\r", "").Split("\n\n");
		InputArray = split[0];

		Insertion = split[1].Split("\n").Select(s =>
		{
			var instruction = s.Split(" -> ");
			return (instruction[0], instruction[1].First());
		}).ToDictionary(d => d.Item1, d => d.Item2);
	}

	private void AddOccurrence(Dictionary<string, long> occurrence, string pair, long frequency)
	{
		if (occurrence.ContainsKey(pair))
			occurrence[pair] += frequency;
		else
			occurrence[pair] = frequency;
	}

	private Dictionary<string, long> RunInsertion(int depth)
	{
		var occurrence = new Dictionary<string, long>();

		for (var i = 1; i < InputArray.Length; i++)
		{
			AddOccurrence(occurrence, $"{InputArray[i - 1]}{InputArray[i]}", 1);
		}

		for (var count = 0; count < depth; count++)
		{
			var temp = new Dictionary<string, long>();
			foreach (var pair in occurrence)
			{
				var search = Insertion[pair.Key];

				AddOccurrence(temp, $"{pair.Key[0]}{search}", pair.Value);
				AddOccurrence(temp, $"{search}{pair.Key[1]}", pair.Value);
			}

			occurrence = temp;
		}

		var data = occurrence.SelectMany(s => new List<(string, long)> { (s.Key[0].ToString(), s.Value), (s.Key[1].ToString(), s.Value ) })
			.GroupBy(w => w.Item1).ToDictionary(g => g.Key, g => g.Sum(s => s.Item2));

		//adjust for undercounted start and finish chars
		data[InputArray[0].ToString()]++;
		data[InputArray[^1].ToString()]++;

		return data.ToDictionary(d => d.Key, d => d.Value / 2);
	}

	public string Part1()
	{
		var polymer = RunInsertion(10);

		var groups = polymer.Select(s => (s.Key, s.Value)).OrderByDescending(o => o.Value).ToList();

		return (groups.First().Value - groups.Last().Value).ToString();
	}

	public string Part2()
	{
		var polymer = RunInsertion(40);

		var groups = polymer.Select(s => (s.Key, s.Value)).OrderByDescending(o => o.Value).ToList();

		return (groups.First().Value - groups.Last().Value).ToString();
	}
}
