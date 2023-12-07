using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day23Tests
{
	private readonly string input = @"#############
#...........#
###B#C#B#D###
  #A#D#C#A#
  #########";

	[Fact]
	public void Part1SouldMatchExampleCountB()
	{
		var day = new Day23(input);

		var answer = day.Part1();

		answer.Should().Be("12521");
	}

	[Fact]
	public void Part2SouldMatchExampleCountH()
	{
		var day = new Day23(input);

		var answer = day.Part2();

		answer.Should().Be("2758514936282235");
	}
}
