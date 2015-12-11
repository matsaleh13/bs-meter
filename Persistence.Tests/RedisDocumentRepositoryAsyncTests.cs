using AnalysisModel;
using DataAccess;
using DataAccess.Interfaces;
using NUnit.Framework;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Jil;
using StackExchange.Redis.Extensions.Newtonsoft;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Tests
{
    public abstract class RedisDocumentRepositoryAsyncTests
    {
        ICacheClient _client;
        RedisDocumentRepository _repository;

        protected abstract ISerializer GetSerializer();

        #region Data

        readonly SortedSet<Key> _keys = new SortedSet<Key>();
        #endregion

        public virtual void OneTimeSetUp()
        {
            var serializer = GetSerializer();

            // Configured via app.config
            var conn = RedisConnectionManager.Instance.GetDefaultConnection();
            _client = new StackExchangeRedisCacheClient(conn, serializer);
        }

        public virtual void OneTimeTearDown()
        {
            _client = null;
        }

        [SetUp]
        public void SetUp()
        {
            //_keys.Add(Person0);
            //_keys.Add(Person1);
            //_keys.Add(Person2);

            //_client.RemoveAll(_keys);

            _repository = new RedisDocumentRepository(_client);
        }

        [TearDown]
        public void TearDown()
        {
            _repository = null;
        }

        [Test]
        public void GetAsyncByKeyTest()
        {
            // Create the test documement first, so we can create a key from the hash.
            var document = DocumentFactory.CreateDocument(TestDocument, "text/plain", TestDocument.Length);

            // Nothing with that key.
            var result = Task.Run(async () => { return await _repository.GetAsync(document.Key).ConfigureAwait(false); });

            Assert.IsNull(result.Result);

            // Add it
            _client.Add(document.Key, document);    // sync, for grins

            // Now get it again
            result = Task.Run(async () => { return await _repository.GetAsync(document.Key).ConfigureAwait(false); });
            var added = result.Result;

            Assert.IsNotNull(added);
            Assert.AreEqual(document, added);
        }

        [Test]
        public void GetAsyncByEntityTest()
        {
            // Add it
            var document = DocumentFactory.CreateDocument(TestDocument, "text/plain", TestDocument.Length);

            // Nothing with that key.
            var result = Task.Run(async () => { return await _repository.GetAsync(document).ConfigureAwait(false); });
            Assert.IsNull(result.Result);

            _client.Add(document.Key, document);    // sync, for grins

            // Now get it again
            result = Task.Run(async () => { return await _repository.GetAsync(document).ConfigureAwait(false); });
            var added = result.Result;

            Assert.IsNotNull(added);
            Assert.AreEqual(document, added);
        }

        [Test]
        public void AddAsyncTest()
        {
            // Add it
            var document = DocumentFactory.CreateDocument(TestDocument, "text/plain", TestDocument.Length);

            var result = Task.Run(async () => { return await _repository.AddAsync(document).ConfigureAwait(false); });
            Assert.IsTrue(result.Result);

            // Get the data
            var added = _client.Get<Document>(document.Key);

            Assert.IsNotNull(added);
            Assert.AreEqual(document, added);
        }

        [Test]
        public void DeleteAsyncByKeyTest()
        {
            var document = DocumentFactory.CreateDocument(TestDocument, "text/plain", TestDocument.Length);

            // Nothing with that key.
            var result = Task.Run(async () => { return await _repository.DeleteAsync(document.Key).ConfigureAwait(false); });

            Assert.IsFalse(result.Result);

            // Add it
            _client.Add(document.Key, document);    // sync, for grins

            // Now delete it again
            result = Task.Run(async () => { return await _repository.DeleteAsync(document).ConfigureAwait(false); });
            var deleted = result.Result;

            Assert.IsTrue(deleted);
        }

        [Test]
        public void DeleteAsyncByEntityTest()
        {
            // Key
            var document = DocumentFactory.CreateDocument(TestDocument, "text/plain", TestDocument.Length);

            var documentToDelete = new Document() { Key = document.Key };

            // Nothing with that key.
            var result = Task.Run(async () => { return await _repository.DeleteAsync(documentToDelete).ConfigureAwait(false); });

            Assert.IsFalse(result.Result);

            // Add it
            _client.Add(document.Key, document);    // sync, for grins

            // Now delete it again
            result = Task.Run(async () => { return await _repository.DeleteAsync(documentToDelete).ConfigureAwait(false); });
            var deleted = result.Result;

            Assert.IsTrue(deleted);
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

    [TestFixture]
    public class RedisRepositoryAsyncJilTests : RedisDocumentRepositoryAsyncTests
    {
        protected override ISerializer GetSerializer() => new JilSerializer();

        [TestFixtureSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
        }

        [TestFixtureTearDown]
        public override void OneTimeTearDown()
        {
            base.OneTimeTearDown();
        }
    }

    [TestFixture]
    public class RedisRepositoryAsyncNewtonsoftTests : RedisDocumentRepositoryAsyncTests
    {
        protected override ISerializer GetSerializer() => new NewtonsoftSerializer();

        [TestFixtureSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
        }

        [TestFixtureTearDown]
        public override void OneTimeTearDown()
        {
            base.OneTimeTearDown();
        }
    }


}