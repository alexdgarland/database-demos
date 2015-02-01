using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDemo.Shared
{
    public class Static
    {
        public static Person GetDefaultPerson()
        { 
            var person = new Person(1234, "Demo", "Person");
            person.AddAddress(new Address("1 Some Street", "Some Area", "Some Town", "Some County", "PO5 CD1"));
            person.AddAddress(new Address("20 Some Other Street", "Some Area", "Some Town", "Some County", "PO5 CD2"));
            return person;
        }
    }
}
