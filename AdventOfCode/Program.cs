﻿

using AdventOfCode;
Console.WriteLine("Enter the day for the Advent Calendar '1-24'");

if (int.TryParse(Console.ReadLine(), out var day))
{
	using var stream = new StreamReader($"Day{day}.txt");
	var input = await stream.ReadToEndAsync();

	Console.WriteLine(day switch
	{
		1 => await new Day1().SolveAsync(input),
		_ => throw new NotImplementedException()
	});
}
