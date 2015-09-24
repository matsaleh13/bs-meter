using AnalysisLib.Interfaces;
using NUnit.Framework;

namespace AnalysisLib.Tests
{
    [TestFixture]
    public class CharacterCounterTests
    {
        protected ICounter _counter;

        [SetUp]
        public virtual void SetUp()
        {
            _counter = new CharacterCounter();
        }

        [Test]
        public void CountTest()
        {
            Assert.AreEqual(0, _counter.Count);

            var counter = new CharacterCounter(10);
            Assert.AreEqual(10, counter.Count);
        }

        [Test]
        public void IncrementTest()
        {
            _counter.Increment();
            Assert.AreEqual(1, _counter.Count);

            _counter.Increment(10);
            Assert.AreEqual(11, _counter.Count);

            _counter.Increment(-11);
            Assert.AreEqual(0, _counter.Count);
        }

        [Test]
        public void DecrementTest()
        {
            _counter.Decrement();
            Assert.AreEqual(-1, _counter.Count);

            _counter.Decrement(10);
            Assert.AreEqual(-11, _counter.Count);

            _counter.Increment(11);
            Assert.AreEqual(0, _counter.Count);
        }

    }
}