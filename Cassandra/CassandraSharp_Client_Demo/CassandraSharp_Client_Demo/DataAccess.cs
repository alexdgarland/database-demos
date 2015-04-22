using System;
using System.Linq;

using CassandraSharp;

namespace CassandraSharp_Client_Demo
{
    class DataAccess
    {

        /*
        Implement CQL statement construction as separate methods
        for debugging and demo purposes.
        */

        public static String GetCustomerSaveCQL(CassandraCustomer customer)
        {
            return String.Format
                (
                "INSERT INTO databasedemos.Customers (CustomerID, Name, AddressSet)\nVALUES ({0}, '{1}', {{{2}}});",
                customer.CustomerID,
                customer.Name,
                customer.Addresses
                    .Select(a => "'" + a + "'")
                    .Aggregate((a, b) => a + "," + b)
                );
        }


        public static String GetCustomerSelectCQL(int customerId)
        {
            return String.Format
                (
                "SELECT CustomerID, Name, AddressSet FROM databasedemos.Customers WHERE CustomerID = {0};",
                customerId
                );
        }


        /*
        
        Actual data access methods
        
        */

        public static void SaveCustomer(CassandraCustomer customer, ICqlCommand cmd)
        {
            cmd.Execute(GetCustomerSaveCQL(customer)).AsFuture().Wait();
        }


        public static CassandraCustomer RetrieveCustomer(int CustomerID, ICqlCommand cmd)
        {
            String selectCQL = GetCustomerSelectCQL(CustomerID);
            Console.WriteLine("Retrieving customer using raw CQL statement:\n{0}", selectCQL);
            return cmd.Execute<CassandraCustomer>(selectCQL).AsFuture().Result.First();
        }

    }
}
