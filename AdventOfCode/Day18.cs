namespace AdventOfCode;

public class Day18 : IAdventDay
{
	private List<string> InputArray { get; }
	private readonly char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
	private readonly char[] symbols = new char[] { '[', ']', ',' };

	public Day18(string input) => InputArray = input.Replace("\r", "").Split("\n").ToList();

	private string Reduce(string input)
	{
		do
		{
			if (Explode(input, out var output))
			{
				input = output;
				continue;
			}

			if (Split(input, out output))
			{
				input = output;
				continue;
			}

			return input;
		} while (true);
	}

	private enum Direction
	{
		Left,
		Right
	}

	private bool Explode(string input, out string output)
	{
		var pairStack = new Stack<char>();
		for (var i = 0; i < input.Length; i++)
		{
			if (input[i] == '[')
				pairStack.Push(input[i]);
			else if (input[i] == ']')
				pairStack.Pop();
			else if (input[i] == ',' && numbers.Contains(input[i - 1]) && numbers.Contains(input[i + 1]))
			{
				if (pairStack.Count > 4)
				{
					var left = input[..i];
					var right = input[(i + 1)..];

					var leftDigit = left[(left.LastIndexOf('[') + 1)..];
					var rightDigit = right[..right.IndexOf(']')];

					left = left[..^(leftDigit.Length + 1)];
					right = right[(rightDigit.Length + 1)..];

					var firstL = FindFirst(Direction.Left, left);
					if (firstL.index >= 0)
					{
						left = left.Remove(firstL.index, firstL.value.Length).Insert(firstL.index, $"{Convert.ToInt32(firstL.value) + Convert.ToInt32(leftDigit)}");
					}

					var firstR = FindFirst(Direction.Right, right);
					if (firstR.index >= 0)
					{
						right = right.Remove(firstR.index, firstR.value.Length).Insert(firstR.index, $"{Convert.ToInt32(firstR.value) + Convert.ToInt32(rightDigit)}");
					}

					output = left + "0" + right;
					return true;
				}
			}
		}

		output = string.Empty;
		return false;
	}

	private bool Split(string input, out string output)
	{
		for (var i = 0; i < input.Length; i++)
		{
			if (numbers.Contains(input[i]) && numbers.Contains(input[i + 1]))
			{
				var left = input[..i];
				var right = input[i..];

				var digit = right[..right.IndexOfAny(symbols)];
				right = right[digit.Length..];

				var num = Convert.ToDouble(digit) / 2;

				output = left + $"[{Math.Floor(num)},{Math.Ceiling(num)}]" + right;
				return true;
			}
		}

		output = string.Empty;
		return false;
	}

	private string Add(string inputA, string inputB) => Reduce($"[{inputA},{inputB}]");

	private string Magnitude(string input)
	{
		if (input[0] != '[')
			return input;

		for (var i = 0; i < input.Length; i++)
		{
			if (input[i] == ',' && numbers.Contains(input[i - 1]) && numbers.Contains(input[i + 1]))
			{
				var left = input[..i];
				var right = input[(i + 1)..];

				var leftDigit = left[(left.LastIndexOf('[') + 1)..];
				var rightDigit = right[..right.IndexOf(']')];

				left = left[..^(leftDigit.Length + 1)];
				right = right[(rightDigit.Length + 1)..];

				var sum = Convert.ToInt32(leftDigit) * 3 + Convert.ToInt32(rightDigit) * 2;

				return Magnitude(left + sum + right);
			}
		}

		return "";
	}

	private (string value, int index) FindFirst(Direction direction, string input)
	{
		if (direction == Direction.Left)
		{
			var numEnd = input.LastIndexOfAny(numbers);

			if (numEnd == -1)
				return ("", numEnd);
			else
			{
				var numStart = input[..numEnd].LastIndexOfAny(symbols) + 1;
				return (input[numStart..++numEnd], numStart);
			}
		}
		else
		{
			var numStart = input.IndexOfAny(numbers);
			if (numStart == -1)
				return ("", numStart);
			else
			{
				var numEnd = input[numStart..].IndexOfAny(symbols) + numStart;
				return (input[numStart..numEnd], numStart);
			}
		}
	}

	public string Part1()
	{
		var result = InputArray[0];
		for (var i = 1; i < InputArray.Count; i++)
		{
			result = Add(result, InputArray[i]);
		}
		return Magnitude(result);
	}

	public string Part2()
	{
		var test = InputArray.SelectMany(s => InputArray, (a, b) => (a, b))
			.Where(w => w.a != w.b)
			.SelectMany(s => new List<int> { Convert.ToInt32(Magnitude(Add(s.a, s.b))), Convert.ToInt32(Magnitude(Add(s.b, s.a))) })
			.Max();

		return test.ToString();
	}
}
