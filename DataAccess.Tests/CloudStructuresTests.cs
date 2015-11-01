using CloudStructures;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Tests
{
    [TestFixture]
    public class CloudStructuresTests
    {
        #region Redis

        public static class RedisGroups
        {
            // load from web.config
            private static Dictionary<string, RedisGroup> configDict = CloudStructures.CloudStructuresConfigurationSection
                .GetSection()
                .ToRedisGroups()
                .ToDictionary(x => x.GroupName);

            // setup group
            public static readonly RedisGroup Cache = configDict["Cache"];

            public static readonly RedisGroup Session = configDict["Session"];

            static RedisGroups()
            {
                // If you want to enable Glimpse's RedisInfo then register groups
                //Glimpse.CloudStructures.Redis.RedisInfoTab.RegisiterConnection(new[] { Cache, Session });
            }
        }

        // Settings should holds in static variable
        public static class RedisServer
        {
            //public static readonly RedisSettings Default = new RedisSettings("127.0.0.1");
            public static readonly RedisSettings Default = RedisGroups.Cache.Settings[0];
        }

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        #endregion Redis

        #region Model

        // a class
        public class Person
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public override bool Equals(object obj)
            {
                bool equal = false;
                var other = (Person)obj;
                if (other != null)
                {
                    equal = Name.Equals(other.Name) &&
                            Age.Equals(other.Age);
                }

                return equal;
            }
        }

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        public class Address
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        {
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }

            public override bool Equals(object obj)
            {
                bool equal = false;
                var other = (Address)obj;
                if (other != null)
                {
                    equal = Line1.Equals(other.Line1) &&
                            Line2.Equals(other.Line2) &&
                            City.Equals(other.City) &&
                            State.Equals(other.State) &&
                            Zip.Equals(other.Zip);
                }

                return equal;
            }
        }

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        public class PersonWithAddress : Person
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        {
            public PersonWithAddress()
            {
                Address = new Address();
            }

            public Address Address { get; set; }

            public override bool Equals(object obj)
            {
                bool equal = false;
                var other = (PersonWithAddress)obj;

                if (other != null)
                {
                    equal = base.Equals(other);
                    equal = equal && Address.Equals(other.Address);
                }

                return equal;
            }
        }

        #endregion Model

        #region Data

        private readonly string StringKey0 = "test-string-key-0";
        private readonly string HashKey0 = "test-hash-key-0";
        private readonly string ListKey0 = "test-list-key-0";

        private readonly SortedSet<string> _keys = new SortedSet<string>();

        #endregion Data

        [SetUp]
        public void SetUp()
        {
            _keys.Add(StringKey0);
            _keys.Add(HashKey0);
            _keys.Add(ListKey0);

            var db = RedisServer.Default.GetConnection().GetDatabase();
            foreach (var key in _keys)
            {
                db.KeyDelete(key);
            }
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void SetAndGetString()
        {
            // Create RedisString... (or you can use RedisSettings.Default.String<Person>("key"))
            var redis = new RedisString<Person>(RedisServer.Default, StringKey0);
            var person = new Person { Name = "John", Age = 34 };

            // call command(IntelliSense helps you)
            var result = Task.Run(async () => { return await redis.Set(person).ConfigureAwait(false); });

            Assert.IsTrue(result.Result);

            // call command(IntelliSense helps you)
            var copy = Task.Run(async () => { return await redis.Get().ConfigureAwait(false); });

            var copyResult = copy.Result.Value;

            Assert.AreEqual(person, copyResult);
            Assert.AreEqual(person.Name, copyResult.Name);
            Assert.AreEqual(person.Age, copyResult.Age);
        }

        [Test]
        public void SetAndGetList()
        {
            var list = new RedisList<Person>(RedisServer.Default, ListKey0);
            var personList = new[] { new Person { Name = "Tom" }, new Person { Name = "Mary" } };

            var result = Task.Run(async () => { return await list.RightPush(personList); });
            var resultCount = result.Result;

            Assert.AreEqual(2L, resultCount);

            var persons = Task.Run(async () => { return await list.Range(0, 10).ConfigureAwait(false); });

            var personsResult = persons.Result;

            Assert.AreEqual(personList, personsResult);
            Assert.AreEqual(personList[0], personsResult[0]);
            Assert.AreEqual(personList[1], personsResult[1]);
        }

        [Test]
        public void SetAndGetClass()
        {
            var redisClass = new RedisClass<Person>(RedisServer.Default, HashKey0);
            var person = new Person { Name = "Tom" };

            var result = Task.Run(async () => { return await redisClass.Set(person); });
            var resultBool = result.Result;

            Assert.IsTrue(resultBool);

            var copy = Task.Run(async () => { return await redisClass.Get().ConfigureAwait(false); });

            var copyResult = copy.Result;

            Assert.AreEqual(person, copyResult);
            Assert.AreEqual(person.Name, copyResult.Name);
            Assert.AreEqual(person.Age, copyResult.Age);
        }

        [Test]
        public void SetAndGetComplexClass()
        {
            var redisClass = new RedisClass<PersonWithAddress>(RedisServer.Default, HashKey0);
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

            var result = Task.Run(async () => { return await redisClass.Set(person); });
            var resultBool = result.Result;

            Assert.IsTrue(resultBool);

            var copy = Task.Run(async () => { return await redisClass.Get().ConfigureAwait(false); });

            var copyResult = copy.Result;

            Assert.AreEqual(person, copyResult);
        }
    }
}