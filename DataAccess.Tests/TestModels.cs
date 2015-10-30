using System;
using DataAccess.Interfaces;

namespace DataAccess.Tests
{
    public interface IPerson : IEntity
    {
        string Name { get; set; }
        int Age { get; set; }
    }



    // a class
    public class Person : IPerson
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

    public class Address
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

    public class PersonWithAddress : IPerson
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
