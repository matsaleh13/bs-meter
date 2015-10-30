using AnalysisLib.Interfaces;
using System.Threading;
using System;

namespace AnalysisLib
{
    /// <summary>
    /// Basic thread-safe counter implmentation using Interlocked.
    /// Public interface declares int type, but internally we use long because
    /// of Interlocked.
    /// </summary>
    public class CharacterCounter : ICounter
    {
        long _count;

        public CharacterCounter() { }

        /// <summary>
        /// Ctor that initalizes the counter to a specific value.
        /// </summary>
        /// <param name="count">The intitial value.</param>
        public CharacterCounter(int count)
        {
            _count = count;
        }

        /// <summary>
        /// Number of counted characters.
        /// Setter is private to avoid non-threadsafe use of ++ operator.
        /// </summary>
        public int Count
        {
            get
            {
                // Not required on 64-bit, but we don't know how we'll be running.
                return (int) Interlocked.Read(ref _count);
            }

            protected set
            {
                Interlocked.Exchange(ref _count, value);
            }
        }

        public int Increment(int value = 1) => (int)Interlocked.Add(ref _count, value);

        public int Decrement(int value = 1) => (int) Interlocked.Add(ref _count, -value);
    }


    public class CharacterPercentCounter : CharacterCounter, IFrequencyCounter
    {
        readonly ICounter _all;

        /// <summary>
        /// Ctor 
        /// </summary>
        /// <param name="all">An ICounter of the basis against which the percentage is calculated.</param>
        public CharacterPercentCounter(ICounter all)
        {
            _all = all;
        }

        /// <summary>
        /// Ctor that initializes the counter to a specific value.
        /// </summary>
        /// <param name="all">An ICounter of the basis against which the percentage is calculated.</param>
        /// <param name="count">The initial value.</param>
        public CharacterPercentCounter(ICounter all, int count) : base(count)
        {
            _all = all;
        }

        /// <summary>
        /// Number of counted characters as a percentage of all characters.
        /// </summary>
        public float Frequency => PercentOfCharacterCount(Count);

        protected float PercentOfCharacterCount(int count) => count == 0 ? 0 : (float)count / _all.Count * 100.0f;
    }


    public class CharacterRepeatCounter : IRepeatCounter
    {
        public CharacterRepeatCounter(ICounter all)
        {
            Counter = new CharacterPercentCounter(all);
            RepeatCounter = new CharacterPercentCounter(all);
        }

        public IFrequencyCounter Counter { get; }
        public IFrequencyCounter RepeatCounter { get; }
    }

}
