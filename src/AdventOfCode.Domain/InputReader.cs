using System.Reflection;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Domain;

public static class InputReader
{
    public static IEnumerable<string> ReadLines([CallerFilePath] string sourceFilePath = "")
    {
        var dir = Path.GetFileName(Path.GetDirectoryName(sourceFilePath));
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        var inputFilePath = Path.Join(path, dir, "input");

        return File.ReadAllLines(inputFilePath);
    }

    public static string ReadText([CallerFilePath] string sourceFilePath = "")
    {
        var dir = Path.GetFileName(Path.GetDirectoryName(sourceFilePath));
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        var inputFilePath = Path.Join(path, dir, "input");

        return File.ReadAllText(inputFilePath);
    }
}