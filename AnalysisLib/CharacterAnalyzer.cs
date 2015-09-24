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
        private CharacterType AnalyzeBuffer(char[] buffer, int count, CharacterAnalyzerResult result, CharacterType lastCharType)
        {
            result.CharacterCount.Increment(count);

            for (int ix = 0; ix < count; ++ix)
            {
                var c = buffer[ix];

                var currentCharType = CharacterTypeUtils.GetCharType(c);
                var currentCounter = result.GetCounter(currentCharType);

                currentCounter.Counter.Increment();
                if (currentCharType == lastCharType) currentCounter.RepeatCounter.Increment();

                lastCharType = currentCharType;
            }   // for

            return lastCharType;
        }

        /// <summary>
        /// Synchronous method to analyze the data stream.
        /// </summary>
        /// <param name="data">Data stream to analyze.</param>
        /// <param name="blockSize">Max number of characters to read at a time.</param>
        /// <returns>Result of the analysis.</returns>
        public IAnalyzerResult Analyze(Stream data, int blockSize = 1024)
        {
            var result = new CharacterAnalyzerResult();
            var buffer = new char[blockSize];

            using (var reader = new StreamReader(data))
            {
                var lastCharType = CharacterType.Invalid;

                while (!reader.EndOfStream)
                {
                    var count = reader.Read(buffer, 0, blockSize);

                    lastCharType = AnalyzeBuffer(buffer, count, result, lastCharType);
                }   
            }

            return result;
        }

        /// <summary>
        /// Asynchronous method to analyze the data stream.
        /// </summary>
        /// <param name="data">Data stream to analyze.</param>
        /// <param name="blockSize">Max number of characters to read at a time.</param>
        /// <returns>Result of the analysis.</returns>
        public async Task<IAnalyzerResult> AnalyzeAsync(Stream data, int blockSize = 1024)
        {
            var result = new CharacterAnalyzerResult();
            var buffer = new char[blockSize];

            using (var reader = new StreamReader(data))
            {
                var lastCharType = CharacterType.Invalid;

                while (!reader.EndOfStream)
                {
                    var count = await reader.ReadAsync(buffer, 0, blockSize).ConfigureAwait(false);

                    lastCharType = AnalyzeBuffer(buffer, count, result, lastCharType);
                }   
            }

            return result;
        }
    }
}
