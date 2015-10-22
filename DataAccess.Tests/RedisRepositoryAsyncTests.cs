using DataAccess.Interfaces;
using NUnit.Framework;
using System.Threading.Tasks;
using System;
using StackExchange.Redis;

namespace DataAccess.Tests
{
    [TestFixture]
    public class RedisRepositoryAsyncTests
    {
        #region Model

        class TestEntity : IEntity
        {
            public string Key { set; get; }

            public string Name { set; get; }
            public string Desc { set; get; }
        }

        readonly string TestKey0 = "tk0";
        #endregion

        //ConnectionMultiplexer _redis;

        //[TestFixtureSetUp]
        //public void OneTimeSetUp()
        //{
        //    ConfigurationOptions options = 
        //    _redis = ConnectionMultiplexer.Connect("localhost");
        //}

        //[TestFixtureTearDown]
        //public void OneTimeTearDown()
        //{
        //    _redis.Dispose();
        //    _redis = null;
        //}


        //[Test]
        //public void GetAsyncTest()
        //{
        //    var entity = new TestEntity()
        //    {
        //        Key = TestKey0,
        //        Name = "foo",
        //        Desc = "This is a foo."
        //    };

        //    Task.Run(async () => { await } );
        //    Assert.Fail();
        //}

        //[Test]
        //public void AddAsyncTest()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void DeleteAsyncTest()
        //{
        //    Assert.Fail();
        //}

        //[Test]
        //public void SaveAsyncTest()
        //{
        //    Assert.Fail();
        //}
    }
}