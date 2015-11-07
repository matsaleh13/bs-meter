using Common;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Responsible for creating and managing access to Redis connections.
    /// For now, a Singleton; maybe use IoC later.
    /// </summary>
    public class RedisConnectionManager
    {
        readonly IDictionary<string, ConnectionMultiplexer> _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();

        /// <summary>
        /// Ctor for testing only.
        /// </summary>
        internal RedisConnectionManager()
        {
        }

        static RedisConnectionManager _instance;

        /// <summary>
        /// Singleton access.
        /// </summary>
        public static RedisConnectionManager Instance => _instance ?? (_instance = new RedisConnectionManager());


        /// <summary>
        /// Returns a connection having the given name.
        /// </summary>
        /// <param name="name">Connection string name (from config file).</param>
        /// <returns>The ConnectionMultiplexer for the given connection</returns>
        public ConnectionMultiplexer GetConnection(string name)
        {
            ConnectionMultiplexer conn;
            if (!_connections.TryGetValue(name, out conn))
            {
                // TODO: handle errors
                conn = ConnectionMultiplexer.Connect(GlobalConfig.Instance.Connections[name]);
            }
            _connections[name] = conn;

            return conn;
        }
    }
}
