using AnalysisLib;
using NUnit.Framework;

namespace AnalysisLib.Tests
{
    [TestFixture]
    public class CharacterPercentCounterTests : CharacterCounterTests
    {
        CharacterCounter _all;

        /// NOTE: don't use [SetUp] attribute with virtual.
        public override void SetUp()
        {
            _all = new CharacterCounter(10);

            _counter = new CharacterPercentCounter(_all);
        }

        [Test]
        public void CountPercentTest()
        {
            _counter.Increment();
            _counter.Increment();
            _counter.Increment();

            var percentCounter = (CharacterPercentCounter)_counter;
            Assert.AreEqual(30.0f, percentCounter.CountPercent);

            _counter.Increment(10);
            Assert.AreEqual(130.0f, percentCounter.CountPercent);

            _counter.Decrement(5);
            Assert.AreEqual(80.0f, percentCounter.CountPercent);
        }

    }
}