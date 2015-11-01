using System;
using DataAccess.Interfaces;

namespace DataAccess.Tests
{
    public interface IPerson : IEntity
    {
        string Name { get; set; }
        int Age { get; set; }
    }



#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
                              // a class
    public class Person : IPerson
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public Key Key { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;

            bool equal = false;
            if (obj is Person)
            {
                var other = (Person)obj;
                equal = Name.Equals(other.Name) &&
                        Age.Equals(other.Age);
            }

            return equal;
        }
    }

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Address
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;

            bool equal = false;
            if (obj is Address)
            {
                var other = (Address)obj;
                if (other != null)
                {
                    equal = Line1.Equals(other.Line1) &&
                            Line2.Equals(other.Line2) &&
                            City.Equals(other.City) &&
                            State.Equals(other.State) &&
                            Zip.Equals(other.Zip);
                }
            }

            return equal;
        }
    }

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class PersonWithAddress : IPerson
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public PersonWithAddress()
        {
            Address = new Address();
        }

        public Key Key { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Address Address { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;

            bool equal = false;

            if (obj is PersonWithAddress)
            {
                var other = (PersonWithAddress)obj;
                equal = Address.Equals(other.Address);
            }

            return equal;
        }
    }

}
