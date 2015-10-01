namespace AnalysisLib.Interfaces
{
    /// <summary>
    /// Responsible for aggregating scores derived from one or more IAnalysisResult
    /// instances, to come up with a unified and normalized score for the document.
    /// </summary>
    interface IScoreKeeper: IScoreCalculator
    {
        /// <summary>
        /// The document being scored.
        /// </summary>
        IDocument Document { get; }

        /// <summary>
        /// Add a single score calculator. 
        /// </summary>
        /// <param name="calculator"></param>
        void AddScorer(IScoreCalculator calculator);
    }
}
