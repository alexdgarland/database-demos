using System;

using Shared.DemoRunner;

using CorrugatedIron;


namespace Riak_CorrugatedIron_Client_Demo
{
    partial class Program
    {
        static void Main(string[] args)
        {

            var runner = new DemoRunner("Riak (CorrugatedIron Client)");

            runner.AddOption("1", "Basic GET request demo", GetRequests.Run);
            runner.AddOption("2", "Object handling demo", ObjectHandlingDemo.Run);

            runner.Run();
        
        }

    }

}
