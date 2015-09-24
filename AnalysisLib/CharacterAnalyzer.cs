using AnalysisLib.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace AnalysisLib
{
    /// <summary>
    /// An IAnalyzer that looks for patterns in punctuation that indicate likelihood of deception.
    /// </summary>
    public class CharacterAnalyzer : IAnalyzer
    {
        /// <summary>
        /// Synchronous method to analyze the data stream.
        /// </summary>
        /// <param name="data">Data stream to analyze.</param>
        /// <param name="blockSize">Max number of characters to read at a time.</param>
        /// <returns>Result of the analysis.</returns>
        public IAnalyzerResult Analyze(Stream data, int blockSize=1024)
        {
            var result = new CharacterAnalyzerResult();
            var buffer = new char[blockSize];

            using (var reader = new StreamReader(data))
            {
                var lastCharType = CharacterType.Invalid;

                while (!reader.EndOfStream)
                {
                    var count = reader.Read(buffer, 0, blockSize);

                    result.CharacterCount.Increment(count);

                    for (int ix = 0; ix < count; ++ix)
                    {
                        var c = buffer[ix];

                        IRepeatCounter currentCounter = null;
                        var currentCharType = CharacterType.Invalid;

                        if (char.IsPunctuation(c))
                        {
                            currentCounter = result.Punctuation;
                            currentCharType = CharacterType.Punctuation;
                        }
                        else if (char.IsWhiteSpace(c))
                        {
                            currentCounter = result.Whitespace;
                            currentCharType = CharacterType.Whitespace;
                        }
                        else if (char.IsUpper(c))
                        {
                            currentCounter = result.UpperCase;
                            currentCharType = CharacterType.UpperCase;
                        }
                        else
                        {
                            currentCounter = result.Other;
                            currentCharType = CharacterType.Other;
                        }

                        currentCounter.Counter.Increment();
                        if (currentCharType == lastCharType) currentCounter.RepeatCounter.Increment();
                    }   // for
                }   // while

            }

            return result;
        }

        /// <summary>
        /// Asynchronous method to analyze the data stream.
        /// TODO: Full async internally, calling StreamReader.ReadAsync() or similar.
        /// </summary>
        /// <param name="data">Data stream to analyze.</param>
        /// <param name="blockSize">Max number of characters to read at a time.</param>
        /// <returns>Result of the analysis.</returns>
        public async Task<IAnalyzerResult> AnalyzeAsync(Stream data, int blockSize = 1024) => await Task.Run(() => Analyze(data, blockSize));
    }
}
