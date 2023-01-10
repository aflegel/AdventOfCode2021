namespace AdventOfCode;

public class Day21 : IAdventDay
{
	private int InputPlayer1 { get; }
	private int InputPlayer2 { get; }

	public Day21(string input)
	{
		var players = input.Replace("\r", "")
			.Split("\n");

		InputPlayer1 = Convert.ToInt32(players[0][(players[0].LastIndexOf(":") + 1)..]);
		InputPlayer2 = Convert.ToInt32(players[1][(players[1].LastIndexOf(":") + 1)..]);
	}

	internal interface IDie
	{
		public int Roll();
	}

	private class DeterministicDie : IDie
	{
		private int Current { get; set; } = 0;
		public int Roll()
		{
			if (++Current == 101)
				Current = 1;
			return Current;
		}
	}

	private record Player
	{
		public int Id { get; init; }
		public int Score { get; set; }
		public int Position { get; set; }
		public int Turns { get; set; }
	}

	private static int Roll(IDie die) => die.Roll() + die.Roll() + die.Roll();

	private enum Turn
	{
		Player1,
		Player2
	}



	private static void Process(Player player, int roll)
	{
		player.Position += roll;
		player.Position %= 10;
		player.Score += player.Position == 0 ? 10 : player.Position;
		player.Turns++;
	}

	public string Part1()
	{
		var die = new DeterministicDie();

		var p1 = new Player { Id = 1, Score = 0, Position = InputPlayer1, };
		var p2 = new Player { Id = 2, Score = 0, Position = InputPlayer2, };

		var turn = Turn.Player1;

		while (p1.Score < 1000 && p2.Score < 1000)
		{
			var roll = Roll(die);
			if (turn == Turn.Player1)
			{
				turn = Turn.Player2;
				Process(p1, roll);
			}
			else
			{
				turn = Turn.Player1;
				Process(p2, roll);
			}
		}

		return (Math.Min(p1.Score, p2.Score) * (p1.Turns + p2.Turns) * 3).ToString();
	}

	private static Player CloneProcess(Player player, int roll)
	{
		var newPlayer = new Player
		{
			Id = player.Id,
			Position = player.Position,
			Score = player.Score
		};
		Process(newPlayer, roll);

		return newPlayer;
	}

	private static Dictionary<int, int> Distribution => new() { { 3, 1 }, { 4, 3 }, { 5, 6 }, { 6, 7 }, { 7, 6 }, { 8, 3 }, { 9, 1 } };

	private (ulong p1Wins, ulong p2Wins) Play(Player p1, Player p2, Turn turn)
	{
		if (turn == Turn.Player2 && p1.Score >= 21)
			return (1, 0);
		else if (turn == Turn.Player1 && p2.Score >= 21)
			return (0, 1);

		ulong p1Wins = 0;
		ulong p2Wins = 0;

		foreach (var roll in Distribution)
		{
			(ulong p1, ulong p2) results;
			if (turn == Turn.Player1)
			{
				var newP1 = CloneProcess(p1, roll.Key);
				results = Play(newP1, p2, Turn.Player2);
			}
			else
			{
				var newP2 = CloneProcess(p2, roll.Key);
				results = Play(p1, newP2, Turn.Player1);
			}

			p1Wins += results.p1 * (ulong)roll.Value;
			p2Wins += results.p2 * (ulong)roll.Value;
		}

		return (p1Wins, p2Wins);
	}

	public string Part2()
	{
		var p1 = new Player { Id = 1, Score = 0, Position = InputPlayer1, };
		var p2 = new Player { Id = 2, Score = 0, Position = InputPlayer2, };
		var turn = Turn.Player1;

		var results = Play(p1, p2, turn);
		return Math.Max(results.p1Wins, results.p2Wins).ToString();
	}
}

