using System;

using CassandraSharp;
using CassandraSharp.Config;
using CassandraSharp.CQLPoco;


namespace CassandraSharp_Client_Demo
{
    abstract class CassandraDemoExample
    {
        // Standardise disposable use of cluster across examples
        public static void RunAction(Action<ICqlCommand> Example)
        {
            try
            {
                XmlConfigurator.Configure();
                using (ICluster cluster = ClusterManager.GetCluster("TestCassandra"))
                {
                    ICqlCommand cmd = cluster.CreatePocoCommand();
                    Example(cmd);       // Call Action that runs example using command object
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
