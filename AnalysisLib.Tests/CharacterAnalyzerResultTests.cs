using Common.Tests;
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
            AssertUtils.AreNearlyEqual(0.10f, result.Punctuation.Counter.Frequency);
            Assert.AreEqual(2, result.Punctuation.RepeatCounter.Count);
            AssertUtils.AreNearlyEqual(0.02f, result.Punctuation.RepeatCounter.Frequency);

            result.Whitespace.Counter.Increment(10);
            result.Whitespace.RepeatCounter.Increment(2);

            Assert.AreEqual(10, result.Whitespace.Counter.Count);
            AssertUtils.AreNearlyEqual(0.10f, result.Whitespace.Counter.Frequency);
            Assert.AreEqual(2, result.Whitespace.RepeatCounter.Count);
            AssertUtils.AreNearlyEqual(0.02f, result.Whitespace.RepeatCounter.Frequency);

            result.UpperCase.Counter.Increment(10);
            result.UpperCase.RepeatCounter.Increment(2);

            Assert.AreEqual(10, result.UpperCase.Counter.Count);
            AssertUtils.AreNearlyEqual(0.10f, result.UpperCase.Counter.Frequency);
            Assert.AreEqual(2, result.UpperCase.RepeatCounter.Count);
            AssertUtils.AreNearlyEqual(0.02f, result.UpperCase.RepeatCounter.Frequency);

            result.Other.Counter.Increment(10);
            result.Other.RepeatCounter.Increment(2);

            Assert.AreEqual(10, result.Other.Counter.Count);
            AssertUtils.AreNearlyEqual(0.10f, result.Other.Counter.Frequency);
            Assert.AreEqual(2, result.Other.RepeatCounter.Count);
            AssertUtils.AreNearlyEqual(0.02f, result.Other.RepeatCounter.Frequency);
        }

    }
}