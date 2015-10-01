using System;
using AnalysisLib.Interfaces;

namespace AnalysisLib
{
    /// <summary>
    /// Responsible for computing a score from a CharacterAnalyzerResult.
    /// </summary>
    public class CharacterScoreCalculator : IScoreCalculator
    {
        readonly CharacterAnalyzerResult _result;
        float _score;

        public CharacterScoreCalculator(CharacterAnalyzerResult result)
        {
            _result = result;
        }

        public float GetScore()
        {
            // Document size

            // Punctuation

            // Whitespace

            // UpperCase

            // Other


            return _score;
        }
    }
}
