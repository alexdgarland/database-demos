using System;
using System.Configuration;

using Neo4jClient;


namespace Neo4j_Client_Demo
{
    class DemoSetup
    {

        private static GraphClient _theClient;


        public static GraphClient GetClient()
        {
            if (_theClient == null)
            {
                String neo4j_URI = ConfigurationManager.ConnectionStrings["Neo4jDemo"].ConnectionString;
                _theClient = new GraphClient(new Uri(neo4j_URI));
                _theClient.Connect();
            }
            return _theClient;
        }


        public static void ClearDown()
        {
            Console.WriteLine("Deleting all relationships and nodes from Neo4j ...\n");            
            
            var client = GetClient();
            
            // Delete all relationships
            client.Cypher
                .Match("()-[r]-()")
                .Delete("r")
                .ExecuteWithoutResults();
            
            // Having done that - can now delete all nodes
            client.Cypher
                .Match("(n)")
                .Delete("n")
                .ExecuteWithoutResults();
            
            Console.WriteLine("DONE\n");
        }

    }
}
