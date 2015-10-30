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
    /// A general purpose counter that computes the frequency 
    /// of its count against some other counter as a percentage.
    /// </summary>
    public interface IFrequencyCounter : ICounter
    {
        /// <summary>
        /// Frequency of items as a percentage of all items.
        /// </summary>
        float Frequency { get; }
    }

    /// <summary>
    /// A general purpose counter of items and repeated occurrences of those items.
    /// It contains two IPercentCounter instances, one for the main count, and another
    /// for the repeated occurrences.
    /// </summary>
    public interface IRepeatCounter
    {
        IFrequencyCounter Counter { get; }
        IFrequencyCounter RepeatCounter { get; }
    }
     
}
