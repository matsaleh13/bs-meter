using Formo;
using System;
using System.Configuration;

namespace Common
{
    public class CorpusSettings
    {
        public CorpusSettings(string envFile)
        {
            // HACK
            // Load the Redis instance host address into the environment with this.
            // Necessary for now because the docker-machine may not always have the same IP.

            EnvironmentFile.Load(envFile);
            var env = Environment.GetEnvironmentVariables();

            env.Contains("REDIS_HOST");
        }

        string _redisHost;
        public string RedisHost => _redisHost ?? (_redisHost = Environment.GetEnvironmentVariable("REDIS_HOST"));

        string LoadConnectionString()
        {
            dynamic config = new Formo.Configuration();
            string conn = Environment.ExpandEnvironmentVariables(config.ConnectionString.Redis.ConnectionString);
            return conn;
        }

        string _redisConnection;
        public string RedisConnection => _redisConnection ?? (_redisConnection = LoadConnectionString());

    }
}
