using System;
using System.Linq;

using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;


namespace MongoDBDemo
{
    class Example2
    {

        public static void Run(MongoDatabase database)
        {

            var collection = database.GetCollection<Customer>("Customers");     // Connect to customer collection

            // Create a customer in memory

            Customer c1 = Customer.GetDefault();
            Console.WriteLine(String.Format("\nInitial Customer:\n\n{0}\n", c1.ToString()));
            int savedCustomerId = c1.Id;     // Keep a record of the ID


            // Automaticaly serialize the customer to MongoDB.

            collection.Save(c1);


            // Retrieve the customer details into a customer object with an additional field
            // (assume we have expanded our object definition to meet a new requirement)

            var collection2 = database.GetCollection<NewSchemaCustomer>("Customers");    // Connect to same Mongo collection but specify a different type

            var query = Query<NewSchemaCustomer>.EQ(c => c.Id, savedCustomerId);
            NewSchemaCustomer c2 = collection2.FindOne(query);
            Console.WriteLine(String.Format("\nRetrieved Customer (New Format):\n\n{0}\n", c2.ToString()));

        }

    }
}
