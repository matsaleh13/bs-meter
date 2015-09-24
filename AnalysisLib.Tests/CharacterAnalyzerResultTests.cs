using NUnit.Framework;

namespace AnalysisLib.Tests
{
    [TestFixture]
    public class CharacterAnalyzerResultTests
    {
        [Test]
        public void CharacterAnalyzerResultTest()
        {
            var result = new CharacterAnalyzerResult();

            result.CharacterCount.Increment(100);

            result.Punctuation.Counter.Increment(10);
            result.Punctuation.RepeatCounter.Increment(2);

            Assert.AreEqual(10, result.Punctuation.Counter.Count);
            Assert.AreEqual(10.0f, result.Punctuation.Counter.CountPercent);
            Assert.AreEqual(2, result.Punctuation.RepeatCounter.Count);
            Assert.AreEqual(2.0f, result.Punctuation.RepeatCounter.CountPercent);

            result.Whitespace.Counter.Increment(10);
            result.Whitespace.RepeatCounter.Increment(2);

            Assert.AreEqual(10, result.Whitespace.Counter.Count);
            Assert.AreEqual(10.0f, result.Whitespace.Counter.CountPercent);
            Assert.AreEqual(2, result.Whitespace.RepeatCounter.Count);
            Assert.AreEqual(2.0f, result.Whitespace.RepeatCounter.CountPercent);

            result.UpperCase.Counter.Increment(10);
            result.UpperCase.RepeatCounter.Increment(2);

            Assert.AreEqual(10, result.UpperCase.Counter.Count);
            Assert.AreEqual(10.0f, result.UpperCase.Counter.CountPercent);
            Assert.AreEqual(2, result.UpperCase.RepeatCounter.Count);
            Assert.AreEqual(2.0f, result.UpperCase.RepeatCounter.CountPercent);

            result.Other.Counter.Increment(10);
            result.Other.RepeatCounter.Increment(2);

            Assert.AreEqual(10, result.Other.Counter.Count);
            Assert.AreEqual(10.0f, result.Other.Counter.CountPercent);
            Assert.AreEqual(2, result.Other.RepeatCounter.Count);
            Assert.AreEqual(2.0f, result.Other.RepeatCounter.CountPercent);
        }

    }
}