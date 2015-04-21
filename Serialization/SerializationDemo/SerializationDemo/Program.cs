using System;
using System.IO;
using System.Xml.Serialization;


using Shared.DemoRunner;

namespace SerializationDemo
{

    class Program
    {
        static void Main(string[] args)
        {

            var runner = new DemoRunner("Serializing objects to relational storage");

            runner.AddOption("1", "Serialise object to XML and store in SQL Server", XML_SQLServer_Example.Run);
            runner.AddOption("2", "Serialise object to JSON and store in Postgres", JSON_Postgres_Example.Run);

            runner.Run();

        }
    }

}