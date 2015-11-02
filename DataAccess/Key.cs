namespace DataAccess
{
    public class Key
    {
        public static readonly string Separator;
        static readonly string _format;

        public static string Domain { get; }

        static Key()
        {
            Separator = ":";
            Domain = "bsm";
            _format = string.Format("{1}{0}{{0}}{0}{{1}}", Separator, Domain);
        }

        public Key()
        {

        }

        public Key(string scope, string id)
        {
            Value = string.Format(_format, scope, id);
        }

        public Key(string key)
        {
            Value = key;
        }


        public override bool Equals(object obj) => (obj is string) && Value == (string)obj;
        public override int GetHashCode() => Value.GetHashCode();
        
        public string Value { get; }

        public static implicit operator string(Key key) => key.Value;
        public static implicit operator Key(string key) => new Key(key);

    }
}
