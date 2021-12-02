using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day2Tests
{
	[Fact]
	public void Part1SouldMatchExampleCount()
	{
		var input = "forward 5\ndown 5\nforward 8\nup 3\ndown 8\nforward 2";

		var day2 = new Day2(input);

		var answer = day2.SolvePart1();

		answer.Should().Be(150);
	}

	[Fact]
	public void Part2SouldMatchExampleCount()
	{
		var input = "forward 5\ndown 5\nforward 8\nup 3\ndown 8\nforward 2";

		var day2 = new Day2(input);

		var answer = day2.SolvePart2();

		answer.Should().Be(900);
	}
}
