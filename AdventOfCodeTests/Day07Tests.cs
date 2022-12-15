using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day07Tests
{
	private readonly string input = @"16,1,2,0,4,2,7,1,2,14";

	[Fact]
	public void Part1SouldMatchExampleCount()
	{
		var day2 = new Day07(input);

		var answer = day2.Part1();

		answer.Should().Be("37");
	}

	[Fact]
	public void Part2SouldMatchExampleCount()
	{
		var day2 = new Day07(input);

		var answer = day2.Part2();

		answer.Should().Be("168");
	}
}
