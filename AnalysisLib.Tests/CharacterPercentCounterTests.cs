using Common;
using NUnit.Framework;

namespace AnalysisLib.Tests
{
    [TestFixture]
    public class CharacterPercentCounterTests : CharacterCounterTests
    {
        CharacterCounter _all;

        public static void AreNearlyEqual(float expected, float actual)
        {
            if (!Util.NearlyEqual(expected, actual))
            {
                throw new AssertionException(string.Format("Expected: {0:G17}\nBut was: {1:G17}", expected, actual));
            }
        }

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
            AreNearlyEqual(0.30f, percentCounter.Frequency);

            _counter.Increment(10);
            AreNearlyEqual(1.30f, percentCounter.Frequency);

            _counter.Decrement(5);
            AreNearlyEqual(0.80f, percentCounter.Frequency);
        }

    }
}