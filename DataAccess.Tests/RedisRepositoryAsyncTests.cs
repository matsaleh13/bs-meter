using Common;
using DataAccess.Interfaces;
using NUnit.Framework;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Jil;
using StackExchange.Redis.Extensions.Newtonsoft;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Tests
{
    public abstract class RedisRepositoryAsyncTests
    {
        class PersonRepositoryAsync : RedisRepositoryAsync<PersonWithAddress>
        {
            public PersonRepositoryAsync(ICacheClient client) : base(client) {}
        }

        ICacheClient _client;
        IRepositoryAsync<PersonWithAddress> _repository;

        protected abstract ISerializer GetSerializer();

        #region Data
        readonly string Person0 = "test-person-key-0";
        readonly string Person1 = "test-person-key-1";
        readonly string Person2 = "test-person-key-2";

        readonly SortedSet<string> _keys = new SortedSet<string>();
        #endregion

        [TestFixtureSetUp]
        public void OneTimeSetUp()
        {
            var serializer = GetSerializer();

            // Configured via app.config
            var conn = GlobalConfig.Instance.Corpus["Redis"];
            _client = new StackExchangeRedisCacheClient(serializer, conn);
        }

        [TestFixtureTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            _keys.Add(Person0);
            _keys.Add(Person1);
            _keys.Add(Person2);

            _client.RemoveAll(_keys);

            _repository = new PersonRepositoryAsync(_client);
        }

        [TearDown]
        public void TearDown()
        {
            _repository = null;
        }

        [Test]
        public void GetAsyncByKeyTest()
        {
            // Nothing with that key.
            var result = Task.Run(async () => { return await _repository.GetAsync(Person0).ConfigureAwait(false); });

            Assert.IsNull(result.Result);

            // Add it
            var person = new PersonWithAddress
            {
                Key = Person0,
                Name = "Matt",
                Age = 51,
                Address = new Address
                {
                    Line1 = "1234 Anywhere Street",
                    Line2 = "",
                    City = "Anywhere",
                    State = "WA",
                    Zip = "12345"
                }
            };

            _client.Add(person.Key, person);    // sync, for grins

            // Now get it again
            result = Task.Run(async () => { return await _repository.GetAsync(Person0).ConfigureAwait(false); });
            var added = result.Result;

            Assert.IsNotNull(added);
            Assert.AreEqual(person, added);
        }

        [Test]
        public void GetAsyncByEntityTest()
        {
            // Add it
            var person = new PersonWithAddress
            {
                Key = Person0,
                Name = "Matt",
                Age = 51,
                Address = new Address
                {
                    Line1 = "1234 Anywhere Street",
                    Line2 = "",
                    City = "Anywhere",
                    State = "WA",
                    Zip = "12345"
                }
            };

            // Nothing with that key.
            var result = Task.Run(async () => { return await _repository.GetAsync(person).ConfigureAwait(false); });
            Assert.IsNull(result.Result);

            _client.Add(person.Key, person);    // sync, for grins

            // Now get it again
            result = Task.Run(async () => { return await _repository.GetAsync(person).ConfigureAwait(false); });
            var added = result.Result;

            Assert.IsNotNull(added);
            Assert.AreEqual(person, added);
        }

        [Test]
        public void AddAsyncTest()
        {
            // Add it
            var person = new PersonWithAddress
            {
                Key = Person0,
                Name = "Matt",
                Age = 51,
                Address = new Address
                {
                    Line1 = "1234 Anywhere Street",
                    Line2 = "",
                    City = "Anywhere",
                    State = "WA",
                    Zip = "12345"
                }
            };

            var result = Task.Run(async () => { return await _repository.AddAsync(person).ConfigureAwait(false); });
            Assert.IsTrue(result.Result);

            // Get the data
            var added = _client.Get<PersonWithAddress>(Person0);

            Assert.IsNotNull(added);
            Assert.AreEqual(person, added);
        }

        [Test]
        public void DeleteAsyncByKeyTest()
        {
            // Nothing with that key.
            var result = Task.Run(async () => { return await _repository.DeleteAsync(Person0).ConfigureAwait(false); });

            Assert.IsFalse(result.Result);

            // Add it
            var person = new PersonWithAddress
            {
                Key = Person0,
                Name = "Matt",
                Age = 51,
                Address = new Address
                {
                    Line1 = "1234 Anywhere Street",
                    Line2 = "",
                    City = "Anywhere",
                    State = "WA",
                    Zip = "12345"
                }
            };

            _client.Add(person.Key, person);    // sync, for grins

            // Now delete it again
            result = Task.Run(async () => { return await _repository.DeleteAsync(Person0).ConfigureAwait(false); });
            var deleted = result.Result;

            Assert.IsTrue(deleted);
        }

        [Test]
        public void DeleteAsyncByEntityTest()
        {
            // Key
            var personToDelete = new PersonWithAddress
            {
                Key = Person0,
            };

            // Nothing with that key.
            var result = Task.Run(async () => { return await _repository.DeleteAsync(personToDelete).ConfigureAwait(false); });

            Assert.IsFalse(result.Result);

            // Add it
            var person = new PersonWithAddress
            {
                Key = Person0,
                Name = "Matt",
                Age = 51,
                Address = new Address
                {
                    Line1 = "1234 Anywhere Street",
                    Line2 = "",
                    City = "Anywhere",
                    State = "WA",
                    Zip = "12345"
                }
            };

            _client.Add(person.Key, person);    // sync, for grins

            // Now delete it again
            result = Task.Run(async () => { return await _repository.DeleteAsync(personToDelete).ConfigureAwait(false); });
            var deleted = result.Result;

            Assert.IsTrue(deleted);
        }
    }

    [TestFixture]
    public class RedisRepositoryAsyncJilTests : RedisRepositoryAsyncTests
    {
        protected override ISerializer GetSerializer() => new JilSerializer();
    }

    [TestFixture]
    public class RedisRepositoryAsyncNewtonsoftTests : RedisRepositoryAsyncTests
    {
        protected override ISerializer GetSerializer() => new NewtonsoftSerializer();
    }

}