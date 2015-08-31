using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisLib.Interfaces;

namespace AnalysisLib
{
    /// <summary>
    /// An IAnalyzer that looks for patterns in punctuation that indicate likelihood of deception.
    /// </summary>
    public class CharacterAnalyzer : IAnalyzer
    {
        public IAnalyzerResult Analyze(Stream data)
        {
            var result = new CharacterAnalyzerResult();

            using (var reader = new StreamReader(data))
            {
                var lastCharType = CharacterType.Invalid;

                while (!reader.EndOfStream)
                {
                    var c = reader.Read();

                    ++result.CharacterCount;

                    CharacterCounter currentCounter = null;

                    if (char.IsPunctuation((char)c))
                    {
                        currentCounter = result.Punctuation;
                    }
                    else if (char.IsWhiteSpace((char)c))
                    {
                        currentCounter = result.Whitespace;
                    }
                    else if (char.IsUpper((char)c))
                    {
                        currentCounter = result.UpperCase;
                    }
                    else
                    {
                        currentCounter = result.Other;
                    }

                    currentCounter.Update(lastCharType);
                    lastCharType = currentCounter.CharType;
                }
            }

            return result;
        }

        public async Task<IAnalyzerResult> AnalyzeAsync(Stream data)
        {
            return await Task.Run(() => Analyze(data));
        }
    }
}
