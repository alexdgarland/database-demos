using CorrugatedIron;
using System;

namespace Riak_CorrugatedIron_Client_Demo
{
    partial class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IRiakClient client = Connect();
                Console.WriteLine("Getting message...");
                Console.WriteLine(GetDemoMessage(client));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured: " + e.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

    }

}
