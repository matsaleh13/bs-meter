using DataAccess.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class RedisEntityMapper<TEntity>
        where TEntity : IEntity, new()
    {
        public static HashEntry[] ToHashEntries(TEntity entity)
        {
            var hashEntries = new List<HashEntry>(PropertyInfoCache<TEntity>.Value.Count);

            foreach (var kv in PropertyInfoCache<TEntity>.Value)
            {
                hashEntries.Add(new HashEntry(kv.Key, (RedisValue)kv.Value.GetValue(entity)));
            }

            return hashEntries.ToArray();
        }

        public static TEntity ToEntity(RedisKey key, HashEntry[] hashEntries)
        {
            var entity = new TEntity() { Key = key.ToString() };

            foreach (var hashEntry in hashEntries)
            {
                var propName = hashEntry.Name;
                var propValue = hashEntry.Value;

                PropertyInfo propInfo;
                bool ok = PropertyInfoCache<TEntity>.Value.TryGetValue(propName, out propInfo);
                if (ok)
                {
                    propInfo.SetValue(entity, propValue);
                }
                else
                {
                    // TODO: Logging or something
                    throw new ApplicationException(string.Format("Invalid or misssing property name [{}] for class [{}]", propName, typeof(TEntity)));
                }
            }

            return entity;
        }

    }
}
