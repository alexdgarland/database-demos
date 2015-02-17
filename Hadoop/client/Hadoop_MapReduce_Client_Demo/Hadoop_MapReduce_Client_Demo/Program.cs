using Microsoft.Hadoop.MapReduce;
using Microsoft.Hadoop.WebClient.WebHCatClient;
using System;

namespace Hadoop_MapReduce_Client_Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            // Set up config
            var config = new HadoopJobConfiguration();
            String DFSUserPath = "/user/" + Environment.UserName;   // We have explicitly set up a user folder in HDFS
            config.InputPath = DFSUserPath + "/input";
            config.OutputFolder = DFSUserPath + "/output";

            // Connect
            IHadoop cluster = Hadoop.Connect();

            // Execute MapReduce job
            try
            {
                MapReduceResult mrResult = cluster.MapReduceJob.Execute<PostcodeDistrictCountMapper, CountReducer>(config);
                Console.WriteLine("Job result is " + mrResult.Info.ExitCode.ToString());
            }
            catch (Exception e)
            {
                String logPath = String.Format("C:\\Users\\{0}\\Desktop\\MRLog.txt", Environment.UserName);
                using (var stream = new System.IO.StreamWriter(logPath))
                {
                    stream.Write(e);
                }
                Console.WriteLine("An error occured, please consult {0} for details.", logPath);
            }

            Console.Read();

        }
    }
}
