using CorrugatedIron;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Riak_CorrugatedIron_Client_Demo
{
    partial class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("\n*** Simple demo of .NET Riak Client \"CorrugatedIron\" ***\n");

                IRiakClient client = Connect();
                CheckClient(client);

                Console.WriteLine("\nGetting message...\n");
                Console.WriteLine("\"" + GetDemoMessage(client) + "\"");

                Console.WriteLine("\nGetting Riak logo image...\n");
                String tempFilePath = GetImage(client, fileName: "RiakLogo.jpg");
                ShowImage(tempFilePath);
                File.Delete(tempFilePath);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured: " + e.Message);
            }
            finally
            {
                Console.Write("\nDONE - Hit return to exit.");
                Console.ReadLine();
            }
        }

        static void ShowImage(String filePath)
        {
            Process process = new Process();
            process.StartInfo.FileName = filePath;
            process.Start();
            process.WaitForExit();
        }

    }

}
