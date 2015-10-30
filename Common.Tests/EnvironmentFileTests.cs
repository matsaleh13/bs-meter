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
        const string _validContents2 = "EXPRESSION=this=that";
        const string _validContents3 = "QUOTED_STRING=\"this is a quoted string\"";
        const string _validContents4 = "REDIS_HOST =192.168.99.200";
        const string _validContents5 = "REDIS_HOST= 192.168.99.200";

        const string _invalidFile = "invalid.env";
        const string _invalidContents1 = "REDIS_HOST";
        const string _invalidContents2 = "=192.168.99.200";


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
        [TestCase(_validFile, _validContents3)]
        [TestCase(_validFile, _validContents4)]
        [TestCase(_validFile, _validContents5)]
        public void LoadTest(string path, string contents)
        {
            CreateFile(path, contents);
            Assert.IsTrue(EnvironmentFile.Load(path));

            var allvars = Environment.GetEnvironmentVariables();

            var parsed = contents.Split(new char[] { '=' }, 2);
            var name = parsed[0].Trim();
            var value = parsed[1].Trim();

            var envvar = Environment.GetEnvironmentVariable(name);
            Assert.IsNotNull(envvar);

            Assert.AreEqual(value, envvar);
        }

        [TestCase(_invalidFile, _invalidContents1)]
        [TestCase(_invalidFile, _invalidContents2)]
        public void LoadTestFail(string path, string contents)
        {
            CreateFile(path, contents);
            Assert.IsFalse(EnvironmentFile.Load(path));
        }
    }
}