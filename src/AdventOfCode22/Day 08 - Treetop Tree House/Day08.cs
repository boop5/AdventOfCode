// ReSharper disable once CheckNamespace

namespace AdventOfCode22.Day08;

public class Day08
{
    [Fact]
    public void SolvePart1()
    {
        var input = InputReader.ReadLines().ToList();
        var grid = BuildGrid(input);
        var visibilityGrid = BuildVisibilityGrid(grid);
        var flat = visibilityGrid.SelectMany(x => x);

        Assert.Equal(1782, flat.Count(x => x));
    }

    [Fact]
    public void SolvePart2()
    {
        var input = InputReader.ReadLines().ToList();
        var grid = BuildGrid(input);
        var scoreGrid = BuildScoreGrid(grid);
        var flat = scoreGrid.SelectMany(x => x);

        Assert.Equal(474606, flat.Max());
    }

    private IReadOnlyList<int[]> BuildScoreGrid(IReadOnlyList<int[]> grid)
    {
        #region Distance Methods

        int GetBelowDistance(IReadOnlyList<int[]> grid, int i, int j, int col)
        {
            int belowDistance;
            var belowTrees = grid.Skip(i + 1).Select(x => x[j]).ToList();
            var below = belowTrees.LastOrDefault(x => x >= col);
            if (below != default)
                belowDistance = belowTrees.IndexOf(below) + 1;
            else
                belowDistance = belowTrees.Count;
            return belowDistance;
        }

        int GetAboveDistance(IReadOnlyList<int[]> grid, int i, int j, int col)
        {
            int aboveDistance;
            var aboveTrees = grid.Take(i).Select(x => x[j]).ToList();
            var above = aboveTrees.LastOrDefault(x => x >= col);
            if (above != default)
                aboveDistance = aboveTrees.Count - aboveTrees.LastIndexOf(above);
            else
                aboveDistance = aboveTrees.Count;
            return aboveDistance;
        }

        int GetRightDistance(IReadOnlyList<int[]> grid, int i, int j, int col)
        {
            int rightDistance;
            var rightTrees = grid[i].Skip(j + 1).ToList();
            var right = rightTrees.FirstOrDefault(x => x >= col);
            if (right != default)
                rightDistance = rightTrees.IndexOf(right) + 1;
            else
                rightDistance = rightTrees.Count;
            return rightDistance;
        }

        int GetLeftDistance(IReadOnlyList<int[]> grid, int i, int j, int col)
        {
            int leftDistance;
            var leftTrees = grid[i].Take(j).ToList();
            var left = leftTrees.LastOrDefault(x => x >= col);
            if (left != default)
                leftDistance = leftTrees.Count - leftTrees.LastIndexOf(left);
            else
                leftDistance = leftTrees.Count;
            return leftDistance;
        }

        #endregion

        int[]?[] scoreGrid = new int[grid.Count][];

        for (var i = 0; i < grid.Count; i++)
        {
            var row = grid[i];
            for (var j = 0; j < row.Length; j++)
            {
                var col = grid[i][j];
                scoreGrid[i] ??= new int[row.Length];

                var leftDistance = GetLeftDistance(grid, i, j, col);
                var rightDistance = GetRightDistance(grid, i, j, col);
                var aboveDistance = GetAboveDistance(grid, i, j, col);
                var belowDistance = GetBelowDistance(grid, i, j, col);

                scoreGrid[i]![j] = leftDistance * aboveDistance * rightDistance * belowDistance;
            }
        }

        return scoreGrid!;
    }

    private IReadOnlyList<bool[]> BuildVisibilityGrid(IReadOnlyList<int[]> grid)
    {
        bool[]?[] visibilityGrid = new bool[grid.Count][];

        for (var i = 0; i < grid.Count; i++)
        {
            var row = grid[i];
            for (var j = 0; j < row.Length; j++)
            {
                visibilityGrid[i] ??= new bool[grid[i].Length];

                var col = grid[i][j];
                var visible = false;
                if (j == 0)
                {
                    visible = true; // left side
                }
                else if (j == grid[i].Length - 1)
                {
                    visible = true; // right side
                }
                else if (i == 0)
                {
                    visible = true; // upper side
                }
                else if (i == grid.Count - 1)
                {
                    visible = true; // lower side
                }
                else
                {
                    var leftTrees = grid[i].Take(j);
                    if (leftTrees.Max() < col) visible = true;

                    var rightTrees = grid[i].Skip(j + 1);
                    if (rightTrees.Max() < col) visible = true;

                    var treesAbove = grid.Take(i).Select(x => x[j]);
                    if (treesAbove.Max() < col) visible = true;

                    var treesBelow = grid.Skip(i + 1).Select(x => x[j]);
                    if (treesBelow.Max() < col) visible = true;
                }

                visibilityGrid[i]![j] = visible;
            }
        }

        return visibilityGrid!;
    }

    private IReadOnlyList<int[]> BuildGrid(IReadOnlyCollection<string> rows)
    {
        return rows.Select(row => row.ToCharArray())
                   .Select(columns => columns
                                      .Select(x => int.Parse(x.ToString()))
                                      .ToArray())
                   .ToArray();
    }
}