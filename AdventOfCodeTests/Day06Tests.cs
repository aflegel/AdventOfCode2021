using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day06Tests
{
	private readonly string input = @"3,4,3,1,2";

	[Fact]
	public void Part1SouldMatchExampleCount()
	{
		var day2 = new Day06(input);

		var answer = day2.Part1();

		answer.Should().Be("5934");
	}

	[Fact]
	public void Part2SouldMatchExampleCount()
	{
		var day2 = new Day06(input);

		var answer = day2.Part2();

		answer.Should().Be("26984457539");
	}
}
