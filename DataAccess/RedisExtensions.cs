using DataAccess.Interfaces;
using StackExchange.Redis;

namespace DataAccess
{
    public static class RedisExtensions
    {
        public const string KeySeparator = ":";

        public static RedisKey ToRedisKey<TEntity>(this TEntity entity) where TEntity : IEntity => entity.Key;

        public static string CreateKey<TEntity>(this TEntity entity, int id) where TEntity : IEntity => 
            string.Format("{}:{}", typeof(TEntity).Name, id.ToString());
    }
}
