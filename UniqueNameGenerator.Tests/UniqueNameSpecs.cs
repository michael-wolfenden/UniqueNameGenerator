using System.Text.RegularExpressions;
using Shouldly;
using UniqueNameGenerator;

namespace UniqueNameGenerator.Tests;

public class UniqueNameSpecs
{
    [Fact]
    public void throws_if_word_list_is_null()
    {
        Should.Throw<ArgumentNullException>(() =>
        {
            _ = new UniqueName(null!);
        });
    }

    [Fact]
    public void throws_if_word_lists_is_null()
    {
        Should.Throw<ArgumentNullException>(() =>
        {
            _ = new UniqueName(Animals.WordList, null!);
        });
    }

    [Fact]
    public void throws_if_separator_is_null()
    {
        Should.Throw<ArgumentNullException>(() =>
        {
            _ = new UniqueName(Animals.WordList).Separator(null!);
        });
    }

    [Fact]
    public void throws_if_formatter_is_null()
    {
        Should.Throw<ArgumentNullException>(() =>
        {
            _ = new UniqueName(Animals.WordList).Format(null!);
        });
    }

    [Fact]
    public void throws_if_style_is_invalid()
    {
        Should.Throw<ArgumentOutOfRangeException>(() =>
        {
            _ = new UniqueName(Animals.WordList).Format((Style)int.MaxValue);
        });
    }

    [Fact]
    public void returns_a_random_name_given_a_single_word_list()
    {
        var uniqueNameGenerator = new UniqueName(new StaticWordList("a"));

        uniqueNameGenerator.Generate().ShouldBe("a");
    }

    [Fact]
    public void returns_a_random_name_given_multiple_word_lists()
    {
        var uniqueNameGenerator = new UniqueName(
            new StaticWordList("a"),
            new StaticWordList("b"),
            new StaticWordList("c"));

        uniqueNameGenerator.Generate().ShouldBe("a_b_c");
    }

    [Fact]
    public void returns_random_combinations()
    {
        var uniqueNameGenerator = new UniqueName(
            new StaticWordList("Adjective1", "Adjective2", "Adjective3"),
            new StaticWordList("Color1", "Color2", "Color3"),
            new StaticWordList("Animal1", "Animal2", "Animal3"));

        uniqueNameGenerator.Generate().ShouldMatch("^Adjective[123]_Color[123]_Animal[123]$");
    }

    [Fact]
    public void accepts_a_custom_separator_word()
    {
        var uniqueNameGenerator = new UniqueName(
                new StaticWordList("a"),
                new StaticWordList("b"),
                new StaticWordList("c")
            )
            .Separator("|SPACE|");

        uniqueNameGenerator.Generate().ShouldBe("a|SPACE|b|SPACE|c");
    }

    [Fact]
    public void accepts_a_blank_separator()
    {
        var uniqueNameGenerator = new UniqueName(
                new StaticWordList("a"),
                new StaticWordList("b"),
                new StaticWordList("c")
            )
            .Separator("");

        uniqueNameGenerator.Generate().ShouldBe("abc");
    }

    [Fact]
    public void returns_the_same_name_given_an_equal_seed()
    {
        var uniqueNameGenerator = new UniqueName(
                Colors.WordList,
                Adjectives.WordList,
                Animals.WordList
            )
            .Seed(120498);

        var expected = "turquoise_critical_hornet";

        uniqueNameGenerator.Generate().ShouldBe(expected);
        uniqueNameGenerator.Generate().ShouldBe(expected);
        uniqueNameGenerator.Generate().ShouldBe(expected);
    }

    [Fact]
    public void returns_the_same_name_given_an_equal_string_seed()
    {
        var uniqueNameGenerator = new UniqueName(
                Colors.WordList,
                Adjectives.WordList,
                Animals.WordList
            )
            .Seed("seed as a string");

        var expected = "tan_sour_ermine";

        uniqueNameGenerator.Generate().ShouldBe(expected);
        uniqueNameGenerator.Generate().ShouldBe(expected);
        uniqueNameGenerator.Generate().ShouldBe(expected);
    }

