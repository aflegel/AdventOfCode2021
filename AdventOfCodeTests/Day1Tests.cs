﻿using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day1Tests
{
	private readonly string input = "199\n200\n208\n210\n200\n207\n240\n269\n260\n263";
	
	[Fact]
	public void Part1SouldMatchExampleCount()
	{
		var day1 = new Day1(input);

		var answer = day1.Part1();

		answer.Should().Be("7");
	}

	[Fact]
	public void Part2SouldMatchExampleCount()
	{
		var day1 = new Day1(input);

		var answer = day1.Part2();

		answer.Should().Be("5");
	}
}
