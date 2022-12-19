namespace AdventOfCode;

public class Day16 : IAdventDay
{
	private List<bool> InputArray { get; }

	public Day16(string input) => InputArray = input.SelectMany(s => GetByte(s)).ToList();

	private static IEnumerable<bool> GetByte(char input)
	{
		var temp = input switch
		{
			'0' => 0,
			'1' => 1,
			'2' => 2,
			'3' => 3,
			'4' => 4,
			'5' => 5,
			'6' => 6,
			'7' => 7,
			'8' => 8,
			'9' => 9,
			'A' => 10,
			'B' => 11,
			'C' => 12,
			'D' => 13,
			'E' => 14,
			'F' => 15,
			_ => throw new NotImplementedException(),
		};

		for (var i = 3; i >= 0; i--)
		{
			yield return (temp & (1 << i)) != 0;
		}
	}

	private static ulong ReadVersion(List<bool> input) => Convert(Read(input, 0, 3));
	private static ulong ReadType(List<bool> input) => Convert(Read(input, 3, 3));
	private static List<bool> Read(List<bool> input, int skip, int? take = null) => input.Skip(skip).Take(take ?? input.Count - skip).ToList();
	private static ulong Convert(List<bool> input)
	{
		ulong result = 0;
		input.ForEach(i => result = (result << 1) | (i ? 1 : (ulong)0));
		return result;
	}

	private abstract class PacketBase
	{
		public int Version { get; }
		public int TotalLength { get; protected init; }
		protected int Header { get; init; }
		public int PacketType { get; }
		public ulong? Literal { get; protected init; }
		protected List<bool> Data { get; }
		public List<PacketBase> Packets { get; } = new();

		public PacketBase(List<bool> bytes, int version, int type, int header)
		{
			Data = bytes;
			Version = version;
			Header = header;
			PacketType = type;
		}
	}
	private class PacketL0Base : PacketBase
	{
		private const int Length = 15;

		public PacketL0Base(List<bool> bytes, int version, int type) : base(bytes, version, type, 7)
		{
			var offset = Header;

			var totalLength = (int)Convert(Read(Data, offset, Length));

			totalLength += offset + Length;

			offset += Length;
			do
			{
				var test = PacketFactory(bytes.Skip(offset).ToList());
				offset += test.TotalLength;
				Packets.Add(test);
			} while (offset < totalLength);

			TotalLength = offset;
		}
	}

	private class PacketL1Base : PacketBase
	{
		private const int Length = 11;

		public PacketL1Base(List<bool> bytes, int version, int type) : base(bytes, version, type, 7)
		{
			var packetCount = (int)Convert(Read(Data, Header, Length));

			var offset = Header + Length;

			for (var i = 0; i < packetCount; i++)
			{
				var test = PacketFactory(bytes.Skip(offset).ToList());
				Packets.Add(test);
				offset += test.TotalLength;
			}
			TotalLength = offset;
		}
	}

	private class PacketLiteral : PacketBase
	{
		public PacketLiteral(List<bool> bytes, int version, int type) : base(bytes, version, type, 6)
		{
			var result = new List<bool>();
			var offset = Header;
			bool test;
			do
			{
				var take = Read(Data, offset, 5);
				test = take.First();
				result.AddRange(take.Skip(1));
				offset += 5;
			} while (test);
			Literal = Convert(result);
			TotalLength = offset;
		}
	}

	private static PacketBase PacketFactory(List<bool> bytes)
	{
		var version = (int)ReadVersion(bytes);
		var type = (int)ReadType(bytes);
		var length = Read(bytes, 6, 1).First();

		return type switch
		{
			4 => new PacketLiteral(bytes, version, type),
			_ => length switch
			{
				false => new PacketL0Base(bytes, version, type),
				true => new PacketL1Base(bytes, version, type)
			},
		};
	}

	private int SumVersions(PacketBase packet) => packet.Version + packet.Packets.Sum(x => SumVersions(x));

	private ulong Evaluate(PacketBase packet)
	{
		var result = packet.PacketType switch
		{
			0 => packet.Packets.Select(s => Evaluate(s)).Aggregate((sum, s) => sum + s),
			1 => packet.Packets.Select(s => Evaluate(s)).Aggregate((sum, s) => sum * s),
			2 => packet.Packets.Min(s => Evaluate(s)),
			3 => packet.Packets.Max(s => Evaluate(s)),
			4 => packet.Literal,
			5 => (ulong)(Evaluate(packet.Packets.First()) > Evaluate(packet.Packets.Last()) ? 1 : 0),
			6 => (ulong)(Evaluate(packet.Packets.First()) < Evaluate(packet.Packets.Last()) ? 1 : 0),
			7 => (ulong)(Evaluate(packet.Packets.First()) == Evaluate(packet.Packets.Last()) ? 1 : 0),
			_ => throw new NotImplementedException(),
		};

		return result ?? 0;
	}

	public string Part1()
	{
		//foreach (var i in InputArray)
		//{ Console.Write(i ? 1 : 0); }
		var packet = PacketFactory(InputArray);

		return SumVersions(packet).ToString();
	}

	public string Part2()
	{
		var packet = PacketFactory(InputArray);

		return Evaluate(packet).ToString();
	}
}
