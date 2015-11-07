using NUnit.Framework;
using System.IO;

namespace Common.Tests
{
    [TestFixture]
    public class GlobalConfigTests
    {
        string _envFile = "test_corpus.env";
        string _testAddress = "foo.bar.baz";

        public void CreateFile(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        [SetUp]
        public void SetUp()
        {
            CreateFile(_envFile, "REDIS_HOST=" + _testAddress);
        }

        [TearDown]
        public void TearDown()
        {
            DeleteFile(_envFile);
        }

        [Test]
        public void GlobalConfigInstanceTest()
        {
            var config = new GlobalConfig();
           
            Assert.IsNotNull(config.Corpus);
            Assert.IsNotNullOrEmpty(config.Corpus["Redis"]);
            Assert.IsTrue(config.Corpus["Redis"].Contains(_testAddress));
        }

        [Test]
        public void GlobalConfigSingletonTest()
        {
            var config = GlobalConfig.Instance;

            Assert.IsNotNull(config.Corpus);
            Assert.IsNotNullOrEmpty(config.Corpus["Redis"]);
            Assert.IsTrue(config.Corpus["Redis"].Contains(_testAddress));

            Assert.AreSame(config, GlobalConfig.Instance);
            Assert.AreNotSame(config, new GlobalConfig());
        }

    }
}