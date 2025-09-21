using System.Globalization;

namespace UniqueNameGenerator;

public class UniqueName
{
    private readonly IWordList[] _wordLists;
    private string _separator = "_";
    private int? _seed;
    private Func<string, string> _formatter = word => word;

    public UniqueName(IWordList wordList, params IWordList[] wordLists)
    {
        if (wordList is null) throw new ArgumentNullException(nameof(wordList));
        if (wordLists is null) throw new ArgumentNullException(nameof(wordLists));

        _wordLists = [wordList, ..wordLists];
    }

    public string Generate()
    {
        var random = _seed is null ? new Random() : new Random(_seed.Value);
        var words = _wordLists.Select(wordList => _formatter(wordList.GetWord(random)));
        return string.Join(_separator, words);
    }

    public UniqueName Separator(string separator)
    {
        _separator = separator ?? throw new ArgumentNullException(nameof(separator));

        return this;
    }

    public UniqueName Seed(int seed)
    {
        _seed = seed;
        return this;
    }

    public UniqueName Seed(string seed)
    {
        _seed = GetDeterministicHashCode(seed);
        return this;

        // see: https://andrewlock.net/why-is-string-gethashcode-different-each-time-i-run-my-program-in-net-core/#a-deterministic-gethashcode-implementation
        static int GetDeterministicHashCode(string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                    {
                        break;
                    }

                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }
    }

    public UniqueName Format(Func<string, string> formatter)
    {
        _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        return this;
    }

    public UniqueName Format(Style style)
    {
        _formatter = GetFormatterForStyle(style);
        return this;

        static Func<string, string> GetFormatterForStyle(Style style) => style switch
        {
            Style.LowerCase => word => CultureInfo.CurrentCulture.TextInfo.ToLower(word),
            Style.UpperCase => word => CultureInfo.CurrentCulture.TextInfo.ToUpper(word),
            Style.TitleCase => word => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.TextInfo.ToLower(word)),
            _ => throw new ArgumentOutOfRangeException(nameof(style), style, message: null),
        };
    }
}
