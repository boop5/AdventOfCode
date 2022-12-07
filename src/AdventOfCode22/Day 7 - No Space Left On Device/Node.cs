// ReSharper disable once CheckNamespace
namespace AdventOfCode22.Day7;

internal abstract class Node
{
    public AdventDirectory? Parent { get; init; }
    public string Name { get; init; } = string.Empty;

    public abstract int GetSize();
}

internal class AdventDirectory : Node
{
    private readonly List<Node> _children = new();

    public ReadOnlyCollection<AdventDirectory> Directories
        => _children.OfType<AdventDirectory>().ToList().AsReadOnly();

    public ReadOnlyCollection<AdventFile> Files
        => _children.OfType<AdventFile>().ToList().AsReadOnly();

    public void Add(Node node)
    {
        _children.Add(node);
    }

    public override int GetSize()
    {
        return _children.Sum(x => x.GetSize());
    }
}

internal class AdventFile : Node
{
    public int Size { get; init; }

    public override int GetSize()
    {
        return Size;
    }
}