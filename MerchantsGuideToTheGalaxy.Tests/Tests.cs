using MerchantGuideToTheGalaxy;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace MerchantsGuideToTheGalaxy.Tests;

public class RomanToDecimalConverterTests
{
    [Theory]
    [InlineData("", 0)]
    [InlineData("I", 1)]
    [InlineData("V", 5)]
    [InlineData("II", 2)]
    [InlineData("IV", 4)]
    [InlineData("IX", 9)]
    [InlineData("XIV", 14)]
    [InlineData("XL", 40)]
    [InlineData("XLII", 42)]
    [InlineData("MCMXLIV", 1944)]
    public void ConvertToDecimal_ValidNumerals(string roman, int expected)
    {
        Assert.Equal(expected, new RomanToDecimalConverter().ConvertToDecimal(roman));
    }

    [Theory]
    [InlineData("IIII")]
    [InlineData("XXXX")]
    [InlineData("Z")]
    [InlineData("IZ")]
    public void ConvertToDecimal_InvalidNumerals_ReturnsMinusOne(string roman)
    {
        var sw = new StringWriter();
        var original = Console.Out;
        Console.SetOut(sw);
        try
        {
            Assert.Equal(-1, new RomanToDecimalConverter().ConvertToDecimal(roman));
        }
        finally { Console.SetOut(original); }
    }

    [Fact]
    public void Validate_RepeatedLetters_Throws()
    {
        var map = new Dictionary<char, int> { { 'I', 1 }, { 'V', 5 } };
        Assert.Throws<Exception>(() => new RomanToDecimalConverter().Validate("IIII", map));
    }

    [Fact]
    public void Validate_ValidNumeral_DoesNotThrow()
    {
        var map = new Dictionary<char, int> { { 'I', 1 }, { 'V', 5 } };
        new RomanToDecimalConverter().Validate("IV", map);
    }
}

public class MerchantGuideToGalaxyConverterTests
{
    private static string Run(params string[] input)
    {
        var sw = new StringWriter();
        var original = Console.Out;
        Console.SetOut(sw);
        try { new MerchantGuideToGalaxyConverter().MerchantGuideToGalaxyConvert(input); }
        finally { Console.SetOut(original); }
        return sw.ToString();
    }

    private static readonly string[] Setup =
    {
        "glob is I", "prok is V", "pish is X", "tegj is L",
        "glob glob Silver is 34 Credits",
        "glob prok Gold is 57800 Credits",
        "pish pish Iron is 3910 Credits",
    };

    [Fact]
    public void MetalCredits_AreComputed()
    {
        var output = Run(Setup.Append("how many Credits is glob prok Silver ?").ToArray());
        Assert.Contains("glob prok Silver  is 68", output.Replace("Silver is", "Silver  is"));
    }

    [Fact]
    public void GoldAndIron_AreComputed()
    {
        var output = Run(Setup.Concat(new[]
        {
            "how many Credits is glob prok Gold ?",
            "how many Credits is glob prok Iron ?",
        }).ToArray());
        Assert.Contains("57800", output);
        Assert.Contains("782", output);
    }

    [Fact]
    public void UnknownQuestion_PrintsFallback()
    {
        var output = Run("how much wood could a woodchuck chuck if a woodchuck could chuck wood ?");
        Assert.Contains("I have no idea what you are talking about", output);
    }

    [Fact]
    public void NonsenseStatement_PrintsFallback()
    {
        Assert.Contains("I have no idea", Run("May the force be with you"));
    }

    [Fact]
    public void HowMuchQuery_DoesNotThrow()
    {
        var output = Run(Setup.Append("how much is pish tegj glob glob ?").ToArray());
        Assert.DoesNotContain("no idea", output);
    }
}

public class ProgramTests
{
    [Fact]
    public void Main_RunsSampleInput()
    {
        var sw = new StringWriter();
        var original = Console.Out;
        Console.SetOut(sw);
        try { Program.Main(Array.Empty<string>()); }
        finally { Console.SetOut(original); }
        Assert.Contains("I have no idea what you are talking about", sw.ToString());
    }
}
