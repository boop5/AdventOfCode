// ReSharper disable once CheckNamespace
namespace AdventOfCode22.Day9;

public class Day9
{
    [Fact]
    public void SolvePart1()
    {
        var input = InputReader.ReadLines();
        var instructions = BuildInstructions(input);
        var count = GetUniqueVisitsForTail(instructions, 2);

        Assert.Equal(6406, count);
    }

    [Fact]
    public void SolvePart2()
    {
        var input = InputReader.ReadLines();
        var instructions = BuildInstructions(input);
        var count = GetUniqueVisitsForTail(instructions, 10);

        Assert.Equal(2643, count);
    }

    private IEnumerable<Instruction> BuildInstructions(IEnumerable<string> input)
    {
        return input.Select(Instruction.FromString);
    }

    private int GetUniqueVisitsForTail(IEnumerable<Instruction> instructions, int ropeLength)
    {
        var knots = Enumerable.Repeat((0, 0), ropeLength).ToArray();
        var visited = new HashSet<(int, int)>();

        foreach (var instruction in instructions)
            visited.AddRange(ExecuteInstructions(instruction, knots));

        return visited.Count;
    }

    private IEnumerable<(int, int)> ExecuteInstructions(Instruction instruction, (int, int)[] knots)
    {
        for (var i = 0; i < instruction.Moves; i++)
        {
            knots[0].Move(instruction.Direction);

            for (var iKnot = 1; iKnot < knots.Length; iKnot++)
                knots[iKnot].Follow(knots[iKnot - 1]);

            yield return knots.Last();
        }
    }
}