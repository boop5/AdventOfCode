namespace AoC_2022_Day4
{
    public class UnitTest1
    {
        [Fact]
        public void SolvePart1()
        {
            var occurrences = 0;
            var pairs = ReadInput();

            foreach (var pair in pairs)
            {
                var splitPair = pair.Split(",");
                var range1 = splitPair[0];
                var range2 = splitPair[1];

                var splitRange1 = range1.Split("-");
                var from1 = int.Parse(splitRange1[0]);
                var to1 = int.Parse(splitRange1[1]);

                var splitRange2 = range2.Split("-");
                var from2 = int.Parse(splitRange2[0]);
                var to2 = int.Parse(splitRange2[1]);


                if (from1 <= from2 && to1 >= to2)
                {
                    occurrences++;
                }
                else if (from2 <= from1 && to2 >= to1)
                {
                    occurrences++;
                }
            }

            Assert.Equal(453, occurrences);
        }

        [Fact]
        public void SolvePart2()
        {
            var occurrences = 0;
            var pairs = ReadInput();

            foreach (var pair in pairs)
            {
                var splitPair = pair.Split(",");
                var range1 = splitPair[0];
                var range2 = splitPair[1];

                var splitRange1 = range1.Split("-");
                var from1 = int.Parse(splitRange1[0]);
                var to1 = int.Parse(splitRange1[1]);
                var sections1 = Enumerable.Range(from1, to1 - from1 + 1);

                var splitRange2 = range2.Split("-");
                var from2 = int.Parse(splitRange2[0]);
                var to2 = int.Parse(splitRange2[1]);
                var sections2 = Enumerable.Range(from2, to2 - from2 + 1);

                if (sections1.Any(sections2.Contains))
                {
                    occurrences++;
                }
            }

            Assert.Equal(919, occurrences);
        }

        private static List<string> ReadInput()
        {
            return File.ReadAllLines("input").ToList();
        }
    }
}