using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day15Tests
{
	private readonly string input = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";

	[Fact]
	public void Part1SouldMatchExampleCount()
	{
		var day2 = new Day15(input);

		var answer = day2.Part1();

		answer.Should().Be("40");
	}

	[Fact]
	public void Part2SouldMatchExampleCount()
	{
		var day2 = new Day15(input);

		var answer = day2.Part2();

		answer.Should().Be("315");
	}
}
