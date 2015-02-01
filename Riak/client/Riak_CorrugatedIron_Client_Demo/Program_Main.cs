using System;

// "Corrugated Iron" .NET Client for Riak
using CorrugatedIron;
// Shared code with simple Person, Address, Order objects for all demos
using dds = DatabaseDemo.Shared;

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

                Console.WriteLine("\nPress:\n - 1 for basic GET request demo\n - 2 for object handling demo");
                string selection = Console.ReadLine();
                if (selection == "1")
                {
                    ShowBasicRequests(client);
                }
                else if (selection == "2")
                {
                    ShowObjectHandling(client);
                }
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






    }

}
