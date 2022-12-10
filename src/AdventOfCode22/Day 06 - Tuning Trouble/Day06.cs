// ReSharper disable once CheckNamespace
namespace AdventOfCode22.Day06;

public class Day06
{
    [Fact]
    public void SolvePart1()
    {
        // given examples
        Assert.Equal(5, FindDistinctTokenIndex("bvwbjplbgvbhsrlpgdmjqwftvncz", 4));
        Assert.Equal(6, FindDistinctTokenIndex("nppdvjthqldpwncqszvftbrmjlhg", 4));
        Assert.Equal(10, FindDistinctTokenIndex("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 4));
        Assert.Equal(11, FindDistinctTokenIndex("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 4));

        // actual stream
        var dataStream = InputReader.ReadText();
        var index = FindDistinctTokenIndex(dataStream, tokenLength: 4);
        Assert.Equal(1275, index);
    }

    [Fact]
    public void SolvePart2()
    {
        // given examples
        Assert.Equal(19, FindDistinctTokenIndex("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 14));
        Assert.Equal(23, FindDistinctTokenIndex("bvwbjplbgvbhsrlpgdmjqwftvncz", 14));
        Assert.Equal(23, FindDistinctTokenIndex("nppdvjthqldpwncqszvftbrmjlhg", 14));
        Assert.Equal(29, FindDistinctTokenIndex("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 14));
        Assert.Equal(26, FindDistinctTokenIndex("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 14));

        // actual stream
        var dataStream = InputReader.ReadText();
        var index = FindDistinctTokenIndex(dataStream, tokenLength: 14);
        Assert.Equal(3605, index);
    }

    private int FindDistinctTokenIndex(string dataStream, int tokenLength)
    {
        var chars = dataStream.ToCharArray().ToList();
        var indexBasedTokenLength = tokenLength - 1;

        for (var i = indexBasedTokenLength; i < chars.Count; i++)
        {
            var lastChars = chars.Skip(i - indexBasedTokenLength).Take(tokenLength);
            var distinctChars = lastChars.Distinct();

            if (distinctChars.Count() == tokenLength)
            {
                return i + 1;
            }
        }

        throw new ArgumentException();
    }
}