using Common.Tests;
using NUnit.Framework;

namespace AnalysisLib.Tests
{
    [TestFixture]
    public class CharacterFrequencyCounterTests : CharacterCounterTests
    {
        CharacterCounter _all;

        /// NOTE: don't use [SetUp] attribute with virtual.
        public override void SetUp()
        {
            _all = new CharacterCounter(10);

            _counter = new CharacterFrequencyCounter(_all);
        }

        [Test]
        public void CountPercentTest()
        {
            _counter.Increment();
            _counter.Increment();
            _counter.Increment();

            var percentCounter = (CharacterFrequencyCounter)_counter;
            AssertUtils.AreNearlyEqual(0.30f, percentCounter.Frequency);

            _counter.Increment(10);
            AssertUtils.AreNearlyEqual(1.30f, percentCounter.Frequency);

            _counter.Decrement(5);
            AssertUtils.AreNearlyEqual(0.80f, percentCounter.Frequency);
        }

    }
}