    [Fact]
    public void does_not_alter_the_words_when_a_format_is_not_provided()
    {
        var uniqueNameGenerator = new UniqueName(
            new StaticWordList("UPPERCASE"),
            new StaticWordList("lowercase"),
            new StaticWordList("Capitalized"));

        uniqueNameGenerator.Generate().ShouldMatch("UPPERCASE_lowercase_Capitalized");
    }

    [Fact]
    public void returns_a_lower_case_formatted_name_when_style_is_set_to_lower_case()
    {
        var uniqueNameGenerator = new UniqueName(
                new StaticWordList("UPPERCASE"),
                new StaticWordList("lowercase"),
                new StaticWordList("Capitalized")
            )
            .Format(Style.LowerCase);

        uniqueNameGenerator.Generate().ShouldMatch("uppercase_lowercase_capitalized");
    }

    [Fact]
    public void returns_an_upper_case_formatted_name_when_style_is_set_to_upper_case()
    {
        var uniqueNameGenerator = new UniqueName(
                new StaticWordList("UPPERCASE"),
                new StaticWordList("lowercase"),
                new StaticWordList("Capitalized")
            )
            .Format(Style.UpperCase);

        uniqueNameGenerator.Generate().ShouldMatch("UPPERCASE_LOWERCASE_CAPITALIZED");
    }

    [Fact]
    public void returns_a_title_case_formatted_name_when_style_is_set_to_title_case()
    {
        var uniqueNameGenerator = new UniqueName(
                new StaticWordList("UPPERCASE"),
                new StaticWordList("lowercase"),
                new StaticWordList("Capitalized")
            )
            .Format(Style.TitleCase);

        uniqueNameGenerator.Generate().ShouldMatch("Uppercase_Lowercase_Capitalized");
    }

    [Fact]
    public void accepts_a_custom_format()
    {
        var uniqueNameGenerator = new UniqueName(
                new StaticWordList("UPPERCASE"),
                new StaticWordList("lowercase"),
                new StaticWordList("Capitalized")
            )
            .Format(word => Regex.Replace(word, "e", "!", RegexOptions.IgnoreCase));

        uniqueNameGenerator.Generate().ShouldMatch("UPP!RCAS!_low!rcas!_Capitaliz!d");
    }

    [Fact]
    public void numeric_word_list_returns_a_number()
    {
        var uniqueNameGenerator = new UniqueName(
                Colors.WordList,
                Adjectives.WordList,
                Animals.WordList,
                Numeric.WordList
            )
            .Seed(1);

        uniqueNameGenerator.Generate().ShouldMatch("brown_classic_kiwi_771");
    }

    [Fact]
    public void numeric_word_list_pads_numbers_by_default()
    {
        var uniqueNameGenerator = new UniqueName(
                Colors.WordList,
                Adjectives.WordList,
                Animals.WordList,
                Numeric.WordList.Min(1).Max(9)
            )
            .Seed(1);

        uniqueNameGenerator.Generate().ShouldMatch("brown_classic_kiwi_007");
    }

    [Fact]
    public void numeric_word_list_allows_changing_the_format()
    {
        var uniqueNameGenerator = new UniqueName(
                Colors.WordList,
                Adjectives.WordList,
                Animals.WordList,
                Numeric.WordList.Format(number => number.ToString("D6"))
            )
            .Seed(1);

        uniqueNameGenerator.Generate().ShouldMatch("brown_classic_kiwi_000771");
    }

    [Fact]
    public void numeric_word_list_throws_if_formatter_is_null()
    {
        Should.Throw<ArgumentNullException>(() =>
        {
            _ = Numeric.WordList.Format(null!);
        });
    }

    private class StaticWordList(params string[] words) : IWordList
    {
        public string GetWord(Random random) =>
            words[new Random().Next(0, words.Length)];
    }
}
