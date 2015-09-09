using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        int Count { get; set; }

        /// <summary>
        /// Threadsafe incrementer.
        /// </summary>
        /// <param name="value">Value by which to increment the counter.</param>
        /// <returns>The new value of the counter.</returns>
        int CountIncrement(int value = 1);

    }

    public interface IPercentCounter
    {
        /// <summary>
        /// Number of counted items as a percentage of all items.
        /// </summary>
        float CountPercent { get; }
    }


    /// <summary>
    /// A counter of items and repeated occurrances of those items.
    /// </summary>
    public interface IRepeatCounter : ICounter, IPercentCounter
    {
        /// <summary>
        /// Number of groups of repeated counted items.
        /// </summary>
        int RepeatedCount { get; set; }

        /// <summary>
        /// Threadsafe incrementer.
        /// </summary>
        /// <param name="value">Value by which to increment the counter.</param>
        /// <returns>The new value of the counter.</returns>
        int RepeatedCountIncrement(int value = 1);

        /// <summary>
        /// Repeated count as a percentage of all items.
        /// </summary>
        float RepeatedCountPercent { get; }
    }
}
