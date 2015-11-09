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
        public static KeyFactory Instance => _instance ?? (_instance = new KeyFactory(RedisConnectionManager.Instance.GetConnection("Redis")));


        private ConnectionMultiplexer _connection;

        /// <summary>
        /// Ctor that requires connection information in order to create unique keys in the data store.
        /// </summary>
        /// <param name="connection">A ConnectionMultiplexer with access to a Redis data store.</param>
        public KeyFactory(ConnectionMultiplexer connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Retrieve from the persistent store the next unique ID for the given 
        /// KeyScope. The returned value can then be used as the unique component
        /// of a Key within that KeyScope.
        /// </summary>
        /// <param name="scope">The KeyScope that defines the context of the key that will use the returned ID.</param>
        /// <returns></returns>
        long GetNextId(KeyScope scope)
        {
            return _connection.GetDatabase().StringIncrement(scope.NextIdKey);
        }

        const string _keyFormat = "{1}{0}{2}";

        /// <summary>
        /// Creates a key within the given KeyScope. If the optional id parameter 
        /// is provided, it is used as the unique component of the returned key.
        /// If not, then an integer ID is generated in the database for the given scope.
        /// </summary>
        /// <param name="scope">The scope that defines the context of the key.</param>
        /// <param name="id">Optional string that serves as a unique component within the scope.</param>
        /// <returns>The new key.</returns>
        public Key CreateKey(KeyScope scope, string id = null)
        {
            if (id == null)
            {
                id = GetNextId(scope).ToString();
            }

            return new Key(string.Format(_keyFormat, KeyScope.Separator, scope.Value, id));
        }

        /// <summary>
        /// Creates a key within the given scope, which consists of two components. 
        /// If the optional id parameter is provided, it is used as the unique component of the returned key.
        /// If not, then an integer ID is generated in the database for the given scope.
        /// </summary>
        /// <param name="scope0">The first of two components of the scope that defines the context of the key.</param>
        /// <param name="scope1">The second of two components of the scope that defines the context of the key.</param>
        /// <param name="id">Optional string that serves as a unique component within the scope.</param>
        /// <returns>The new key.</returns>
        public Key CreateKey(string scope0, string scope1, string id = null) => 
            CreateKey(new KeyScope(scope0, scope1), id);

        /// <summary>
        /// Creates a key within the given scope, which consists of two components. 
        /// If the optional id parameter is provided, it is used as the unique component of the returned key.
        /// If not, then an integer ID is generated in the database for the given scope.
        /// </summary>
        /// <param name="scope0">The first of three components of the scope that defines the context of the key.</param>
        /// <param name="scope1">The second of three components of the scope that defines the context of the key.</param>
        /// <param name="scope2">The third of three components of the scope that defines the context of the key.</param>
        /// <param name="id">Optional string that serves as a unique component within the scope.</param>
        /// <returns>The new key.</returns>
        public Key CreateKey(string scope0, string scope1, string scope2, string id = null) => 
            CreateKey(new KeyScope(scope0, scope1, scope2), id);

        /// <summary>
        /// Overload of <see cref="CreateKey(KeyScope, string)"/> that accepts an id of type long.
        /// </summary>
        /// <param name="scope">The scope that defines the context of the key.</param>
        /// <param name="id">Optional string that serves as a unique component within the scope.</param>
        /// <returns>The new key.</returns>
        public Key CreateKey(KeyScope scope, long id) => 
            CreateKey(scope, id.ToString());

        /// <summary>
        /// Overload of <see cref="CreateKey(string, string, string)"/> that accepts an id of type long.
        /// </summary>
        /// <param name="scope0">The first of three components of the scope that defines the context of the key.</param>
        /// <param name="scope1">The second of three components of the scope that defines the context of the key.</param>
        /// <param name="id">Optional string that serves as a unique component within the scope.</param>
        /// <returns>The new key.</returns>
        public Key CreateKey(string scope0, string scope1, long id) => 
            CreateKey(new KeyScope(scope0, scope1), id.ToString());

        /// <summary>
        /// Overload of <see cref="CreateKey(string, string, string, string)"/> that accepts an id of type long.
        /// </summary>
        /// <param name="scope0">The first of three components of the scope that defines the context of the key.</param>
        /// <param name="scope1">The second of three components of the scope that defines the context of the key.</param>
        /// <param name="scope2">The third of three components of the scope that defines the context of the key.</param>
        /// <param name="id">Optional string that serves as a unique component within the scope.</param>
        /// <returns>The new key.</returns>
        public Key CreateKey(string scope0, string scope1, string scope2, long id) => 
            CreateKey(new KeyScope(scope0, scope1, scope2), id.ToString());

    }
}
