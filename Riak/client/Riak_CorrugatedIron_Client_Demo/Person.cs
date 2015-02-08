using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Riak_CorrugatedIron_Client_Demo
{
    public class Person
    {
        public Person(int PersonID, string FirstName, string LastName)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            Addresses = new List<Address>();
        }

        public int PersonID { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address> Addresses { get; private set; }

        public void AddAddress(Address NewAddress)
        {
            this.Addresses.Add(NewAddress);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(String.Format("ID: {0}\nName: {1} {2}", this.PersonID, this.FirstName, this.LastName));
            sb.Append("\nAddresses: " + this.Addresses.Count());
            foreach (Address a in this.Addresses)
            {
                sb.Append("\n - " + a);
            }
            return sb.ToString();
        }

        public static Person GetDefault()
        {
            var person = new Person(1234, "Demo", "Person");
            person.AddAddress(new Address("1 Some Street", "Some Area", "Some Town", "Some County", "PO5 CD1"));
            person.AddAddress(new Address("20 Some Other Street", "Some Area", "Some Town", "Some County", "PO5 CD2"));
            return person;
        }

    }
}
