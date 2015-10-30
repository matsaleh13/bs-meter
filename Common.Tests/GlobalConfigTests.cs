using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class GlobalConfigTests
    {
        [Test]
        public void GlobalConfigInstanceTest()
        {
            var config = new GlobalConfig();
           
            Assert.IsNotNull(config.Corpus);
            Assert.IsNotNullOrEmpty(config.Corpus.Redis.ConnectionString);
            Assert.AreEqual("{HOST}:6379,allowAdmin=true,ssl=false,connectTimeout=5000,database=0,password=null", config.Corpus.Redis.ConnectionString);
        }

        [Test]
        public void GlobalConfigSingletonTest()
        {
            var config = GlobalConfig.Instance;

            Assert.IsNotNull(config.Corpus);
            Assert.IsNotNullOrEmpty(config.Corpus.Redis.ConnectionString);
            Assert.AreEqual("{HOST}:6379,allowAdmin=true,ssl=false,connectTimeout=5000,database=0,password=null", config.Corpus.Redis.ConnectionString);

            Assert.AreSame(config, GlobalConfig.Instance);
            Assert.AreNotSame(config, new GlobalConfig());
        }

    }
}