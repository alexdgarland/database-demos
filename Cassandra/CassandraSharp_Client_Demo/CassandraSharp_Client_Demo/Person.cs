using System;

namespace CassandraSharp_Client_Demo
{
    class Person
    {
        public int PersonID;
        public string FirstName;
        public string LastName;

        public override string ToString()
        {
            return String.Format("Person ID: {0}, Name: {1} {2}", this.PersonID, this.FirstName, this.LastName);
        }

    }
}
