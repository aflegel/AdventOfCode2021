

using AdventOfCode;
Console.WriteLine("Enter the day for the Advent Calendar '1-24'");

if (int.TryParse(Console.ReadLine(), out var day))
{
	using var stream = new StreamReader($"Day{day}.txt");
	var input = await stream.ReadToEndAsync();

	Console.WriteLine(await GetDay(day, input).SolveAsync());
}


IAdventDay GetDay(int day, string input) => day switch
{
	1 => new Day1(input),
	2 => new Day2(input),
	3 => new Day3(input),
	_ => throw new NotImplementedException()
};
