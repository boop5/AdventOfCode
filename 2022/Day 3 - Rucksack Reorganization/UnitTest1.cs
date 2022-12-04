using AdventOfCode.Domain.Extensions;

namespace AoC_2022_Day3;

public class UnitTest1
{
    private Dictionary<char, int> ItemPriorities { get; } = new()
    {
        { 'a', 1 },
        { 'b', 2 },
        { 'c', 3 },
        { 'd', 4 },
        { 'e', 5 },
        { 'f', 6 },
        { 'g', 7 },
        { 'h', 8 },
        { 'i', 9 },
        { 'j', 10 },
        { 'k', 11 },
        { 'l', 12 },
        { 'm', 13 },
        { 'n', 14 },
        { 'o', 15 },
        { 'p', 16 },
        { 'q', 17 },
        { 'r', 18 },
        { 's', 19 },
        { 't', 20 },
        { 'u', 21 },
        { 'v', 22 },
        { 'w', 23 },
        { 'x', 24 },
        { 'y', 25 },
        { 'z', 26 }
    };

    [Fact]
    public void SolvePart1()
    {
        var rucksacks = ReadInput();
        var result = rucksacks.Select(GetCommonItemPriority).Sum();

        Assert.Equal(8493, result);
    }

    [Fact]
    public void SolvePart2()
    {
        var rucksacks = ReadInput().ToList();
        var groups = rucksacks.ChunkBy(3);
        var result = groups.Select(GetBadgeTypeOfGroup)
                           .Select(GetItemPriority)
                           .Sum();

        Assert.Equal(2552, result);
    }

    private char GetBadgeTypeOfGroup(List<string> group)
    {
        var elf1 = group[0];
        var elf2 = group[1];
        var elf3 = group[2];

        var badge = elf1.Where(elf2.Contains)
                        .Where(elf3.Contains)
                        .First();

        return badge;
    }

    private int GetCommonItemPriority(string rucksack)
    {
        var items = rucksack.ToList();
        var half = items.Count / 2;
        var compartment1 = items.Take(half).ToList();
        var compartment2 = items.Skip(half).Take(half).ToList();
        var commonItem = compartment1.Where(compartment2.Contains).First();
        var value = GetItemPriority(commonItem);

        return value;
    }

    private int GetItemPriority(char item)
    {
        if (char.IsUpper(item))
            return ItemPriorities[char.ToLower(item)] + 26;
        return ItemPriorities[item];
    }

    private static IEnumerable<string> ReadInput()
    {
        return File.ReadAllLines("input");
    }
}