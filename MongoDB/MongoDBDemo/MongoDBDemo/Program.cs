
using System;

using Shared.DemoRunner;


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
            
            // Run demos - use shared code to pick which one(s)

            var runner = new DemoRunner("MongoDB");
            runner.AddOption("1", "Basic demo using \"Customer\" object", Example1.Run);
            runner.AddOption("2", "Changing the schema", Example2.Run);
            runner.Run();

        }
    }
}
