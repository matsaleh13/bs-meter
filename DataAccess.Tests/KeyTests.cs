using NUnit.Framework;
using System;

namespace DataAccess.Tests
{
    [TestFixture]
    public class KeyTests
    {
        [Test]
        public void DefaultCtorKeyTest()
        {
            var k = new Key();
            Assert.IsNull(k.Value);
        }

        [Test]
        public void StringCtorKeyTest()
        {
            var k = new Key("test");
            Assert.IsNotNull(k.Value);
        }

        [Test]
        public void StringCtorWithNullKeyTest()
        {
            Assert.Throws<ArgumentNullException>(delegate { var k = new Key(null); });
        }

        [Test]
        public void ToStringTest()
        {
            var k = new Key("test");

            Assert.AreEqual(k.Value, k.ToString());
        }

        [Test]
        public void DefaultCtorEqualsTest()
        {
            var k1 = new Key();
            var k2 = new Key();

            Assert.AreEqual(k1, k2);
        }

        [Test]
        public void StringCtorEqualsTest()
        {
            var k1 = new Key("test");
            var k2 = new Key("test");

            Assert.AreEqual(k1, k2);
        }

        [Test]
        public void UnequalEqualsTest()
        {
            var k1 = new Key("test");
            var k2 = new Key("TEST");

            Assert.AreNotEqual(k1, k2);
        }


        [Test]
        public void GetHashCodeTest()
        {
            var k = new Key("test");
            Assert.AreEqual(k.Value.GetHashCode(), k.GetHashCode());
        }
    }
}