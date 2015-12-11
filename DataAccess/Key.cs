using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using System;

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
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Parameter is null.");
            Value = key;
        }


        public string Value { get; }
        public override string ToString() => Value ?? "<null>";

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Key k = obj as Key;
            if (k == null) return false;

            return Equals(k);
        }

        public bool Equals(Key key)
        {
            if (key == null) return false;

            return Equals(Value, key.Value);
        }

        public override int GetHashCode() => (int)Value?.GetHashCode();
        
        /// <summary>
        /// Converts a Key to a string equal to the key's Value property.
        /// Returns null if key is null.
        /// </summary>
        /// <param name="key">The Key to convert.</param>
        public static implicit operator string(Key key) => key?.Value;

        /// <summary>
        /// Converts a string to a key having a Value property equal to the value of the string.
        /// NOTE: No validation is performed on the string.
        /// </summary>
        /// <param name="key">The string to convert.</param>
        public static implicit operator Key(string key) => new Key(key);

        /// <summary>
        /// Converts a Key to an equivalent RedisKey.
        /// Returns null if key is null.
        /// </summary>
        /// <param name="key">The Key to convert.</param>
        public static implicit operator RedisKey (Key key) => key?.Value;

        /// <summary>
        /// Converts a RedisKey to an equivalent Key.
        /// </summary>
        /// <param name="key">The RedisKey to convert.</param>
        public static implicit operator Key(RedisKey key) => new Key(key);
    }
}
