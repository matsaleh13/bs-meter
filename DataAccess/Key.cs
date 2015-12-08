using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;

namespace DataAccess
{
    /// <summary>
    /// Responsible for abstracting the contents of a Redis key and its components.
    /// A Redis key is basically a URI, without a scheme, and which uses a separator
    /// defined by KeyScope.Separator.
    /// </summary>
    public class Key
    {
        /// <summary>
        /// Parameterless ctor for use by (de)serializers.
        /// </summary>
        public Key()
        {
        }

        /// <summary>
        /// Ctor for assigning any string as the key.
        /// </summary>
        /// <param name="key">The value of the key.</param>
        internal Key(string key)
        {
            Value = key;
        }


        public string Value { get; }
        public override string ToString() => Value;

        public override bool Equals(object obj) => (obj is string) && Value == (string)obj;
        public override int GetHashCode() => Value.GetHashCode();
        
        public static implicit operator string(Key key) => key.Value;
        public static implicit operator Key(string key) => new Key(key);

        public static implicit operator RedisKey (Key key) => key.Value;
        public static implicit operator Key(RedisKey key) => new Key(key);
    }
}
