using NUnit.Framework;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Jil;
using StackExchange.Redis.Extensions.Newtonsoft;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Tests
{
    public abstract class RedisExtensionsTests
    {
        //#region Model
        //public interface IPerson
        //{
        //    string Name { get; set; }
        //    int Age { get; set; }
        //}



        //// a class
        //public class Person : IPerson
        //{
        //    public string Name { get; set; }

        //    public int Age { get; set; }

        //    public override bool Equals(object obj)
        //    {
        //        if (obj == null) return false;
        //        if (ReferenceEquals(this, obj)) return true;

        //        bool equal = false;
        //        if (obj is Person)
        //        {
        //            var other = (Person)obj;
        //            equal = Name.Equals(other.Name) &&
        //                    Age.Equals(other.Age);
        //        }

        //        return equal;
        //    }
        //}

        //public class Address
        //{
        //    public string Line1 { get; set; }
        //    public string Line2 { get; set; }
        //    public string City { get; set; }
        //    public string State { get; set; }
        //    public string Zip { get; set; }

        //    public override bool Equals(object obj)
        //    {
        //        if (obj == null) return false;
        //        if (ReferenceEquals(this, obj)) return true;

        //        bool equal = false;
        //        if (obj is Address)
        //        {
        //            var other = (Address)obj;
        //            if (other != null)
        //            {
        //                equal = Line1.Equals(other.Line1) &&
        //                        Line2.Equals(other.Line2) &&
        //                        City.Equals(other.City) &&
        //                        State.Equals(other.State) &&
        //                        Zip.Equals(other.Zip);
        //            }
        //        }

        //        return equal;
        //    }
        //}

        //public class PersonWithAddress : IPerson
        //{
        //    public PersonWithAddress()
        //    {
        //        Address = new Address();
        //    }

        //    public string Name { get; set; }

        //    public int Age { get; set; }

        //    public Address Address { get; set; }

        //    public override bool Equals(object obj)
        //    {
        //        if (obj == null) return false;
        //        if (ReferenceEquals(this, obj)) return true;

        //        bool equal = false;

        //        if (obj is PersonWithAddress)
        //        {
        //            var other = (PersonWithAddress)obj;
        //            equal = Address.Equals(other.Address);
        //        }

        //        return equal;
        //    }
        //}

        //#endregion

        #region Data
        readonly string StringKey0 = "test-string-key-0";
        readonly string HashKey0 = "test-hash-key-0";
        readonly string ListKey0 = "test-list-key-0";

        readonly SortedSet<string> _keys = new SortedSet<string>();
        #endregion

        ICacheClient _client;

        protected abstract ISerializer GetSerializer();

        [TestFixtureSetUp]
        public void OneTimeSetUp()
        {
            var serializer = GetSerializer();
            _client = new StackExchangeRedisCacheClient(serializer);
        }

        [TestFixtureTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            _keys.Add(StringKey0);
            _keys.Add(HashKey0);
            _keys.Add(ListKey0);

            _client.RemoveAll(_keys);
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void SetAndGetString()
        {
            var person = new Person { Name = "John", Age = 34 };

            var result = Task.Run(async () => { return await _client.AddAsync(StringKey0, person).ConfigureAwait(false); });

            Assert.IsTrue(result.Result);

            var copy = Task.Run(async () => { return await _client.GetAsync<Person>(StringKey0).ConfigureAwait(false); });

            var copyResult = copy.Result;

            Assert.AreEqual(person, copyResult);
            Assert.AreEqual(person.Name, copyResult.Name);
            Assert.AreEqual(person.Age, copyResult.Age);
        }

        [Test]
        public void SetAndGetList()
        {
            var personList = new[] { new Person { Name = "Tom" }, new Person { Name = "Mary" } };

            // NOTE: pretty sure this doesn't use the Redis LIST type internally. 
            // TODO: check out SE.Redis for that.
            var result = Task.Run(async () => { return await _client.AddAsync(ListKey0, personList); });
            Assert.IsTrue(result.Result);

            var persons = Task.Run(async () => { return await _client.GetAsync<IList<Person>>(ListKey0).ConfigureAwait(false); });

            var personsResult = persons.Result;

            Assert.AreEqual(personList, personsResult);
            Assert.AreEqual(personList[0], personsResult[0]);
            Assert.AreEqual(personList[1], personsResult[1]);
        }

        [Test]
        public void SetAndGetClass()
        {
            var person = new Person { Name = "Tom" };

            // NOTE: Pretty sure this will be the same as the string test above.
            var result = Task.Run(async () => { return await _client.AddAsync(HashKey0, person).ConfigureAwait(false); });

            Assert.IsTrue(result.Result);

            var copy = Task.Run(async () => { return await _client.GetAsync<Person>(HashKey0).ConfigureAwait(false); });

            var copyResult = copy.Result;

            Assert.AreEqual(person, copyResult);
            Assert.AreEqual(person.Name, copyResult.Name);
            Assert.AreEqual(person.Age, copyResult.Age);
        }

        [Test]
        public void SetAndGetComplexClass()
        {
            var person = new PersonWithAddress
            {
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

            // NOTE: Pretty sure this will be the same as the string test above.
            var result = Task.Run(async () => { return await _client.AddAsync(HashKey0, person).ConfigureAwait(false); });
            var boolResult = result.Result;

            Assert.IsTrue(boolResult);

            var copy = Task.Run(async () => { return await _client.GetAsync<PersonWithAddress>(HashKey0).ConfigureAwait(false); });

            var copyResult = copy.Result;

            Assert.AreEqual(person, copyResult);
        }


    }


    [TestFixture]
    public class RedisExtensionsJilTests : RedisExtensionsTests
    {
        // NOTE: JilSerializer can't handle POCOs with inheritance; it requires a call to Jil.SerializeDynamic() instead, which it doesn't do.
        protected override ISerializer GetSerializer() => new JilSerializer();
    }

    [TestFixture]
    public class RedisExtensionsNewtonsoftTests : RedisExtensionsTests
    {
        protected override ISerializer GetSerializer() => new NewtonsoftSerializer();
    }
}
