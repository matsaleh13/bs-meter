using System;

namespace DataAccess
{
    public class KeyScope
    {
        public const string Separator = ":";
        public const string Domain = "bsm";

        const string NextId = "next_id";

        readonly string _scope;
        public KeyScope(string scope0)
        {
            _scope = scope0;
        }

        public KeyScope(string scope0, string scope1)
        {
            _scope = string.Format("{1}{0}{2}", Separator, scope0, scope1);
        }

        public KeyScope(string scope0, string scope1, string scope2)
        {
            _scope = string.Format("{1}{0}{2}{0}{3}", Separator, scope0, scope1, scope2);
        }

        string _value;
        /// <summary>
        /// The fully-qualified scope value, without a trailing separator.
        /// </summary>
        public string Value => _value ?? (_value = string.Format("{1}{0}{2}", Separator, Domain, _scope));

        string _nextIdKey;

        /// <summary>
        /// The key for the unique ID generator within the current scope.
        /// </summary>
        public string NextIdKey => _nextIdKey ?? (_nextIdKey = string.Format("{1}{0}{2}", Separator, Value, NextId));

        public override string ToString() => Value;

        /// <summary>
        /// Creates a new KeyScope containing the scope of the provided key.
        /// </summary>
        /// <param name="key">The key from which the scope will be created.</param>
        /// <returns>The KeyScope</returns>
        public static KeyScope FromKey(Key key)
        {
            var lastSep = key.Value.LastIndexOf(KeyScope.Separator, StringComparison.InvariantCulture);
            return new KeyScope(key.Value.Substring(0, lastSep));
        }

        public static implicit operator string(KeyScope scope) => scope.Value;
        public static implicit operator KeyScope(string scope) => new KeyScope(scope);
    }
}
