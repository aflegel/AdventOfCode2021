using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day11Tests
{
	private readonly string input = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

	[Fact]
	public void Part1SouldMatchExampleCount()
	{
		var day2 = new Day11(input);

		var answer = day2.Part1();

		answer.Should().Be("1656");
	}

	[Fact]
	public void Part2SouldMatchExampleCount()
	{
		var day2 = new Day11(input);

		var answer = day2.Part2();

		answer.Should().Be("195");
	}
}
