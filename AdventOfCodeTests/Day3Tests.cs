﻿using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day3Tests
{
	private readonly string input = "00100\n11110\n10110\n10111\n10101\n01111\n00111\n11100\n10000\n11001\n00010\n01010";

	[Fact]
	public void Part1SouldMatchExampleCount()
	{
		var day2 = new Day3(input);

		var answer = day2.Part1();

		answer.Should().Be("198");
	}

	[Fact]
	public void Part2SouldMatchExampleCount()
	{
		var day2 = new Day3(input);

		var answer = day2.Part2();

		answer.Should().Be("230");
	}
}
