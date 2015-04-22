using System;

using Shared.Customer;

using CassandraSharp;
using CassandraSharp.Config;
using CassandraSharp.CQLPoco;

namespace CassandraSharp_Client_Demo
{
    class Example1
    {

        public static void Run()
        {

            try
            {
                XmlConfigurator.Configure();

                using (ICluster cluster = ClusterManager.GetCluster("TestCassandra"))
                {

                    // Create a customer in memory
                    CassandraCustomer c1 = Customer.GetDefault<CassandraCustomer>();
                    Console.WriteLine(String.Format("\nInitial Customer:\n\n{0}\n", c1.ToString()));
                    int savedCustomerID = c1.CustomerID;


                    ICqlCommand cmd = cluster.CreatePocoCommand();


                    // Save customer to Cassandra
                    Console.WriteLine("Saving data to Cassandra using raw CQL statement:\n{0}\n", DataAccess.GetCustomerSaveCQL(c1));
                    DataAccess.SaveCustomer(c1, cmd);


                    // Retrieve a copy of the customer from Cassandra
                    CassandraCustomer c2 = DataAccess.RetrieveCustomer(savedCustomerID, cmd);
                    Console.WriteLine(String.Format("\nRetrieved Customer:\n\n{0}\n", c2.ToString()));


                    // Update original object, save ...
                    c1.Addresses.Add("200 Some Avenue, Some Town, Somewhere, PO5 CD3");
                    Console.WriteLine("\n\nUpdating existing record in Cassandra using raw CQL statement:\n{0}\n", DataAccess.GetCustomerSaveCQL(c1));
                    DataAccess.SaveCustomer(c1, cmd);

                    // ... and then retrieve
                    c2 = DataAccess.RetrieveCustomer(savedCustomerID, cmd);
                    Console.WriteLine(String.Format("\nRetrieved Customer (after adding new address):\n\n{0}\n", c2.ToString()));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                ClusterManager.Shutdown();
            }

        }

    }

}
