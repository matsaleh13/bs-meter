using AnalysisLib.Interfaces;
using System.Threading;

namespace AnalysisLib
{
    public class CharacterCounter : ICounter
    {
        int _count;

        /// <summary>
        /// Number of counted characters.
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }

            set
            {
                _count = value;
            }
        }

        public int CountIncrement(int value = 1) => Interlocked.Add(ref _count, value);
    }


    public class CharacterPercentCounter : CharacterCounter, ICounter, IPercentCounter
    {
        readonly ICounter _all;
        public CharacterPercentCounter(ICounter all)
        {
            _all = all;
        }

        /// <summary>
        /// Number of counted characters as a percentage of all characters.
        /// </summary>
        public float CountPercent => PercentOfCharacterCount(Count);

        protected float PercentOfCharacterCount(int count) => count == 0 ? 0 : (float)count / _all.Count * 100.0f;
    }


    public class CharacterRepeatCounter : CharacterPercentCounter, ICounter, IPercentCounter, IRepeatCounter
    {
        readonly CharacterPercentCounter _repeated;
        public CharacterRepeatCounter(ICounter all) : base(all)
        {
            _repeated = new CharacterPercentCounter(all);
        }

        /// <summary>
        /// Number of groups of repeated counted characters.
        /// </summary>
        public int RepeatedCount
        {
            get
            {
                return _repeated.Count;
            }

            set
            {
                _repeated.Count = value;
            }
        }

        public int RepeatedCountIncrement(int value = 1) => _repeated.CountIncrement(value);

        /// <summary>
        /// Repeated count as a percentage of all characters.
        /// </summary>
        public float RepeatedCountPercent => _repeated.CountPercent;
    }

}
