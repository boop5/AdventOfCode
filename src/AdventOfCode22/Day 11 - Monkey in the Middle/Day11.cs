// ReSharper disable CheckNamespace
namespace AdventOfCode22.Day11;

public class Day11
{
    [Fact]
    public void SolvePart1()
    {
        var input = InputReader.ReadText();
        var monkeys = BuildMonkeys(input).ToList();

        PlayRounds(monkeys, 20, true);
        var sum = CalculateResult(monkeys);

        Assert.Equal(55216, sum);
    }

    [Fact]
    public void SolvePart2()
    {
        var input = InputReader.ReadText();
        var monkeys = BuildMonkeys(input).ToList();

        PlayRounds(monkeys, 10000, false);
        var sum = CalculateResult(monkeys);

        Assert.Equal(12848882750, sum);
    }

    private static long CalculateResult(IReadOnlyList<Monkey> monkeys)
    {
        return monkeys.Select(x => x.CountInspectedItems)
                         .OrderByDescending(x => x)
                         .Take(2)
                         .Select(Convert.ToInt64)
                         .Aggregate((x, y) => x * y);
    }

    private static void PlayRounds(IReadOnlyList<Monkey> monkeys, int rounds, bool divideBy3)
    {
        var lcm = monkeys.Select(x => x.Divisor).Aggregate((x, y) => x * y);

        for (var round = 0; round < rounds; round++)
            foreach (var monkey in monkeys)
                while (monkey.Items.Any())
                {
                    var item = monkey.Items.Dequeue();
                    monkey.CountInspectedItems++;
                    item = monkey.ExecuteOperation(item);

                    if (divideBy3)
                        item /= 3;
                    else
                        item %= lcm;

                    var testResult = monkey.Test(item);
                    var throwTo = monkey.ThrowTo(testResult);
                    monkeys[throwTo].Items.Enqueue(item);
                }
    }

    private static IEnumerable<Monkey> BuildMonkeys(string input)
    {
        return input.SplitByBlankLine()
                    .Select(x => x.SplitByLineBreak().ToList())
                    .Select(Monkey.CreateFromDescription);
    }

    #region Examples

    private const string ExampleInput = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

    [Fact]
    public void SolveExample1()
    {
        var monkeys = BuildMonkeys(ExampleInput).ToList();

        PlayRounds(monkeys, 20, true);
        var sum = CalculateResult(monkeys);

        Assert.Equal(10605, sum);
    }

    [Fact]
    public void SolveExample2()
    {
        var monkeys = BuildMonkeys(ExampleInput).ToList();
        PlayRounds(monkeys, 10000, false);
        var sum = CalculateResult(monkeys);

        Assert.Equal(2713310158, sum);
    }

    #endregion
}