using CorrugatedIron;
using CorrugatedIron.Models;
using System;

using Shared.Customer;

namespace Riak_CorrugatedIron_Client_Demo
{
    partial class ObjectHandlingDemo
    {
        public static void Run()
        {
            IRiakClient client = DemoSetup.GetClient();

            // Create a customer in memory

            Customer c1 = Customer.GetDefault();
            Console.WriteLine(String.Format("\nInitial Customer:\n\n{0}\n", c1.ToString()));
            int savedCustomerID = c1.CustomerID;


            // Save customer to "customers" bucket in Riak.
            // Note that we explicitly specific the key to use, so can use the Customer object without specialisation.

            var saveObject = new RiakObject("customers", c1.CustomerID.ToString(), c1);
            client.Put(saveObject);


            // Retrieve the customer details into a new customer object

            Customer c2 = client.Get("customers", savedCustomerID.ToString())
                                    .Value.GetObject<Customer>();
                                    // For deserialisation of simple objects, use the generic GetObject method as-is.
                                    // CorrugatedIron also supports custom serialisation.
            Console.WriteLine(String.Format("\nRetrieved Customer:\n\n{0}\n", c2.ToString()));


            // Deserialise an "old schema" stored object into a new-format object

            NewSchemaCustomer c3 = client.Get("customers", savedCustomerID.ToString())
                                    .Value.GetObject<NewSchemaCustomer>();
            Console.WriteLine(String.Format("\nRetrieved Customer - Deserialised to new object format:\n\n{0}\n", c3.ToString()));

        }

    }
}
