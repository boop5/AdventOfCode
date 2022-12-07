// ReSharper disable once CheckNamespace
namespace AdventOfCode22.Day7;

public class Day7
{
    [Fact]
    public void SolvePart1()
    {
        const int maxFileSize = 100000;

        var input = InputReader.ReadLines().ToList().AsReadOnly();
        var tree = BuildTree(input);
        var flattened = FlattenTree(tree);
        var dirs = flattened.OfType<AdventDirectory>();
        var result = dirs.Select(x => x.GetSize()).Where(x => x <= maxFileSize).Sum();

        Assert.Equal(919137, result);
    }

    [Fact]
    public void SolvePart2()
    {
        const int totalDiskSize = 70000000;
        const int requiredSpace = 30000000;

        var input = InputReader.ReadLines().ToList().AsReadOnly();
        var tree = BuildTree(input);
        var usedSpace = tree.GetSize();
        var unusedSpace = totalDiskSize - usedSpace;
        var minSpaceToDelete = requiredSpace - unusedSpace;

        var flattened = FlattenTree(tree);
        var dirs = flattened.OfType<AdventDirectory>().ToList().AsReadOnly();
        var x = dirs.Select(x => x.GetSize())
                    .Where(x => x >= minSpaceToDelete)
                    .Min();

        Assert.Equal(2877389, x);
    }

    private IReadOnlyCollection<Node> FlattenTree(AdventDirectory root)
    {
        var nodes = new List<Node>();
        if (root.Files.Any()) nodes.AddRange(root.Files);

        if (root.Directories.Any()) nodes.AddRange(root.Directories);

        foreach (var child in root.Directories) nodes.AddRange(FlattenTree(child));

        return nodes;
    }

    private AdventDirectory BuildTree(ReadOnlyCollection<string> input)
    {
        var root = new AdventDirectory { Name = "/" };
        var current = root;

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.StartsWith("$ ls"))
            {
                // ignore
            }
            else if (line.StartsWith("$ cd .."))
            {
                // move upwards
                current = current.Parent!;
            }
            else if (line == "$ cd /")
            {
                // move to root
                current = root;
            }
            else if (line.StartsWith("$ cd /"))
            {
                // split and move down the path
                // wasn't required though
            }
            else if (line.StartsWith("$ cd "))
            {
                // move "down" to directory
                var dirName = line.Split(" ")[2];
                var dir = current.Directories.SingleOrDefault(x => x.Name == dirName);
                if (dir == null) 
                    current.Add(new AdventDirectory { Name = dirName, Parent = current });

                current = current.Directories.Single(x => x.Name == dirName);
            }
            else if (line.StartsWith("dir "))
            {
                // add dir
                var dirname = line.Substring(4);
                var dir = new AdventDirectory { Name = dirname, Parent = current };
                current.Add(dir);
            }
            else if (char.IsNumber(line[0]))
            {
                // add file
                var splitLine = line.Split(" ");
                var filesize = int.Parse(splitLine[0]);
                var filename = splitLine[1];
                var file = new AdventFile { Name = filename, Size = filesize, Parent = current };
                current.Add(file);
            }
        }

        return root;
    }
}