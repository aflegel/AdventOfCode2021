using System.Numerics;

namespace AdventOfCode;

public class Day19 : IAdventDay
{
	private List<Sensor> InputArray { get; }

	public Day19(string input) => InputArray = input.Replace("\r", "")
		//by scanner
		.Split("\n\n").Select((s, i) => new Sensor
		{
			Id = i,
			Beacons = s.Split("\n").Skip(1).Select(s =>
			{
				var x = s.Split(",").Select(x => Convert.ToInt32(x)).ToList();

				return new Vector3(x[0], x[1], x[2]);
			}).ToList()
		}).ToList();

	private class Sensor
	{
		public int Id { get; set; }
		public Vector3 Position { get; set; }
		public List<Vector3> Beacons { get; set; }
	}

	private static (Vector3 translation, Vector3 rotation)? MatchRotation((Vector3 a, Vector3 b/*, Vector3 c*/) pair1, (Vector3 a, Vector3 b/*, Vector3 c*/) pair2)
	{
		var pair2l = new List<Vector3> { pair2.a, pair2.b /*, pair2.c */};

		for (var i = 0; i < 4; i++)
		{
			for (var j = 0; j < 4; j++)
			{
				for (var k = 0; k < 4; k++)
				{
					var rotation = Rotate(pair2l, i, j, k);
					var shift = DetectHotizontalShift(pair1, (rotation[0], rotation[1]/*, test[2]*/));

					if (shift != null)
					{
						return ((Vector3)shift, new Vector3(i, j, k));
					}
				}
			}
		}

		return null;
	}

	private static Vector3? DetectHotizontalShift((Vector3 a, Vector3 b/*, Vector3 c*/) pair1, (Vector3 a, Vector3 b/*, Vector3 c*/) pair2)
	{
		var testA = pair1.a - pair2.a;
		var testb = pair1.b - pair2.b;

		if (testA == testb)
			return testA;
		else
			return null;
	}

	private static List<Vector3> Rotate(List<Vector3> beacons, int x, int y, int z)
	{
		if (x == 0 && y == 0 && z == 0)
			return beacons;

		var test = beacons.Select(s =>
				{
					if (x > 0)
						return new Vector3(s.X, s.Z, -s.Y);
					else if (y > 0)
						return new Vector3(-s.Z, s.Y, s.X);
					else if (z > 0)
						return new Vector3(s.Y, -s.X, s.Z);

					return s;
				}).ToList();

		if (x > 0)
			x--;
		else if (y > 0)
			y--;
		else if (z > 0)
			z--;


		return Rotate(test, x, y, z);
	}

	private static List<(Vector3 a, Vector3 b)> CrossProduct(List<Vector3> positions) => positions
		.SelectMany(s => positions, (a, b) => (a, b))
			.Where(w => w.a != w.b)
			.ToList();

	private static Sensor? Triangulate(List<Vector3> beacons, Sensor unknownSensor)
	{
		var beaconList = CrossProduct(beacons);
		var unknownList = CrossProduct(unknownSensor.Beacons);

		foreach (var unknown in unknownList)
		{
			var unknownSignature = unknown.a - unknown.b;
			foreach (var beacon in beaconList)
			{
				var beaconSignature = beacon.a - beacon.b;

				if (unknownSignature.Length() == beaconSignature.Length())
				{
					//Console.WriteLine();
					var match = MatchRotation(beacon, unknown);
					if (match.HasValue)
					{

						Console.Write($"{Render(beacon.a)}, {Render(beacon.b)}");
						Console.WriteLine($"  to  {Render(unknown.a)}, {Render(unknown.b)}");
						Console.WriteLine($"  Rotated ({match?.rotation.X * 90},{match?.rotation.Y * 90},{match?.rotation.Z * 90}) Translated {Render(match?.translation ?? new Vector3())}");

						return new Sensor
						{
							Id = unknownSensor.Id,
							Position = match?.translation ?? new Vector3(),
							Beacons = Rotate(unknownSensor.Beacons, (int)match.Value.rotation.X, (int)match.Value.rotation.Y, (int)match.Value.rotation.Z)
								.Select(s => new Vector3(s.X + match.Value.translation.X, s.Y + match.Value.translation.Y, s.Z + match.Value.translation.Z))
								.ToList()
						};
					}
				}
			}
		}
		return null;
	}

	private List<Sensor> TriangulateSensors(List<Sensor> triangulatedSensors, List<Sensor> unknown)
	{
		if (unknown.Count == 0)
			return triangulatedSensors;

		foreach (var sensor in unknown)
		{
			Console.WriteLine($"Sensor {sensor.Id}: ");

			foreach (var triangulated in triangulatedSensors)
			{
				Console.WriteLine($"  against Sensor {triangulated.Id}");

				var result = Triangulate(triangulated.Beacons, sensor);

				if (result != null)
				{
					triangulatedSensors.Add(result);
					var zero = triangulatedSensors.First();
					zero.Beacons = zero.Beacons.Union(result.Beacons).Distinct().ToList();
					break;
				}
			}
		}

		unknown = unknown.Where(w => !triangulatedSensors.Select(s => s.Id).Contains(w.Id)).ToList();

		return TriangulateSensors(triangulatedSensors, unknown);
	}


	public string Part1()
	{
		var beacons = new List<Vector3>();

		foreach (var beacon in InputArray.First().Beacons)
		{
			beacons.Add(new Vector3(beacon.X, beacon.Y, beacon.Z));
		}

		var triangulatedSensors = TriangulateSensors(new List<Sensor> { InputArray.First() }, InputArray.Skip(1).ToList());

		beacons = triangulatedSensors.SelectMany(s => s.Beacons).Distinct().ToList();

		return beacons.Count.ToString();
	}

	public string Part2()
	{
		var beacons = new List<Vector3>();

		foreach (var beacon in InputArray.First().Beacons)
		{
			beacons.Add(new Vector3(beacon.X, beacon.Y, beacon.Z));
		}

		var triangulatedSensors = TriangulateSensors(new List<Sensor> { InputArray.First() }, InputArray.Skip(1).ToList());

		var positions = triangulatedSensors.SelectMany(s => triangulatedSensors, (a, b) => a.Position - b.Position).Max(m => m.X + m.Y + m.Z);

		return positions.ToString();
	}

	private static string Render(Vector3 position) => $"({position.X},{position.Y},{position.Z})";
}
