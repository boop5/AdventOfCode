// ReSharper disable once CheckNamespace

namespace AdventOfCode22.Day9;

internal class Instruction
{
    public Instruction(string line)
    {
        var split = line.Split(' ');
        Direction = split[0] switch
        {
        "L" => (-1, 0),
        "R" => (1, 0),
        "D" => (0, -1),
        "U" => (0, 1),
        _ => throw new ArgumentOutOfRangeException()
        };
        Moves = int.Parse(split[1]);
    }

    public (int X, int Y) Direction { get; init; }
    public int Moves { get; set; }
}

public class Day9
{
    [Fact]
    public void SolvePart1()
    {
        var input = InputReader.ReadLines();
        var instructions = BuildInstructions(input);
        var count = GetUniqueVisitsForTail1(instructions);

        Assert.Equal(6406, count);
    }

    [Fact]
    public void SolvePart2()
    {
        var input = InputReader.ReadLines();
        var instructions = BuildInstructions(input);
        var count = GetUniqueVisitsForTail10(instructions);

        Assert.Equal(2643, count);
    }

    private List<Instruction> BuildInstructions(IEnumerable<string> input)
    {
        var instructions = new List<Instruction>();

        foreach (var line in input)
        {
            var instruction = new Instruction(line);
            instructions.Add(instruction);
        }

        return instructions;
    }

    private int GetUniqueVisitsForTail1(List<Instruction> instructions)
    {
        (int X, int Y) head = (0, 0);
        (int X, int Y) tail = (0, 0);

        var visitedPositionsTail = new HashSet<(int, int)>();

        foreach (var instruction in instructions)
            for (var i = 0; i < instruction.Moves; i++)
            {
                head.Move(instruction.Direction);
                tail.Follow(head);

                visitedPositionsTail.Add(tail);
            }

        return visitedPositionsTail.Count;
    }

    private int GetUniqueVisitsForTail10(List<Instruction> instructions)
    {
        (int X, int Y)[] knots = Enumerable.Repeat((0,0), 10).ToArray();

        var visitedPositionsTail = new HashSet<(int, int)>();

        foreach (var instruction in instructions)
            for (var i = 0; i < instruction.Moves; i++)
            {
                knots[0].X += instruction.Direction.X;
                knots[0].Y += instruction.Direction.Y;

                for (var j = 1; j < knots.Length; j++)
                {
                    knots[j].Follow(knots[j-1]);
                }

                visitedPositionsTail.Add(knots.Last());
            }

        return visitedPositionsTail.Count;
    }
}

internal static class Day9Extensions
{
    public static void Move(this ref (int X, int Y) pos, (int X, int Y) moves)
    {
        pos.X += moves.X;
        pos.Y += moves.Y;
    }

    public static void Follow(this ref (int X, int Y) pos, (int X, int Y) target)
    {
        var stepX = target.X - pos.X;
        var stepY = target.Y - pos.Y;

        if (Math.Abs(stepX) <= 1 && Math.Abs(stepY) <= 1)
            return;

        pos.X += Math.Sign(stepX);
        pos.Y += Math.Sign(stepY);
    }
}