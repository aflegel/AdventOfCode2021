using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode;

public class Day12 : IAdventDay
{
	private record Cave
	{
		public string Name { get; set; }
		public List<Cave> Caves { get; set; }
	}

	internal class EnumerableComparer : IEqualityComparer<string[]>
	{
		public bool Equals(string[]? x, string[]? y) => Enumerable.SequenceEqual(x, y);

		public int GetHashCode([DisallowNull] string[] obj)
		{
			HashCode hash = new();
			Array.ForEach(obj, i => hash.Add(i));
			return hash.ToHashCode();
		}
	}

	private string[][] InputArray { get; }

	public Day12(string input) => InputArray = input.Replace("\r", "").Split("\n").Select(x => x.Split("-")).ToArray();

	private Cave[] BuildCaves()
	{
		var caves = InputArray.SelectMany(s => s).Distinct().Select(s => new Cave { Name = s, Caves = new List<Cave>() }).ToArray();

		foreach (var item in InputArray)
		{
			var first = caves.First(f => f.Name == item[0]);
			var second = caves.First(f => f.Name == item[1]);


			if (!first.Caves.Any(a => a.Name == second.Name))
			{
				first.Caves.Add(second);
			}
			if (!second.Caves.Any(a => a.Name == first.Name))
			{
				second.Caves.Add(first);
			}
		}

		return caves;
	}

	private static bool IsLower(string s) => s.All(c => char.IsLower(c));

	private IEnumerable<string[]> TraverseCaveSimple(Cave cave, string[] path)
	{
		foreach (var next in cave.Caves)
		{
			if (cave.Name is "end")
				break;

			if (IsLower(next.Name) && path.Contains(next.Name))
				continue;

			var traversal = TraverseCaveSimple(next, path.Append(cave.Name).ToArray());
			foreach (var array in traversal)
			{
				yield return array;
			}
		}

		yield return path.Append(cave.Name).ToArray();
	}

	public string Part1()
	{
		var caves = BuildCaves();

		var first = caves.First(f => f.Name == "start");

		var paths = TraverseCaveSimple(first, Array.Empty<string>()).Where(w => w.First() == "start" && w.Last() == "end").Distinct(new EnumerableComparer()).ToArray();

		return paths.Length.ToString();
	}

	private IEnumerable<string[]> TraverseCaveComplex(Cave cave, string[] path)
	{
		foreach (var next in cave.Caves)
		{
			if (cave.Name is "end")
				break;

			if (cave.Name is "start" && path.Contains(cave.Name))
				continue;

			if (IsLower(cave.Name) && cave.Name != "start" && cave.Name != "end")
			{
				var groups = path.GroupBy(a => a);
				if (groups.Any(w => IsLower(w.Key) && w.Count() == 2))
				{
					if (path.Contains(cave.Name))
						continue;
				}
			}

			var traversal = TraverseCaveComplex(next, path.Append(cave.Name).ToArray());
			foreach (var array in traversal)
			{
				yield return array;
			}
		}

		yield return path.Append(cave.Name).ToArray();
	}

	public string Part2()
	{
		var caves = BuildCaves();

		var first = caves.First(f => f.Name == "start");

		var paths = TraverseCaveComplex(first, Array.Empty<string>()).Where(w => w.First() == "start" && w.Last() == "end").Distinct(new EnumerableComparer()).ToArray();

		return paths.Length.ToString();
	}
}
