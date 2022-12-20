using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day17Tests
{
	private readonly string input = @"target area: x=20..30, y=-10..-5";

	[Fact]
	public void Part1SouldMatchExampleCountA()
	{
		var day2 = new Day17(input);

		var answer = day2.Part1();

		answer.Should().Be("45");
	}

	[Fact]
	public void Part2SouldMatchExampleCountH()
	{
		var day2 = new Day17(input);

		var answer = day2.Part2();

		answer.Should().Be("112");
	}
}
