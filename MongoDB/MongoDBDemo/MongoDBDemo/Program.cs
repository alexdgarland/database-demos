
using System;

// Demo of "Mongo C-Sharp Driver" (officially supported by MongoDB)
// Get with Nuget - "Install-Package mongocsharpdriver"
// Using version 1.10.0 (see - http://api.mongodb.org/csharp/1.10/)
// which is currently the standard and most widely compatible;
// 2.0 is available but requires >= .NET 4.5
/// Demos based on http://docs.mongodb.org/ecosystem/tutorial/getting-started-with-csharp-driver/

using MongoDB.Driver;


namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Set up connection

            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("test");                          // "test" is the name of the database

            // Select which demo we want to run

            Console.WriteLine("**** MONGODB DEMO ****\n\n");
            
            String response = "";

            while (response.ToUpper() != "Q")
            {
                Console.WriteLine("Select an option:\n\n");
                Console.WriteLine("1 - Basic demo using \"Customer\" object");
                Console.WriteLine("2 - Changing the schema");
                Console.WriteLine("\n ... or press Q to quit.\n\n");

                response = Console.ReadLine();
                switch (response)
                {
                    case "1":
                        {
                            Example1.Run(database);
                            break;
                        }
                    case "2":
                        {
                            Example2.Run(database);
                            break;
                        }
                }
            }
        }
    }
}
