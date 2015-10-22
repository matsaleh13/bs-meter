using NUnit.Framework;

namespace DataAccess.Tests
{
    [TestFixture]
    public class PropertyInfoCacheTests
    {
        public class Test1
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }

        public class Test2
        {
            public Test1 Test { get; set; }
        }

        public class Test3<T>
        {
            public T TestT { get; set; }
        }

        public class Test4 : Test3<Test4>
        {
            public string Desc { get; set; }
        }

        [Test]
        public void PropertyInfoCacheTest1()
        {
            Assert.Contains("Name", PropertyInfoCache<Test1>.Value.Keys);
            Assert.Contains("Id", PropertyInfoCache<Test1>.Value.Keys);
        }

        [Test]
        public void PropertyInfoCacheTest2()
        {
            Assert.Contains("Test", PropertyInfoCache<Test2>.Value.Keys);
        }

        [Test]
        public void PropertyInfoCacheTest3()
        {
            Assert.Contains("TestT", PropertyInfoCache<Test3<int>>.Value.Keys);
        }

        [Test]
        public void PropertyInfoCacheTest4()
        {
            Assert.Contains("Desc", PropertyInfoCache<Test4>.Value.Keys);
        }

    }
}
