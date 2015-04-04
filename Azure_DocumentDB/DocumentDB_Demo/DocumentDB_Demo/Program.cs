using System;
using System.Linq;

using Shared.DemoRunner;


// Demo of DocumentDB / SDK preview - see http://azure.microsoft.com/en-gb/documentation/articles/documentdb-get-started/
// Main example code from Microsoft is available here - https://github.com/Azure/azure-documentdb-net/tree/master/tutorials/get-started
// and uses a lot of the async/ await features of the SDK; I have removed some of this for maximum simplicity.


namespace DocumentDB_Demo
{
    class Program
    {

        static void Main(string[] args)
        {

            var runner = new DemoRunner("Azure Document DB");
            runner.AddOption("1", "Demo of Azure Document DB", Example1.Run);
            runner.Run();

        }

    }
}


