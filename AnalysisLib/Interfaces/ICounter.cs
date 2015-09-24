namespace AnalysisLib.Interfaces
{
    /// <summary>
    /// A general purpose counter of items.
    /// </summary>
    public interface ICounter
    {
        /// <summary>
        /// Number of counted items.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Threadsafe incrementer.
        /// </summary>
        /// <param name="value">Value by which to increment the counter.</param>
        /// <returns>The new value of the counter.</returns>
        int Increment(int value = 1);

        /// <summary>
        /// Threadsafe decrementer.
        /// </summary>
        /// <param name="value">Value by which to decrement the counter.</param>
        /// <returns>The new value of the counter.</returns>
        int Decrement(int value = 1);
    }

    /// <summary>
    /// A general purpose counter of items that also computes the percentage
    /// of its count against some other counter.
    /// </summary>
    public interface IPercentCounter : ICounter
    {
        /// <summary>
        /// Number of counted items as a percentage of all items.
        /// </summary>
        float CountPercent { get; }
    }

    /// <summary>
    /// A general purpose counter of items and repeated occurrences of those items.
    /// It contains two IPercentCounter instances, one for the main count, and another
    /// for the repeated occurrences.
    /// </summary>
    public interface IRepeatCounter
    {
        IPercentCounter Counter { get; }
        IPercentCounter RepeatCounter { get; }
    }
     
}
