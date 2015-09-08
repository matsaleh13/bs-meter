using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisLib
{
    internal enum CharacterType
    {
        Invalid,
        Punctuation,
        Whitespace,
        UpperCase,
        Other
    }


    internal class CharacterCounter
    {
        public CharacterCounter(CharacterAnalyzerResult container, CharacterType charType)
        {
            this.container = container;
            CharType = charType;
        }

        public void Update(CharacterType lastCharType)
        {
            ++Count;
            if (lastCharType == CharType)
            {
                // Repeating
                ++RepeatedCount;
            }
        }

        private CharacterAnalyzerResult container;

        public CharacterType CharType { get; private set; }

        /// <summary>
        /// Number of counted characters.
        /// </summary>
        public int Count;

        /// <summary>
        /// Number of counted characters as a percentage of all characters.
        /// </summary>
        public float CountPercent => (float)Count / container.CharacterCount * 100.0f;

        /// <summary>
        /// Number of groups of repeated counted characters.
        /// </summary>
        public int RepeatedCount;

        /// <summary>
        /// Repeated count as a percentage of all characters.
        /// </summary>
        public float RepeatedCountPercent => (float)RepeatedCount / container.CharacterCount * 100.0f;
    }

    internal class CharacterAnalyzerResult : Interfaces.IAnalyzerResult
    {
        public CharacterAnalyzerResult()
        {
            Punctuation = new CharacterCounter(this, CharacterType.Punctuation);
            Whitespace = new CharacterCounter(this, CharacterType.Whitespace);
            UpperCase = new CharacterCounter(this, CharacterType.UpperCase);
            Other = new CharacterCounter(this, CharacterType.Other);
        }

        /// <summary>
        /// Total number of characters.
        /// </summary>
        public int CharacterCount;

        public CharacterCounter Punctuation { get; private set; }

        public CharacterCounter Whitespace { get; private set; }

        public CharacterCounter UpperCase { get; private set; }

        public CharacterCounter Other { get; private set; }

    }
}
