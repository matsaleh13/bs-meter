using AnalysisModel;

namespace AnalysisLib
{
    public static class StatsBuilder
    {
        public static CharacterStats GetStats(this CharacterAnalyzerResult result)
        {
            return new CharacterStats()
            {
                CharacterCount = result.CharacterCount.Count,

                OtherCountPercent = result.Other.Counter.Frequency,
                OtherRepeatCount = result.Other.RepeatCounter.Count,
                OtherCount = result.Other.Counter.Count,
                OtherRepeatCountPercent = result.Other.RepeatCounter.Frequency,

                PunctuationCount = result.Punctuation.Counter.Count,
                PunctuationCountPercent = result.Punctuation.Counter.Frequency,
                PunctuationRepeatCount = result.Punctuation.RepeatCounter.Count,
                PunctuationRepeatCountPercent = result.Punctuation.RepeatCounter.Frequency,

                UpperCaseCount = result.UpperCase.Counter.Count,
                UpperCaseCountPercent = result.UpperCase.Counter.Frequency,
                UpperCaseRepeatCount = result.UpperCase.RepeatCounter.Count,
                UpperCaseCountRepeatPercent = result.UpperCase.RepeatCounter.Frequency,

                WhitespaceCount = result.Whitespace.Counter.Count,
                WhitespaceCountPercent = result.Whitespace.Counter.Frequency,
                WhitespaceRepeatCount = result.Whitespace.RepeatCounter.Count,
                WhitespaceRepeatCountPercent = result.Whitespace.RepeatCounter.Frequency,
            };
        }
    }
}
