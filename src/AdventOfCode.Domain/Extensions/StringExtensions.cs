namespace AdventOfCode.Domain.Extensions;

public static class StringExtensions
{
    public static string[] SplitByBlankLine(
        this string str,
        StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    {
        return str.Split(new[] { Environment.NewLine + Environment.NewLine }, options);
    }

    public static IEnumerable<string> SplitByLineBreak(
        this string str, 
        StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    {
        return str.Split(Environment.NewLine, options);
    }

    public static bool IsNumber(this string str)
    {
        return int.TryParse(str, out _);
    }
}