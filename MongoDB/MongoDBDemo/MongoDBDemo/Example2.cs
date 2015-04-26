using System;

using MongoDB.Driver;
using MongoDB.Driver.Builders;

using Shared.Customer;


namespace MongoDBDemo
{
    class Example2
    {

        public static void Run()
        {
            MongoDatabase database = DemoSetup.GetDatabase();

            var collection = database.GetCollection<MongoDBCustomer>("Customers");     // Connect to customer collection

            // Create a customer in memory

            MongoDBCustomer c1 = Customer.GetDefault<MongoDBCustomer>();
            Console.WriteLine(String.Format("\nInitial Customer:\n\n{0}\n", c1.ToString()));
            int savedCustomerId = c1.Id;     // Keep a record of the ID


            // Automaticaly serialize the customer to MongoDB.

            collection.Save(c1);


            // Retrieve the customer details into a customer object with an additional field
            // (assume we have expanded our object definition to meet a new requirement)

            var collection2 = database.GetCollection<MongoDBNewSchemaCustomer>("Customers");    // Connect to same Mongo collection but specify a different type

            var query = Query<MongoDBNewSchemaCustomer>.EQ(c => c.Id, savedCustomerId);
            MongoDBNewSchemaCustomer c2 = collection2.FindOne(query);
            Console.WriteLine(String.Format("\nRetrieved Customer (New Format):\n\n{0}\n", c2.ToString()));

        }

    }
}
