using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day03Tests
{
	private readonly string input = @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";

	[Fact]
	public void Part1SouldMatchExampleCount()
	{
		var day2 = new Day03(input);

		var answer = day2.Part1();

		answer.Should().Be("198");
	}

	[Fact]
	public void Part2SouldMatchExampleCount()
	{
		var day2 = new Day03(input);

		var answer = day2.Part2();

		answer.Should().Be("230");
	}
}
