namespace UniqueNameGenerator;

public class Numeric : IWordList
{
    public static readonly Numeric WordList =
        new Numeric(1, 999, number => number.ToString("D3", provider: null));

    private readonly int _min;
    private readonly int _max;
    private readonly Func<int, string> _formatter;

    private Numeric(int min, int max, Func<int, string> formatter)
    {
        _min = min;
        _max = max;
        _formatter = formatter;
    }

    public Numeric Min(int min) =>
        new(min, _max, _formatter);

    public Numeric Max(int max) =>
        new(_min, max, _formatter);

    public Numeric Format(Func<int, string> formatter)
    {
        if (formatter is null) throw new ArgumentNullException(nameof(formatter));

        return new Numeric(_min, _max, formatter);
    }

    public string GetWord(Random random) =>
        _formatter(random.Next(_min, _max + 1)); // upper bound is exclusive
}
