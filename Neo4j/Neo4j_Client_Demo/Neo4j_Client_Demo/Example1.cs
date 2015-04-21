
using System;
using System.Collections.Generic;

using Neo4jClient;


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

            foreach (var c in new List<Neo4jCustomer> { Customer1, Customer2, Customer3 })
            {
                Console.WriteLine("Saving customer to graph:\n{0}\n", c.ToString());
                c.SaveToGraph(client);
                Console.WriteLine("\nDONE\n");
            }
        }

    }
}
