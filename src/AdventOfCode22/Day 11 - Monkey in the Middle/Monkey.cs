namespace AdventOfCode22.Day11;

internal class Monkey
{
    private Monkey(IEnumerable<long> items,
                   Func<long, long> executeOperation,
                   Func<bool, int> throwTo,
                   int divisor)
    {
        Items = new Queue<long>(items);
        ExecuteOperation = executeOperation;
        ThrowTo = throwTo;
        Divisor = divisor;
    }

    public Queue<long> Items { get; }
    public Func<long, long> ExecuteOperation { get; }
    public Func<bool, int> ThrowTo { get; }
    public int CountInspectedItems { get; set; }
    public int Divisor { get; }

    public bool Test(long i)
    {
        return i % Divisor == 0;
    }

    public static Monkey CreateFromDescription(IReadOnlyList<string> description)
    {
        Func<long, long> GetOperation()
        {
            const string txt = "Operation: new = old ";
            var line3 = description[2];
            var opText = line3[txt.Length..];
            var method = opText[0];
            var value = opText[2..];

            long GetValue(long i, string v)
            {
                if (long.TryParse(v, out var n))
                    return n;
                if (v == "old")
                    return i;
                throw new ArgumentException();
            }

            return oldValue =>
            {
                return method switch
                {
                '+' => oldValue + GetValue(oldValue, value),
                '*' => oldValue * GetValue(oldValue, value),
                _ => throw new ArgumentException()
                };
            };
        }

        Func<bool, int> GetThrowTo()
        {
            int Get(int line)
            {
                return int.Parse(description[line - 1].Split("throw to monkey")[1]);
            }

            var trueValue = Get(5);
            var falseValue = Get(6);

            return x => x ? trueValue : falseValue;
        }

        int GetDivisor()
        {
            const string txt = "Test: divisible by ";
            var line4 = description[3];
            var numberText = line4[(txt.Length - 1)..];
            var number = int.Parse(numberText);

            return number;
        }

        IEnumerable<long> GetItems()
        {
            const string txt = "Starting items: ";
            var line2 = description[1];
            var numbersText = line2[(txt.Length - 1)..].Split(",");
            var numbers = numbersText.Select(long.Parse);

            return numbers.ToImmutableList();
        }

        return new Monkey(GetItems(),
                          GetOperation(),
                          GetThrowTo(),
                          GetDivisor());
    }
}