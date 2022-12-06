using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace AdventOfCode22.Day5;

public class Day5
{
    private readonly Regex _instructionsRegex = new(@"move (\d+) from (\d+) to (\d+)");

    [Fact]
    public void SolvePart1()
    {
        var result = Solve(MoveBehavior.MoveCrate);

        Assert.Equal("QPJPLMNNR", result);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = Solve(MoveBehavior.MoveStack);

        Assert.Equal("BQDNWJPVJ", result);
    }

    private string Solve(MoveBehavior moveBehavior)
    {
        var input = InputReader.ReadText();
        var splitInput = input.Split(Environment.NewLine + Environment.NewLine);
        var schemeText = splitInput[0];
        var instructionsText = splitInput[1];

        var columns = BuildColumns(schemeText);
        var instructions = BuildInstructions(instructionsText);

        ExecuteInstructions(columns, instructions, moveBehavior);

        var result = BuildResultString(columns);

        return result;
    }

    private string BuildResultString(IReadOnlyDictionary<int, LinkedList<char>> columns)
    {
        return string.Join(string.Empty, columns.Select(x => x.Value.First()).ToList());
    }

    private void ExecuteInstructions(IReadOnlyDictionary<int, LinkedList<char>> columns,
                                     IEnumerable<MoveInstruction> instructions,
                                     MoveBehavior moveBehavior)
    {
        foreach (var instruction in instructions)
        {
            if (moveBehavior == MoveBehavior.MoveStack)
            {
                var stack = new List<char>();
                for (var i = 0; i < instruction.Move; i++)
                {
                    stack.Add(columns[instruction.From].First());
                    columns[instruction.From].RemoveFirst();
                }

                stack.Reverse();
                foreach (var crate in stack)
                {
                    columns[instruction.To].AddFirst(crate);
                }
            }
            else if (moveBehavior == MoveBehavior.MoveCrate)
            {
                for (var i = 0; i < instruction.Move; i++)
                {
                    var x = columns[instruction.From].First();
                    columns[instruction.From].RemoveFirst();
                    columns[instruction.To].AddFirst(x);
                }
            }
        }
    }

    private IEnumerable<MoveInstruction> BuildInstructions(string instructionsText)
    {
        var instructions = instructionsText.Split(Environment.NewLine).ToList();

        foreach (var instruction in instructions)
        {
            var x = _instructionsRegex.Match(instruction).Groups;
            var moveInstruction = new MoveInstruction(int.Parse(x[1].Value),
                                                      int.Parse(x[2].Value),
                                                      int.Parse(x[3].Value));
            yield return moveInstruction;
        }
    }

    private IReadOnlyDictionary<int, LinkedList<char>> BuildColumns(string schemeText)
    {
        var rows = schemeText.Split(Environment.NewLine).SkipLast(1).ToList();
        var columns = new Dictionary<int, LinkedList<char>>();

        foreach (var row in rows)
        {
            var rowColumns = row
                             .ToList()
                             .Chunk(4)
                             .Select(x => x.Length > 3 ? x.SkipLast(1) : x)
                             .Select(x => x.ToList()[1])
                             .ToList();

            for (var colIndex = 0; colIndex < rowColumns.Count; colIndex++)
            {
                var col = rowColumns[colIndex];
                columns.TryAdd(colIndex + 1, new LinkedList<char>());

                if (col != ' ') columns[colIndex + 1].AddLast(col);
            }
        }

        return columns;
    }

    private enum MoveBehavior
    {
        MoveCrate,
        MoveStack
    }

    private class MoveInstruction
    {
        public MoveInstruction(int move, int from, int to)
        {
            Move = move;
            From = from;
            To = to;
        }

        public int Move { get; }
        public int From { get; }
        public int To { get; }
    }
}