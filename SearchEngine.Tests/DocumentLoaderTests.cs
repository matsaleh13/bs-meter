using SearchEngine;
using NUnit.Framework;
using FakeItEasy;
using DataAccess.Interfaces;
using AnalysisModel;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System;
using Mock4Net.Core;

namespace SearchEngine.Tests
{
    [TestFixture]
    public class DocumentLoaderTests
    {
        const string _testFile = "data.txt";
        string _testDocumentData;
        IRepositoryAsync<Document> _repo;

        #region Utility
        public void CreateFile(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        // Warning: not thread safe
        private void SetupRepoMock()
        {
            // Mock setup
            A.CallTo(() => _repo.AddAsync(A<Document>._))
                .Invokes(callObject => _testDocumentData = ((Document)callObject.Arguments[0]).Content)      // Store the retrieved data for later check
                .Returns(Task.FromResult(true));
        }

        // Warning: not thread safe
        private void CheckRepoMock()
        {
            A.CallTo(() => _repo.AddAsync(A<Document>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.AreEqual(TestDocument, _testDocumentData);
        }
        #endregion


        [SetUp]
        public void SetUp()
        {
            _testDocumentData = "";
            _repo = A.Fake<IRepositoryAsync<Document>>(x => x.Strict());
        }

        [TearDown]
        public void TearDown()
        {
            _repo = null;
            DeleteFile(_testFile);  
        }


        [TestCase(16)]
        [TestCase(256)]
        [TestCase(1024)]
        [TestCase(4096)]
        [TestCase(16384)]
        public void LoadAsyncStreamTest(int blockSize)
        {
            SetupRepoMock();

            var loader = new DocumentLoaderAsync(_repo, blockSize);

            var data = new MemoryStream(Encoding.UTF8.GetBytes(TestDocument));
            var awaiter = Task.Run(async () => await loader.LoadAsync(data, "text/plain").ConfigureAwait(false));
            var result = awaiter.Result;

            Assert.IsTrue(result);
            CheckRepoMock();
        }

        [TestCase(16)]
        [TestCase(256)]
        [TestCase(1024)]
        [TestCase(4096)]
        [TestCase(16384)]
        public void LoadAsyncFileTest(int blockSize)
        {
            CreateFile(_testFile, TestDocument);

            SetupRepoMock();

            var loader = new DocumentLoaderAsync(_repo, blockSize);

            var awaiter = Task.Run(async () => await loader.LoadAsync(_testFile).ConfigureAwait(false));
            var result = awaiter.Result;

            Assert.IsTrue(result);
            CheckRepoMock();
        }

        [TestCase(16)]
        [TestCase(256)]
        [TestCase(1024)]
        [TestCase(4096)]
        [TestCase(16384)]
        public void LoadAsyncUriFileTest(int blockSize)
        {
            CreateFile(_testFile, TestDocument);

            SetupRepoMock();

            var loader = new DocumentLoaderAsync(_repo, blockSize);

            var testPath = Path.GetFullPath(_testFile);
            var awaiter = Task.Run(async () => await loader.LoadAsync(new Uri(string.Format("file:///{0}", testPath))).ConfigureAwait(false));
            var result = awaiter.Result;

            Assert.IsTrue(result);
            CheckRepoMock();
        }

        [TestCase(16)]
        [TestCase(256)]
        [TestCase(1024)]
        [TestCase(4096)]
        [TestCase(16384)]
        public void LoadAsyncUriWebTest(int blockSize)
        {
            var server = FluentMockServer.Start();
            server
                .Given(
                    Requests.WithUrl(string.Format("/{0}", _testFile))
                )
                .RespondWith(
                    Responses
                        .WithStatusCode(200)
                        .WithBody(TestDocument)
                );

            SetupRepoMock();

            var loader = new DocumentLoaderAsync(_repo, blockSize);

            var awaiter = Task.Run(async () => await loader.LoadAsync(new Uri(string.Format("http://localhost:{0}/{1}", server.Port, _testFile))).ConfigureAwait(false));
            var result = awaiter.Result;

            Assert.IsTrue(result);
            CheckRepoMock();
        }


        #region Test Data

        string TestDocument = @"PLEASE PASS THIS ALONG

 
Warren Buffet is asking each addressee to forward this email to a minimum of twenty people on their address list; in turn ask each of those to do likewise.
In three days, most people in The United States of America will have the message. This is one idea that really should be passed around.
When someone does something good, applaud! Begin forwarded

 
I REPORT, YOU DECIDE.

 
The BUFFETT Rule

 
We must support this - pass it on and let’s see if these idiots understand what people pressure is all about.

 
Salary of retired US Presidents .. . . . .. . . . . .. . $180,000 FOR LIFE

 
Salary of House/Senate members .. . . . .. . . . $174,000 FOR LIFE This is stupid

 
Salary of Speaker of the House .. . . . .. . . . . $223,500 FOR LIFE This is really stupid

 
Salary of Majority/Minority Leaders . . .. . . . . $193,400 FOR LIFE Ditto last line

 
Average Salary of a teacher . . .. . . . .. . . . . .. .. $40,065

 
Average Salary of a deployed Soldier . . .. . . .. $38,000

 
I think we found where the cuts should be made! If you agree pass it on, as I just did.

 
Warren Buffet, in a recent interview with CNBC, offers one of the best quotes about the debt ceiling:

 
""I could end the deficit in five minutes,"" he told CNBC. ""You just pass a law that says that anytime there is a deficit of more than 3% of GDP, all sitting members of Congress are ineligible for re-election"".

 
The 26th Amendment(granting the right to vote for 18 year-olds) took only three months and eight days to be ratified! Why? Simple! The people demanded it.That was in 1971 - before computers, e-mail, cell phones, etc.


Of the 27 amendments to the Constitution, seven (7) took one(1) year or less to become the law of the land - all because of public pressure.


Warren Buffet is asking each addressee to forward this email to a minimum of twenty people on their address list; in turn ask each of those to do likewise.


In three days, most people in The United States of America will have the message.This is one idea that really should be passed around.


Congressional Reform Act of 2015

 
1. No Tenure / No Pension.  A Congressman/woman collects a salary while in office and receives no pay when they're out of office.

 
2. Congress (past, present, & future) participates in Social Security.

 
All funds in the Congressional retirement fund move to the Social Security system immediately. All future funds flow into the Social Security system, and Congress participates with the American people. It may not be used for any other purpose.

 
3. Congress can purchase their own retirement plan, just as all Americans do.

 
4. Congress will no longer vote themselves a pay raise. Congressional pay will rise by the lower of CPI or 3%.

 
5. Congress loses their current health care system and participates in the same health care system as the American people.

 
6. Congress must equally abide by all laws they impose on the American people.

 
7. All contracts with past and present Congressmen/women are void effective 12/1/15. The American people did not make this contract with Congressmen/women.


Congress made all these contracts for themselves.Serving in Congress is an honor, not a career.The Founding Fathers envisioned citizen legislators, so ours should serve their term(s), then go home and go back to work.


If each person contacts a minimum of twenty people, then it will only take three days for most people (in the U.S.) to receive the message.Don't you think it's time?


THIS IS HOW YOU FIX CONGRESS!


If you agree, pass it on.If not, delete.
";
        #endregion

    }
}