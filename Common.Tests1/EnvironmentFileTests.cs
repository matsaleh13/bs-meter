using NUnit.Framework;
using System;
using System.IO;

namespace Common.Tests
{
    [TestFixture]
    public class EnvironmentFileTests
    {
        const string _validFile = "valid.env";
        const string _validContents1 = "REDIS_HOST=192.168.99.200";
        const string _validContents2 = "REDIS_HOST=";

        const string _invalidFile = "invalid.env";
        const string _invalidContents1 = "REDIS_HOST =192.168.99.200";
        const string _invalidContents2 = "REDIS_HOST= 192.168.99.200";
        const string _invalidContents3 = "REDIS_HOST";
        const string _invalidContents4 = "=192.168.99.200";


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

        }

        [TearDown]
        public void TearDown()
        {
            foreach(var path in new string[] { _validFile, _invalidFile })
            {
                DeleteFile(path);
            }
        }

        [TestCase(_validFile, _validContents1)]
        [TestCase(_validFile, _validContents2)]
        public void LoadTest(string path, string contents)
        {
            CreateFile(path, contents);

            Assert.IsTrue(EnvironmentFile.Load(path));

            var parsed = contents.Split(new char[] { '=' });


            Assert.AreEqual(parsed[1], Environment.GetEnvironmentVariable(parsed[0]));
        }

        [TestCase(_invalidFile, _invalidContents1)]
        [TestCase(_invalidFile, _invalidContents2)]
        [TestCase(_invalidFile, _invalidContents3)]
        [TestCase(_invalidFile, _invalidContents4)]
        public void LoadTestFail(string path, string contents)
        {
            CreateFile(path, contents);

            Assert.IsFalse(EnvironmentFile.Load(path));
        }
    }
}