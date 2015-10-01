namespace AnalysisLib.Interfaces
{
    /// <summary>
    /// Responsible for computing a score from a single IAnalyzerResult.
    /// </summary>
    interface IScoreCalculator
    {
        /// <summary>
        /// Compute and return the score for the current IAnalyzerResult.
        /// </summary>
        /// <returns>The score, expressed as a float.</returns>
        float GetScore();
    }
}
