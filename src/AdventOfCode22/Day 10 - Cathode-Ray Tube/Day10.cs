// ReSharper disable once CheckNamespace

namespace AdventOfCode22.Day10;

public class Day10
{
    [Fact]
    public void SolvePart1()
    {
        var input = InputReader.ReadLines();
        var register = BuildRegister(input).ToImmutableArray();
        var sum = CalculateStrengthSignal(register);

        Assert.Equal(13720, sum);
    }

    [Fact]
    public void SolvePart2()
    {
        var input = InputReader.ReadLines();
        var screen = CreateCrtScreen(input);
        var expected = "####.###..#..#.###..#..#.####..##..#..#.\r\n" +
                       "#....#..#.#..#.#..#.#..#....#.#..#.#..#.\r\n" +
                       "###..###..#..#.#..#.####...#..#....####.\r\n" +
                       "#....#..#.#..#.###..#..#..#...#....#..#.\r\n" +
                       "#....#..#.#..#.#.#..#..#.#....#..#.#..#.\r\n" +
                       "#....###...##..#..#.#..#.####..##..#..#.";


        Assert.Equal(expected, screen);
    }

    private List<int> BuildCycles(IEnumerable<string> instructions)
    {
        var cycles = new List<int>();

        foreach (var instruction in instructions)
            if (instruction == "noop")
            {
                cycles.Add(0);
            }
            else
            {
                var value = int.Parse(instruction.Split(" ")[1]);
                cycles.Add(0);
                cycles.Add(value);
            }

        return cycles;
    }

    private IReadOnlyCollection<int> BuildRegister(IEnumerable<string> instructions)
    {
        var cycles = BuildCycles(instructions);
        var register = new List<int> { 1 };

        for (var i = 0; i < cycles.Count; i++)
        {
            var currentValue = register[i];
            var newValue = currentValue + cycles[i];
            register.Add(newValue);
        }

        return register;
    }

    private int CalculateStrengthSignal(ImmutableArray<int> register)
    {
        var sum = 0;

        for (var i = 20; i < register.Length; i += 40)
        {
            var value = register[i - 1];
            sum += value * i;
        }

        return sum;
    }

    private string CreateCrtScreen(IEnumerable<string> input)
    {
        var register = BuildRegister(input).ToImmutableArray();
        var grid = Enumerable.Range(0, 6).Select(_ => Enumerable.Range(0, 40).Select(_ => ".").ToList()).ToList();

        for (var i = 0; i < register.Length; i++)
        {
            var v = register[i];

            (int x1, int x2, int x3) spritePosition = (v - 1, v, v + 1);
            var row = i / 40;
            var col = i % 40;


            if (spritePosition.x1 == col ||
                spritePosition.x2 == col ||
                spritePosition.x3 == col)
                grid[row][col] = "#";
        }

        var gridText = string.Join(Environment.NewLine, grid.Select(x => string.Join("", x)));

        return gridText;
    }

    #region Examples

    private string GetExampleInput()
    {
        return @"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop
";
    }

    [Fact]
    public void SolveExamplePart1()
    {
        var input = GetExampleInput();
        var register = BuildRegister(input.SplitByLineBreak()).ToImmutableArray();

        Assert.Equal(21, register[20 - 1]);
        Assert.Equal(420, register[20 - 1] * 20);

        Assert.Equal(19, register[60 - 1]);
        Assert.Equal(1140, register[60 - 1] * 60);

        Assert.Equal(18, register[100 - 1]);
        Assert.Equal(1800, register[100 - 1] * 100);

        Assert.Equal(21, register[140 - 1]);
        Assert.Equal(2940, register[140 - 1] * 140);

        Assert.Equal(16, register[180 - 1]);
        Assert.Equal(2880, register[180 - 1] * 180);

        Assert.Equal(18, register[220 - 1]);
        Assert.Equal(3960, register[220 - 1] * 220);

        var sum = CalculateStrengthSignal(register);

        Assert.Equal(13140, sum);
    }

    [Fact]
    public void SolveExamplePart2()
    {
        var input = GetExampleInput();
        var screen = CreateCrtScreen(input.SplitByLineBreak());

        var expected = @"##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....";
        Assert.Equal(expected, screen);
    }

    #endregion
}