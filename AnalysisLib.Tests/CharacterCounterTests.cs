using NUnit.Framework;

namespace AnalysisLib.Tests
{
    [TestFixture]
    public class CharacterCounterTests
    {
        [Test]
        public void CountTest()
        {
            var counter = new CharacterCounter();
            Assert.AreEqual(0, counter.Count);

            counter.Count++;
            Assert.AreEqual(1, counter.Count);

            counter.Count--;
            Assert.AreEqual(0, counter.Count);
        }

        [Test]
        public void CountIncrementTest()
        {
            var counter = new CharacterCounter();

            counter.CountIncrement();
            Assert.AreEqual(1, counter.Count);

            counter.CountIncrement(10);
            Assert.AreEqual(11, counter.Count);

            counter.CountIncrement(-11);
            Assert.AreEqual(0, counter.Count);
        }
    }
}