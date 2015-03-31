using System;
using System.Linq;

using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;


namespace MongoDBDemo
{
    class Example1
    {

        public static void Run(MongoDatabase database)
        {

            var collection = database.GetCollection<Customer>("Customers");     // Connect to customer collection

            // Create a customer in memory

            Customer c1 = Customer.GetDefault();
            Console.WriteLine(String.Format("\nInitial Customer:\n\n{0}\n", c1.ToString()));
            int savedCustomerId = c1.Id;     // Keep a record of the ID


            // Automaticaly serialize the customer to MongoDB.
            // In this case, it picks up the fact that we have an "Id" field on the customer;
            // we can also get it to auto-allocate and return IDs using collection.Insert.

            collection.Save(c1);


            // Retrieve the customer details into a new customer object

            var query = Query<Customer>.EQ(c => c.Id, savedCustomerId);
            Customer c2 = collection.FindOne(query);
            Console.WriteLine(String.Format("\nRetrieved Customer:\n\n{0}\n", c2.ToString()));


            // Send an update of the customer details to MongoDB (add address)

            String newAddress = "25 Some Avenue, Some Town, Somewhere, PO5 CD3";
            var update = Update<Customer>.AddToSet<String>(c => c.Addresses, newAddress);
            collection.Update(query, update);               // Note, we can use the same query object to locate our customer record


            // Retrieve another copy of the customer with update applied - let's try it with LINQ this time

            Customer c3 = (
                            from c in collection.AsQueryable<Customer>()
                            where c.Id == savedCustomerId
                            select c
                            ).Single();

            Console.WriteLine(String.Format("\nRetrieved Customer (Updated) via LINQ:\n\n{0}\n", c3.ToString()));

        }

    }
}
