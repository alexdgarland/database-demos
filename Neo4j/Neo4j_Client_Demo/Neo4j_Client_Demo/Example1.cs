
using System;
using System.Collections.Generic;

using Neo4jClient;

using Shared.Customer;

namespace Neo4j_Client_Demo
{
    class Example1
    {


        public static void Run()
        {
            GraphClient client = DemoSetup.GetClient(); // Get Neo4j connection
            DemoSetup.ClearDown();                      // Delete everything & start with a blank slate - never do this in production code!

            // Create some customers

            var Customer1 = new Neo4jCustomer(1, "Customer 1", "Leeds");
            var Customer2 = new Neo4jCustomer(2, "Customer 2", "York");
            var Customer3 = new Neo4jCustomer(3, "Customer 3", "Leeds");

            // Save them to Neo4j

            foreach (var customer in new List<Neo4jCustomer> { Customer1, Customer2, Customer3 })
            {
                Console.WriteLine("Saving customer to graph:\n{0}\n", customer.ToString());
                SaveCustomerToGraph(customer, client);
                Console.WriteLine("\nDONE\n");
            }
        }


        private static void SaveCustomerToGraph(Neo4jCustomer customer, GraphClient client)
        {
            // Save customer details to Neo4j.
            // We could combine some of these Cypher queries for efficiency, but let's keep things simple for demo purposes:

            // Merge in the associated store so we know it exists once and only once in the graph
            client.Cypher
                .Merge("(store:Store { Name: { StoreName } })")
                .WithParam("StoreName", customer.NearestStore)
                .ExecuteWithoutResults();

            // Add the Customer to the graph (merging on CustomerID which we take as unique)
            client.Cypher
                .Merge("(customer:Customer { CustomerID: { customerIDParam }})")
                .OnCreate()
                .Set("customer = { customerParam }")
                .WithParams(new { customerIDParam = customer.CustomerID, customerParam = customer })
                .ExecuteWithoutResults();

            // Create the relationship (again - merge as unique)
            client.Cypher
                .Match("(customer:Customer { CustomerID : { customerIDParam } } )", "(store:Store { Name : { storeNameParam } } )")
                .WithParam("customerIDParam", customer.CustomerID)
                .WithParam("storeNameParam", customer.NearestStore)
                .CreateUnique("customer-[:NEAREST_STORE_IS]->store")
                .ExecuteWithoutResults();
            // In a real system, if updating customers we would also want to delete possible outdated links to a "NearestStore"
            // if this link might have changed - but can leave that out for now.

        }


    }
}
