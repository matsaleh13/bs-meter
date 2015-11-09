using NUnit.Framework;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Tests
{
    [TestFixture]
    public class KeyScopeTests
    {
        [Test]
        public void KeyScopeOneSegmentTest()
        {
            var scope = new KeyScope("one");
            Assert.AreEqual(string.Format("{1}{0}{2}", KeyScope.Separator, KeyScope.Domain, "one"), scope.Value);
        }

        [Test]
        public void KeyScopeTwoSegmentsTest()
        {
            var scope = new KeyScope("one", "two");
            Assert.AreEqual(string.Format("{1}{0}{2}{0}{3}", KeyScope.Separator, KeyScope.Domain, "one", "two"), scope.Value);
        }

        [Test]
        public void KeyScopeThreeSegmentsTest()
        {
            var scope = new KeyScope("one", "two", "three");
            Assert.AreEqual(string.Format("{1}{0}{2}{0}{3}{0}{4}", KeyScope.Separator, KeyScope.Domain, "one", "two", "three"), scope.Value);
        }

        [Test]
        public void ToStringTest()
        {
            var scope = new KeyScope("one");
            Assert.AreEqual(string.Format("{1}{0}{2}", KeyScope.Separator, KeyScope.Domain, "one"), scope.ToString());
        }

        [Test]
        public void FromKeyTest()
        {
            var scope = KeyScope.FromKey(new Key("one:two:three:42"));
            Assert.AreEqual(string.Format("{1}{0}{2}{0}{3}{0}{4}", KeyScope.Separator, KeyScope.Domain, "one", "two", "three"), scope.Value);
        }
    }
}