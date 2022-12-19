using Xunit;
using AdventOfCode;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day16Tests
{
	private readonly string input = @"D2FE28";

	[Fact]
	public void Part1SouldMatchExampleCountA()
	{
		var day2 = new Day16("8A004A801A8002F478");

		var answer = day2.Part1();

		answer.Should().Be("16");
	}

	[Fact]
	public void Part1SouldMatchExampleCountB()
	{
		var day2 = new Day16("620080001611562C8802118E34");

		var answer = day2.Part1();

		answer.Should().Be("12");
	}

	[Fact]
	public void Part1SouldMatchExampleCountC()
	{
		var day2 = new Day16("C0015000016115A2E0802F182340");

		var answer = day2.Part1();

		answer.Should().Be("23");
	}

	[Fact]
	public void Part1SouldMatchExampleCountD()
	{
		var day2 = new Day16("A0016C880162017C3686B18A3D4780");

		var answer = day2.Part1();

		answer.Should().Be("31");
	}

	[Fact]
	public void Part2SouldMatchExampleCountA()
	{
		var day2 = new Day16("C200B40A82");

		var answer = day2.Part2();

		answer.Should().Be("3");
	}

	[Fact]
	public void Part2SouldMatchExampleCountB()
	{
		var day2 = new Day16("04005AC33890");

		var answer = day2.Part2();

		answer.Should().Be("54");
	}

	[Fact]
	public void Part2SouldMatchExampleCountC()
	{
		var day2 = new Day16("880086C3E88112");

		var answer = day2.Part2();

		answer.Should().Be("7");
	}

	[Fact]
	public void Part2SouldMatchExampleCountD()
	{
		var day2 = new Day16("CE00C43D881120");

		var answer = day2.Part2();

		answer.Should().Be("9");
	}

	[Fact]
	public void Part2SouldMatchExampleCountE()
	{
		var day2 = new Day16("D8005AC2A8F0");

		var answer = day2.Part2();

		answer.Should().Be("1");
	}

	[Fact]
	public void Part2SouldMatchExampleCountF()
	{
		var day2 = new Day16("F600BC2D8F");

		var answer = day2.Part2();

		answer.Should().Be("0");
	}

	[Fact]
	public void Part2SouldMatchExampleCountG()
	{
		var day2 = new Day16("9C005AC2F8F0");

		var answer = day2.Part2();

		answer.Should().Be("0");
	}

	[Fact]
	public void Part2SouldMatchExampleCountH()
	{
		var day2 = new Day16("9C0141080250320F1802104A08");

		var answer = day2.Part2();

		answer.Should().Be("1");
	}
}
