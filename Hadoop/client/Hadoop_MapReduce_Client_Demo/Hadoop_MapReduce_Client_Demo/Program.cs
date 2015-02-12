using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Hadoop;
using Microsoft.Hadoop.MapReduce;
using Microsoft.Hadoop.WebClient.WebHCatClient;
using System.Security;

// Start with this - http://blogs.msdn.com/b/data_otaku/archive/2013/09/07/hadoop-for-net-developers-implementing-a-simple-mapreduce-job.aspx
// - and see how we get on ...

namespace Hadoop_MapReduce_Client_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("HADOOP_HOME", "/usr/bin/hadoop");
            
            // Set up config.
            var config = new HadoopJobConfiguration();
            config.InputPath = "/user/centosadmin/postcodes.csv";
            config.OutputFolder = "/user/centosadmin/output";

            // Get credentials, set up a connection to the Hadoop cluster.
            string linuxUser = "centosadmin";
            string linuxPW = Password.Get(linuxUser);
            IHadoop cluster = Hadoop.Connect(new Uri("http://linuxvm"), linuxUser, linuxPW);

            // Execute MapReduce job
            MapReduceResult mrResult = cluster.MapReduceJob.Execute<PostcodeDistrictCountMapper, CountReducer>(config);

            /*
            //write job result to console
            int exitCode = jobResult.Info.ExitCode;
            string exitStatus = "Failure";
            if (exitCode == 0) exitStatus = "Success";
            exitStatus = exitCode + " (" + exitStatus + ")";
            Console.WriteLine();
            Console.Write("Exit Code = " + exitStatus);
            */

            Console.Read();

        }
    }
}
