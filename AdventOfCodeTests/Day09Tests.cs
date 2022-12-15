using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day09Tests
{
	private readonly string input = @"2199943210
3987894921
9856789892
8767896789
9899965678";

	[Fact]
	public void Part1SouldMatchExampleCount()
	{
		var day2 = new Day09(input);

		var answer = day2.Part1();

		answer.Should().Be("15");
	}

	[Fact]
	public void Part2SouldMatchExampleCount()
	{
		var day2 = new Day09(input);

		var answer = day2.Part2();

		answer.Should().Be("1134");
	}
}
