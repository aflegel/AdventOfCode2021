using AdventOfCode;

Console.WriteLine("Enter the day for the Advent Calendar '1-24'");

if (int.TryParse(Console.ReadLine(), out var day))
{
	using var stream = new StreamReader($"Day{day}.txt");
	var input = await stream.ReadToEndAsync();

	var daySolver = GetDay(day, input);

	Console.WriteLine($"Enter the which part of Day {day}:");
	if (int.TryParse(Console.ReadLine(), out var part))
	{
		Console.WriteLine(GetPart(daySolver, part));
	}
}

IAdventDay GetDay(int day, string input) => day switch
{
	1 => new Day01(input),
	2 => new Day02(input),
	3 => new Day03(input),
	4 => new Day04(input),
	5 => new Day05(input),
	6 => new Day06(input),
	7 => new Day07(input),
	8 => new Day08(input),
	9 => new Day09(input),
	10 => new Day10(input),
	11 => new Day11(input),
	12 => new Day12(input),
	13 => new Day13(input),
	14 => new Day14(input),
	15 => new Day15(input),
	16 => new Day16(input),
	17 => new Day17(input),
	18 => new Day18(input),
	19 => new Day19(input),
	_ => throw new NotImplementedException()
};

string GetPart(IAdventDay day, int part) => part switch
{
	1 => day.Part1(),
	2 => day.Part2(),
	_ => throw new NotImplementedException()
};
