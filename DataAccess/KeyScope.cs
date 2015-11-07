namespace DataAccess
{
    public class KeyScope
    {
        public const string Separator = ":";

        public const string Domain = "bsm";

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
        public string Value
        {
            get
            {
                return _value ?? (_value = string.Format("{1}{0}{2}", Separator, Domain, _scope));
            }
        }

        public override string ToString() => Value;

        public static implicit operator string(KeyScope scope) => scope.Value;
        public static implicit operator KeyScope(string scope) => new KeyScope(scope);
    }
}
