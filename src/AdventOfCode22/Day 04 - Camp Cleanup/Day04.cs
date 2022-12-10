// ReSharper disable once CheckNamespace
namespace AdventOfCode22.Day04;

public class Day04
{
    private bool SectionContainsOtherSection(string sections)
    {
        var splitSections = sections.Split(",");
        var section1 = splitSections[0];
        var section2 = splitSections[1];

        var range1 = section1.Split("-");
        var from1 = int.Parse(range1[0]);
        var to1 = int.Parse(range1[1]);

        var range2 = section2.Split("-");
        var from2 = int.Parse(range2[0]);
        var to2 = int.Parse(range2[1]);


        if (from1 <= from2 && to1 >= to2)
        {
            return true;
        }
        else if (from2 <= from1 && to2 >= to1)
        {
            return true;
        }

        return false;
    }

    private bool SectionsOverlap(string sections)
    {
        var splitSections = sections.Split(",");
        var section1 = splitSections[0];
        var section2 = splitSections[1];

        var range1 = section1.Split("-");
        var from1 = int.Parse(range1[0]);
        var to1 = int.Parse(range1[1]);
        var sections1 = Enumerable.Range(from1, to1 - from1 + 1);

        var range2 = section2.Split("-");
        var from2 = int.Parse(range2[0]);
        var to2 = int.Parse(range2[1]);
        var sections2 = Enumerable.Range(from2, to2 - from2 + 1);

        if (sections1.Any(sections2.Contains))
        {
            return true;
        }

        return false;
    }

    [Fact]
    public void SolvePart1()
    {
        var pairs = InputReader.ReadLines();
        var occurrences = pairs.Where(SectionContainsOtherSection).Count();

        Assert.Equal(453, occurrences);
    }

    [Fact]
    public void SolvePart2()
    {
        var pairs = InputReader.ReadLines();
        var occurrences = pairs.Where(SectionsOverlap).Count();

        Assert.Equal(919, occurrences);
    }
}