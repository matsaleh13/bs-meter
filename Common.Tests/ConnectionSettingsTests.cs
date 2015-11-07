using Formo;
using NUnit.Framework;
using System.IO;

namespace Common.Tests
{
    [TestFixture]
    public class ConnectionSettingsTests
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
        public void ConnectionSettingsTest()
        {
            dynamic config = new Configuration();

            ConnectionSettings settings = config.Bind<ConnectionSettings>(new ConnectionSettings(_envFile));

            Assert.IsNotNull(settings);
            Assert.IsNotNullOrEmpty(settings["Redis"]);
            Assert.IsTrue(settings["Redis"].Contains(_testAddress));
        }
    }
}