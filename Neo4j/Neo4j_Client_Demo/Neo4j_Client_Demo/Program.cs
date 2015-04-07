

// Demo of Neo4jClient - see https://github.com/Readify/Neo4jClient/wiki, installable via NuGet.

// See also - https://github.com/neo4j-contrib/developer-resources/tree/gh-pages/language-guides/dotnet


using Shared.DemoRunner;

namespace Neo4j_Client_Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            var runner = new DemoRunner("Neo4j");
            
            runner.AddOption("1", "Save customers to Neo4j, with relationships linking to Nearest Store.", Example1.Run);
            runner.AddOption("2", "Query for customers who shop at the same store.", Example2.Run);
            
            runner.Run();

        }
    }
}
