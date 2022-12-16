using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day14Tests
{
	private readonly string input = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

	[Fact]
	public void Part1SouldMatchExampleCount()
	{
		var day2 = new Day14(input);

		var answer = day2.Part1();

		answer.Should().Be("1588");
	}

	[Fact]
	public void Part2SouldMatchExampleCount()
	{
		var day2 = new Day14(input);

		var answer = day2.Part2();

		answer.Should().Be("2188189693529");
	}
}
