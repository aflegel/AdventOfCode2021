using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day20Tests
{
	private readonly string input = @"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

#..#.
#....
##..#
..#..
..###";

	[Fact]
	public void Part1SouldMatchExampleCountB()
	{
		var day2 = new Day20(input);

		var answer = day2.Part1();

		answer.Should().Be("35");
	}

	[Fact]
	public void Part2SouldMatchExampleCountH()
	{
		var day2 = new Day20(input);

		var answer = day2.Part2();

		answer.Should().Be("3351");
	}
}
