using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day21Tests
{
	private readonly string input = @"Player 1 starting position: 4
Player 2 starting position: 8";

	[Fact]
	public void Part1SouldMatchExampleCountB()
	{
		var day2 = new Day21(input);

		var answer = day2.Part1();

		answer.Should().Be("739785");
	}

	[Fact]
	public void Part2SouldMatchExampleCountH()
	{
		var day2 = new Day21(input);

		var answer = day2.Part2();

		answer.Should().Be("444356092776315");
	}
}
