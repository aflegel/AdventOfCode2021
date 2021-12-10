using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day2Tests
{
	private readonly string input = @"forward 5
down 5
forward 8
up 3
down 8
forward 2";

	[Fact]
	public void Part1SouldMatchExampleCount()
	{
		var day2 = new Day2(input);

		var answer = day2.Part1();

		answer.Should().Be("150");
	}

	[Fact]
	public void Part2SouldMatchExampleCount()
	{
		var day2 = new Day2(input);

		var answer = day2.Part2();

		answer.Should().Be("900");
	}
}
