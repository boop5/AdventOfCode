// ReSharper disable once CheckNamespace
namespace AdventOfCode22.Day9;

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