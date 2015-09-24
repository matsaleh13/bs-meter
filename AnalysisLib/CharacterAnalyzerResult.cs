using System;
using AnalysisLib.Interfaces;
using System.Threading;

namespace AnalysisLib
{
    public enum CharacterType
    {
        Invalid,
        Punctuation,
        Whitespace,
        UpperCase,
        Other
    }


    public class CharacterAnalyzerResult : IAnalyzerResult
    {

        public CharacterAnalyzerResult()
        {
            CharacterCount = new CharacterCounter();

            Punctuation = new CharacterRepeatCounter(CharacterCount);
            Whitespace = new CharacterRepeatCounter(CharacterCount);
            UpperCase = new CharacterRepeatCounter(CharacterCount);
            Other = new CharacterRepeatCounter(CharacterCount);
        }

        /// <summary>
        /// Total number of characters.
        /// </summary>
        public ICounter CharacterCount { get; }

        public IRepeatCounter Other { get; }

        public IRepeatCounter Punctuation { get; }

        public IRepeatCounter UpperCase { get; }

        public IRepeatCounter Whitespace { get; }

    }
}
