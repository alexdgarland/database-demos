using CassandraSharp;
using CassandraSharp.Config;
using CassandraSharp.CQLPoco;
using System;


namespace CassandraSharp_Client_Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                XmlConfigurator.Configure();
                using (ICluster cluster = ClusterManager.GetCluster("TestCassandra"))
                {

                    RawCQLDemo.Run(cluster);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();




        }
    }
}
