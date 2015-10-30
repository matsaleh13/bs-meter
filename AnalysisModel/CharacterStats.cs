using DataAccess;
using DataAccess.Interfaces;

namespace AnalysisModel
{
    public class CharacterStats : IEntity
    {
        public Key Key { get; set; }

        public int CharacterCount { get; set; }

        public int OtherCount { get; set; }
        public float OtherCountPercent { get; set; }
        public int OtherRepeatCount { get; set; }
        public float OtherRepeatCountPercent { get; set; }

        public int PunctuationCount { get; set; }
        public float PunctuationCountPercent { get; set; }
        public int PunctuationRepeatCount { get; set; }
        public float PunctuationRepeatCountPercent { get; set; }

        public int UpperCaseCount { get; set; }
        public float UpperCaseCountPercent { get; set; }
        public int UpperCaseRepeatCount { get; set; }
        public float UpperCaseCountRepeatPercent { get; set; }

        public int WhitespaceCount { get; set; }
        public float WhitespaceCountPercent { get; set; }
        public int WhitespaceRepeatCount { get; set; }
        public float WhitespaceRepeatCountPercent { get; set; }
    }
}
