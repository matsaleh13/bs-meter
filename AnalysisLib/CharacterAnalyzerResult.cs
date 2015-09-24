using System;
using AnalysisLib.Interfaces;
using System.Threading;

namespace AnalysisLib
{
    class InvalidCharacterTypeException : ApplicationException
    {
        public InvalidCharacterTypeException()
        {

        }

        public InvalidCharacterTypeException(string message)
        : base(message)
        {
        }

        public InvalidCharacterTypeException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }

    public enum CharacterType
    {
        Invalid,
        Punctuation,
        Whitespace,
        UpperCase,
        Other
    }

    public class CharacterTypeUtils
    {
        public static CharacterType GetCharType(char c)
        {
            var currentCharType = CharacterType.Invalid;

            if (char.IsPunctuation(c))
            {
                currentCharType = CharacterType.Punctuation;
            }
            else if (char.IsWhiteSpace(c))
            {
                currentCharType = CharacterType.Whitespace;
            }
            else if (char.IsUpper(c))
            {
                currentCharType = CharacterType.UpperCase;
            }
            else
            {
                currentCharType = CharacterType.Other;
            }

            return currentCharType;
        }
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

        public IRepeatCounter GetCounter(CharacterType charType)
        {
            switch (charType)
            {
                case CharacterType.Punctuation:
                    return Punctuation;
                case CharacterType.Whitespace:
                    return Whitespace;
                case CharacterType.UpperCase:
                    return UpperCase;
                case CharacterType.Other:
                    return Other;
                default:
                    throw new InvalidCharacterTypeException(string.Format("Invalid character type {0}", charType));
            }
        }
    }
}
