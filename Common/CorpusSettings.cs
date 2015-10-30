using System;
using System.Configuration;

namespace Common
{
    public class CorpusSettings
    {
        public CorpusSettings()
        {

        }

        string _redisHost;
        public string RedisHost => _redisHost ?? (_redisHost = Environment.GetEnvironmentVariable("REDIS_HOST"));

        public ConnectionStringSettings Redis { get; set; }
    }
}
