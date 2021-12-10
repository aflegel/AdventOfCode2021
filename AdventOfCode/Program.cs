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
	1 => new Day1(input),
	2 => new Day2(input),
	3 => new Day3(input),
	4 => new Day4(input),
	5 => new Day5(input),
	6 => new Day6(input),
	7 => new Day7(input),
	8 => new Day8(input),
	9 => new Day9(input),
	_ => throw new NotImplementedException()
};

string GetPart(IAdventDay day, int part) => part switch
{
	1 => day.Part1(),
	2 => day.Part2(),
	_ => throw new NotImplementedException()
};
