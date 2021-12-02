namespace AdventOfCode;

public class Day1 : IAdventDay
{
	public async Task<string> SolveAsync(string input)
    {
        var inputArray = input.Split("\n").Select(s => Convert.ToInt32(s)).ToArray();

        var increases = inputArray.Select((item, index) => (item, index))
            //skip 0
            .Skip(1)
            .Where(w => inputArray[w.index - 1] < w.item).Count();

        return increases.ToString();
    }
}
