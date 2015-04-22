
using Shared.DemoRunner;


namespace CassandraSharp_Client_Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            var runner = new DemoRunner("Cassandra (CassandraSharp Client)");
            
            runner.AddOption("1", "Basic demo of saving, retrieving and updating objects.", Example1.Run);
            
            runner.Run();
            
        }
    }
}
