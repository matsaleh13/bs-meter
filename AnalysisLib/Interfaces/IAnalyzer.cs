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
        /// <returns>Analysis result</returns>
        IAnalyzerResult Analyze(Stream data);


        /// <summary>
        /// Async wrapper for the Analyze method.
        /// </summary>
        /// <param name="data">Unicode string containing data to be analyzed.</param>
        /// <returns>Analysis result</returns>
        Task<IAnalyzerResult> AnalyzeAsync(Stream data);

    }
}
