using System;
using System.Collections.Generic;
using System.Linq;

using Neo4jClient;


namespace Neo4j_Client_Demo
{
    class Example2
    {
        public static void Run()
        {
            GraphClient client = DemoSetup.GetClient(); // Get Neo4j connection

            /*
            
            Query Neo4j to find customers who have the same nearest store.
            
            Equivalent raw Cypher query is:
            
                MATCH (c1:Customer)-[:NEAREST_STORE_IS]->(s:Store)<-[:NEAREST_STORE_IS]-(c2:Customer) RETURN c1, c2, s;

             */

            Console.WriteLine("\nQuering Neo4j for customers who share the same nearest store ...\n");

            /* Build query object; we can inspect this when debugging to see exact Cypher query text generated */
            var query = client.Cypher
                                .Match("(c1:Customer)-[:NEAREST_STORE_IS]->(s:Store)<-[:NEAREST_STORE_IS]-(c2:Customer)")
                                .Where("c1.CustomerID < c2.CustomerID")     // Limit so we don't get duplicate results with positions reversed
                                .Return (
                                        (c1, c2, s) =>
                                            new {
                                                Customer1Name = c1.As<Neo4jCustomer>().Name,
                                                Customer2Name = c2.As<Neo4jCustomer>().Name,
                                                Store = s.As<Neo4jStore>().Name
                                                }
                                        );
            /*
            NOTE: When deserialising to an object type (".As<T>"), it seems we can't return the object itself in results
            (if we try it comes out with all fields zeroed out), only individual properties.
            So presumably the class is not being used as a "true" object in this context (combining public and private data with behaviour)
            but simply to coerce the data held in the node into something manageable by C#'s type system.
            */
            
            var queryResults = query.Results;       // Get an IEnumerable containing query results

            Console.WriteLine("DONE\n");

            Console.WriteLine("\n*** RESULTS ***");
            foreach (var result in queryResults)
            {
                Console.WriteLine("\nFound two customers whose nearest store is {0}:\n", result.Store);
                Console.WriteLine("First Customer: \"{0}\"\n", result.Customer1Name.Trim());
                Console.WriteLine("Second Customer: \"{0}\"\n", result.Customer2Name.Trim());
            }

        }

        // Private class to deserialise store node
        private class Neo4jStore
        {
            public String Name;
        }

    }
}
