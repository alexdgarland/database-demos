using CorrugatedIron;
using System;

namespace Riak_CorrugatedIron_Client_Demo
{
    partial class DemoSetup
    {
        private static IRiakClient _theClient;

        public static IRiakClient GetClient()
        {
            if (_theClient == null)
            {
                Console.Write("Connecting to Riak Cluster...  ");
                var cluster = RiakCluster.FromConfig("riakConfig");     // Initialise cluster details from App.config
                _theClient = cluster.CreateClient();                    // Connect a client to the cluster
                Console.WriteLine("OK");
                CheckClient(_theClient);
            }

            return _theClient;

        }

        static void CheckClient(IRiakClient client)
        {
            Console.Write("Pinging Riak Cluster...  ");
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