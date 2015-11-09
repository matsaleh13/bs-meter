using System;

namespace DataAccess
{
    /// <summary>
    /// Responsible for defining a logical context within which a key may exist.
    /// A KeyScope contains a delimited string, similar to a URI. Each segment represents 
    /// a node in a logical hierarchy. The root node of the hierarchy is the domain, defined
    /// by <see cref="KeyScope.Domain"/> and is the same for all KeyScopes.
    /// The delimiter is defined by the <see cref="KeyScope.Separator"/> field.
    /// 
    /// </summary>
    public class KeyScope
    {
        public const string Separator = ":";
        public const string Domain = "bsm";

        const string NextId = "next_id";

        readonly string _scope;

        /// <summary>
        /// Creates a KeyScope that has a single segment in addition to the domain.
        /// </summary>
        /// <param name="scope">The single segment of the scope.</param>
        public KeyScope(string scope)
        {
            _scope = scope;
        }

        /// <summary>
        /// Creates a KeyScope that has two segmenta in addition to the domain.
        /// </summary>
        /// <param name="scope0">The first segment of the scope after the domain.</param>
        /// <param name="scope1">The second segment of the scope after the domain.</param>
        public KeyScope(string scope0, string scope1)
        {
            _scope = string.Format("{1}{0}{2}", Separator, scope0, scope1);
        }

        /// <summary>
        /// Creates a KeyScope that has three segmenta in addition to the domain.
        /// </summary>
        /// <param name="scope0">The first segment of the scope after the domain.</param>
        /// <param name="scope1">The second segment of the scope after the domain.</param>
        /// <param name="scope2">The third segment of the scope after the domain.</param>
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
