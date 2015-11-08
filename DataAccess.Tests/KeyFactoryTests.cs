using NUnit.Framework;
using System.Collections.Generic;

namespace DataAccess.Tests
{
    [TestFixture]
    public class KeyFactoryTests
    {
        List<Key> _keys = new List<Key>();
        private void AddKey(Key key)
        {
            _keys.Add(key);
        }

        private void DeleteKeys()
        {
            var db = RedisConnectionManager.Instance.GetConnection("Redis").GetDatabase();

            foreach (var key in _keys)
            {
                var scope = KeyScope.FromKey(key);
                db.KeyDelete(scope.NextIdKey);
            }
        }


        [SetUp]
        public void SetUp()
        {

        }

        [TearDown]
        public void TearDown()
        {
            DeleteKeys();
        }

        [Test]
        public void CreateKeyWithKeyScopeAndIdTest()
        {
            var key = KeyFactory.Instance.CreateKey(new KeyScope("one"), 42);
            AddKey(key);

            Assert.AreEqual(string.Format("{1}{0}{2}{0}{3}", KeyScope.Separator, KeyScope.Domain, "one", 42), key.Value);
        }

        [Test]
        public void CreateKeyWithStringScopeAndIdTest()
        {
            var key = KeyFactory.Instance.CreateKey("one", 42);
            AddKey(key);

            Assert.AreEqual(string.Format("{1}{0}{2}{0}{3}", KeyScope.Separator, KeyScope.Domain, "one", 42), key.Value);
        }

        [Test]
        public void CreateKeyWithStringScopeAndId2Test()
        {
            var key = KeyFactory.Instance.CreateKey("one", "two", 42.ToString());
            AddKey(key);

            Assert.AreEqual(string.Format("{1}{0}{2}{0}{3}{0}{4}", KeyScope.Separator, KeyScope.Domain, "one", "two", 42), key.Value);
        }

        [Test]
        public void CreateKeyWithStringScopeAndId3Test()
        {
            var key = KeyFactory.Instance.CreateKey("one", "two", "three", 42.ToString());
            AddKey(key);

            Assert.AreEqual(string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}", KeyScope.Separator, KeyScope.Domain, "one", "two", "three", 42), key.Value);
        }

        [Test]
        public void CreateKeyWithKeyScopeTest()
        {
            var key = KeyFactory.Instance.CreateKey(new KeyScope("one"));
            AddKey(key);

            Assert.AreEqual(string.Format("{1}{0}{2}{0}{3}", KeyScope.Separator, KeyScope.Domain, "one", 1), key.Value);
        }

        [Test]
        public void CreateKeyWithStringScopeTest()
        {
            var key = KeyFactory.Instance.CreateKey("one");
            AddKey(key);

            Assert.AreEqual(string.Format("{1}{0}{2}{0}{3}", KeyScope.Separator, KeyScope.Domain, "one", 1), key.Value);
        }

        [Test]
        public void CreateKeyWithStringScope2Test()
        {
            var key = KeyFactory.Instance.CreateKey("one", "two");
            AddKey(key);

            Assert.AreEqual(string.Format("{1}{0}{2}{0}{3}{0}{4}", KeyScope.Separator, KeyScope.Domain, "one", "two", 1), key.Value);
        }

        [Test]
        public void CreateKeyWithStringScope3Test()
        {
            var key = KeyFactory.Instance.CreateKey("one", "two", "three");
            AddKey(key);

            Assert.AreEqual(string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}", KeyScope.Separator, KeyScope.Domain, "one", "two", "three", 1), key.Value);
        }

    }
}