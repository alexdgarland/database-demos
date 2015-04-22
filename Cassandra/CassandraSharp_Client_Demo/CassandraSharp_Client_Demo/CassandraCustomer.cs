using System;
using System.Collections.Generic;
using System.Linq;

using Shared.Customer;


namespace CassandraSharp_Client_Demo
{
    class CassandraCustomer : Customer
    {
        
        /*
        For serialisation operations:
        Map between generic List (used by in-memory class) and HashSet (used by client driver to synchronise with Cassandra).
        */
        public HashSet<String> AddressSet
        {
            get { return new HashSet<String>(this.Addresses);  }
            set { this.Addresses = value.ToList<String>(); }
        }
    
    }
}
