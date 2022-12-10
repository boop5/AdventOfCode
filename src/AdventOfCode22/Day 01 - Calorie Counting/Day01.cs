// ReSharper disable once CheckNamespace
namespace AdventOfCode22.Day01;

public class Day01
{
    private static IEnumerable<int> GetCalories(IEnumerable<string> input)
    {
        return input.Select(x => x.SplitByLineBreak()
                                  .Where(StringExtensions.IsNumber)
                                  .Select(int.Parse)
                                  .Sum());
    }

    [Fact]
    public void SolvePart1()
    {
        var elves = InputReader.ReadText().SplitByBlankLine();
        var maxCalories = GetCalories(elves).Max();

        Assert.Equal(69281, maxCalories);
    }

    [Fact]
    public void SolvePart2()
    {
        var elves = InputReader.ReadText().SplitByBlankLine();
        var maxCalories = GetCalories(elves)
                          .OrderByDescending(x => x)
                          .Take(3)
                          .Sum();

        Assert.Equal(201524, maxCalories);
    }
}