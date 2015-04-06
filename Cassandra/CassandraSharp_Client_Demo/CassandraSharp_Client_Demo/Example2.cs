﻿using System;

using Shared.Customer;

using CassandraSharp;


namespace CassandraSharp_Client_Demo
{
    class Example2 : CassandraDemoExample
    {

        private static void _runExample(ICqlCommand cmd)
        {

            // Create a customer in memory

            CassandraCustomer c1 = Customer.GetDefault<CassandraCustomer>();
            Console.WriteLine(String.Format("\nInitial Customer:\n\n{0}\n", c1.ToString()));
            int savedCustomerID = c1.CustomerID;


            // Save customer to Cassandra (CQL query is generated by my CassandraCustomer object)

            Console.WriteLine("Saving data to Cassandra using raw CQL statement:\n{0}\n", c1.GetSaveCQL());
            c1.Save(cmd);


            NewSchemaCassandraCustomer c2;


            // Try to retrieve a copy of the customer from Cassandra into new-schema object
            // - this will fail if column does not already exist.

            try
            {
                Console.WriteLine("\nRetrieving customer into new-schema object.");
                c2 = NewSchemaCassandraCustomer.RetrieveCustomer(cmd, savedCustomerID);
                Console.WriteLine(String.Format("\nRetrieved Customer (new schema):\n\n{0}\n", c2.ToString()));
            }
            catch (AggregateException aggEx)
            {
                Console.WriteLine("\nOne or more errors occured while deserialising object to the new schema:");
                foreach (Exception inEx in aggEx.InnerExceptions)
                {
                    Console.WriteLine("- " + inEx.Message);
                }
            }


            // Generate a new-schema object in memory and try and save it back
            // - this will also fail if column does not already exist.

            c2 = Customer.GetDefault<NewSchemaCassandraCustomer>();
            c2.NearestStore = "Leeds";
            Console.WriteLine("\n\nCreating new-schema customer and setting Nearest Store:\n" + c2.ToString());
            try
            {
                Console.WriteLine(String.Format("\nSaving new-schema customer object to Cassandra using CQL statement:\n{0}\n\n", c2.GetSaveCQL()));
                c2.Save(cmd);
            }
            catch (AggregateException aggEx)
            {
                Console.WriteLine("\nOne or more errors occured while serialising object from the new schema:");
                foreach (Exception inEx in aggEx.InnerExceptions)
                {
                    Console.WriteLine("- " + inEx.Message);
                }
                return;     // End here if we're getting errors
            }


            // If the above has succeeded, retrive the customer again and show that we can see the new field
            var c3 = NewSchemaCassandraCustomer.RetrieveCustomer(cmd, savedCustomerID);
            Console.WriteLine(String.Format("\nRetrieved Customer (new schema):\n\n{0}\n", c3.ToString()));


        }

        public static void Run()
        {
            RunAction(_runExample);
        }

    }

}
