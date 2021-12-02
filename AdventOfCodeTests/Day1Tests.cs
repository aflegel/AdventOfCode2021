using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day1Tests
{
	[Fact]
	public void SouldMatchExampleCount()
	{
		var input = "199\n200\n208\n210\n200\n207\n240\n269\n260\n263";

		var day1 = new Day1();

		var answer = day1.SolveAsync(input).GetAwaiter().GetResult();

		answer.Should().Be("7");
	}
}
