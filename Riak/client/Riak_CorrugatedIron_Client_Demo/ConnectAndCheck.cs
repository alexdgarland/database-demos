using CorrugatedIron;
using System;

namespace Riak_CorrugatedIron_Client_Demo
{
    partial class Program
    {
        static IRiakClient Connect()
        {
            Console.WriteLine("Connecting to Riak Cluster...");
            var cluster = RiakCluster.FromConfig("riakConfig");     // Initialise cluster details from App.config
            var client = cluster.CreateClient();                    // Connect a client to the cluster
            Console.WriteLine("OK");
            return client;
        }

        static void CheckClient(IRiakClient client)
        {
            Console.Write("Pinging Riak...  ");
            var pingResult = client.Ping();
            if (pingResult.IsSuccess)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("FAILED");
                throw new Exception("Could not successfully ping Riak server.");
            }
        }
    }

}