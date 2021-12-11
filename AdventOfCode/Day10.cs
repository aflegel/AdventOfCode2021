namespace AdventOfCode;

public class Day10 : IAdventDay
{
	private string[] InputArray { get; }

	public Day10(string input) => InputArray = input.Replace("\r", "").Split("\n");

	private int GetCompilerScore(char character) => character switch
	{
		')' => 3,
		']' => 57,
		'}' => 1197,
		'>' => 25137,
		_ => throw new ArgumentOutOfRangeException()
	};

	private char GetMatch(char character) => character switch
	{
		'(' => ')',
		'[' => ']',
		'{' => '}',
		'<' => '>',
		_ => throw new ArgumentOutOfRangeException(),
	};

	public string Part1()
	{
		var sum = 0;
		foreach (var input in InputArray)
		{
			var stack = new Stack<char>();

			foreach (var i in input)
			{
				if (i is '(' or '{' or '[' or '<')
				{
					stack.Push(i);
				}
				else
				{
					if (i != GetMatch(stack.Pop()))
					{
						sum += GetCompilerScore(i);
					}
				}
			}
		}

		return sum.ToString();
	}

	private long GetAutocompleteScore(char character) => character switch
	{
		'(' => 1,
		'[' => 2,
		'{' => 3,
		'<' => 4,
		_ => throw new ArgumentOutOfRangeException()
	};


	public string Part2()
	{
		var sum = new List<long>();

		foreach (var input in InputArray)
		{
			var valid = true;
			var stack = new Stack<char>();

			foreach (var i in input)
			{
				if (i is '(' or '{' or '[' or '<')
				{
					stack.Push(i);
				}
				else
				{
					var match = stack.Pop();

					var test = GetMatch(match);
					if (i != test)
					{
						valid = false;
						break;
					}
				}
			}

			if (valid)
			{
				sum.Add(stack.Select(s => GetAutocompleteScore(s)).Aggregate((total, next) => total * 5 + next));
			}
		}

		return sum.OrderBy(o => o).Skip(sum.Count / 2).Take(1).First().ToString();
	}
}
