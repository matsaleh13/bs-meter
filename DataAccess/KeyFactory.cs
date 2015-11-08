using System;
using StackExchange.Redis;

namespace DataAccess
{
    /// <summary>
    /// Responsible for creating Key instances for use in retrieving values from a data store.
    /// TODO: Implement this as a singleton for now, hopefully use IoC later.
    /// </summary>
    public class KeyFactory
    {
        static KeyFactory _instance;

        /// <summary>
        /// Singleton access.
        /// TODO: IoC container
        /// </summary>
        public static KeyFactory Instance
        {
            get
            {
                return _instance ?? (_instance = new KeyFactory(RedisConnectionManager.Instance.GetConnection("Redis")));
            }
        }


        private ConnectionMultiplexer _connection;

        /// <summary>
        /// Ctor that requires connection information in order to create unique keys in the data store.
        /// </summary>
        /// <param name="connection">A ConnectionMultiplexer with access to a Redis data store.</param>
        public KeyFactory(ConnectionMultiplexer connection)
        {
            _connection = connection;
        }


        const string _keyFormat0 = "{1}{0}{2}";

        public Key CreateKey(KeyScope scope, int id)
        {
            return new Key(string.Format(_keyFormat0, KeyScope.Separator, scope.Value, id));
        }

        const string _keyFormat1 = "{1}{0}{2}{0}{3}";

        public Key CreateKey(string scope0, string scope1, string id)
        {
            return new Key(string.Format(_keyFormat1, KeyScope.Separator, scope0, scope1, id));
        }

        const string _keyFormat2 = "{1}{0}{2}{0}{3}{0}{4}";

        public Key CreateKey(string scope0, string scope1, string scope2, string id)
        {
            return new Key(string.Format(_keyFormat2, KeyScope.Separator, scope0, scope1, scope2, id));
        }
    }
}
