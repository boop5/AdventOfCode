// ReSharper disable once CheckNamespace
namespace AdventOfCode22.Day09;

internal class Instruction
{
    private Instruction((int X, int Y) direction, int moves)
    {
        Direction = direction;
        Moves = moves;
    }

    public (int X, int Y) Direction { get; init; }
    public int Moves { get; set; }

    public static Instruction FromString(string line)
    {
        var split = line.Split(' ');
        var direction = split[0] switch
        {
        "L" => (-1, 0),
        "R" => (1, 0),
        "D" => (0, -1),
        "U" => (0, 1),
        _ => throw new ArgumentException("bad input", nameof(line))
        };
        var moves = int.Parse(split[1]);

        return new Instruction(direction, moves);
    }
}