using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalysisLib.Interfaces
{
    public interface IAnalyzer
    {
        /// <summary>
        /// Analyzes the input Stream provided and returns the results.
        /// The return type is implementation-specific.
        /// </summary>
        /// <param name="data">Unicode string containing data to be analyzed.</param>
        /// <param name="blockSize">Max number of characters to read at a time.</param>
        /// <returns>Analysis result</returns>
        IAnalyzerResult Analyze(Stream data, int blockSize = 1024);


        /// <summary>
        /// Async wrapper for the Analyze method.
        /// </summary>
        /// <param name="data">Unicode string containing data to be analyzed.</param>
        /// <param name="blockSize">Max number of characters to read at a time.</param>
        /// <returns>Analysis result</returns>
        Task<IAnalyzerResult> AnalyzeAsync(Stream data, int blockSize = 1024);

    }
}
