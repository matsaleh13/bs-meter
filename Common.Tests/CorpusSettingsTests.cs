using Formo;
using NUnit.Framework;
using System.IO;

namespace Common.Tests
{
    [TestFixture]
    public class CorpusSettingsTests
    {
        string _testAddress = "192.0.0.192";    // ya, bogus
        string _envFile = "test.env";

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
        public void CorpusSettingsTest()
        {
            dynamic config = new Configuration();

            CorpusSettings corpus = config.Bind<CorpusSettings>(new CorpusSettings(_envFile));

            Assert.IsNotNull(corpus);
            Assert.IsNotNullOrEmpty(corpus.RedisConnection);
            Assert.IsTrue(corpus.RedisConnection.Contains(_testAddress));
        }
    }
}