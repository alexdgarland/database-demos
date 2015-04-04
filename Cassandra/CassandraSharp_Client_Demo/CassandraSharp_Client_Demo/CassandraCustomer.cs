using System;
using System.Collections.Generic;
using System.Linq;

using Shared.Customer;

using CassandraSharp;

namespace CassandraSharp_Client_Demo
{
    class CassandraCustomer : Customer
    {
        /* Handle saving to Cassandra using CQL and CassandraSharp driver */

        public String GetSaveCQL()
        {
            /*
            Implement CQL statement construction as separate method
            for debugging and demo purposes.
            */
            return String.Format
                (
                "INSERT INTO databasedemos.Customers (CustomerID, Name, AddressSet) "
                + "VALUES ({0}, '{1}', {{{2}}});",
                this.CustomerID,
                this.Name,
                this.Addresses
                    .Select(a => "'" + a + "'")
                    .Aggregate((a, b)=> a + "," + b)
                );
        }

        public void Save(ICqlCommand cmd)
        {
            cmd.Execute(this.GetSaveCQL())
                    .AsFuture().Wait();
        }


        /* Static method to get SELECT CQL */
        public static String GetSelectCQL(int CustomerID)
        {
            return String.Format
                (
                "SELECT CustomerID, Name, AddressSet FROM databasedemos.Customers "
                + "WHERE CustomerID = {0};",
                CustomerID
                );
        }

        /* Static method to retrieve specified customer */
        public static CassandraCustomer RetrieveCustomer(ICqlCommand cmd, int CustomerID)
        {
            String selectCQL = GetSelectCQL(CustomerID);
            Console.WriteLine("Retrieving customer using raw CQL statement:\n{0}", selectCQL);
            
            return cmd.Execute<CassandraCustomer>(selectCQL)
                        .AsFuture().Result.First();
        }


        /*
        For serialisation operations:
        Map between generic List (used by in-memory class)
        and HashSet (used by client driver to synchronise with Cassandra).
        */
        public HashSet<String> AddressSet
        {
            get { return new HashSet<String>(this.Addresses);  }
            set { this.Addresses = value.ToList<String>(); }
        }
    
    }
}
