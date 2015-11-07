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
        public Key(string key)
        {
            Value = key;
        }


        public string Value { get; }

        public override bool Equals(object obj) => (obj is string) && Value == (string)obj;
        public override int GetHashCode() => Value.GetHashCode();
        
        public static implicit operator string(Key key) => key.Value;
        public static implicit operator Key(string key) => new Key(key);

        static ICacheClient _client;
        public static void SetClient(ICacheClient client)
        {
            _client = client;
        }

        public static Key Create(KeyScope scope)
        {
            return new Key(scope.Value);
        }

        public static Key Create(KeyScope scope, string id)
        {
            if (id == null)
            {
                // Get a new ID from the DB.
                if (_client == null)
                    throw new DataAccessException("Client not initialized.");
                id = _client.Database.StringIncrement(scope.Value).ToString();
            }

            var key = string.Format("{1}{0}{2}", KeyScope.Separator, scope.Value, id);
            return new Key(key);
        }

        public static Key Create(string scope0, string id=null)
        {
            return Create(new KeyScope(scope0), id);
        }

        public static Key Create(string scope0, string scope1, string id=null)
        {
            return Create(new KeyScope(scope0, scope1), id);
        }

        public static Key Create(string scope0, string scope1, string scope2, string id=null)
        {
            return Create(new KeyScope(scope0, scope1, scope2), id);
        }
    }
}
