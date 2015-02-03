using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CassandraSharp;
using CassandraSharp.Config;
using CassandraSharp.CQLPoco;
using DatabaseDemo.Shared;

namespace CassandraSharp_Client_Demo
{
    class RawCQLDemo
    {
        public static void Run()
        {
            try
            { 
                XmlConfigurator.Configure();
                using (ICluster cluster = ClusterManager.GetCluster("TestCassandra"))
                {
                    ICqlCommand cmd = cluster.CreatePocoCommand();

                    string insertCQL = "INSERT INTO databasedemos.Persons (PersonID, FirstName, LastName) VALUES (1234, 'Demo', 'Person');";
                    Console.WriteLine("Inserting data using raw CQL statement:\n\n{0}\n", insertCQL);
                    cmd.Execute(insertCQL).AsFuture().Wait();

                    string selectCQL = "SELECT * FROM databasedemos.Persons;";
                    Console.WriteLine("Retrieving person using raw CQL statement:\n\n{0}\n", selectCQL);
                    var retrievedPerson = cmd.Execute<CassandraPerson>(selectCQL).AsFuture().Result.First();
                    Console.WriteLine("{0} {1} {2}", retrievedPerson.PersonID, retrievedPerson.FirstName, retrievedPerson.LastName);
                }
            }
            catch (Exception e)
            {
                    Console.WriteLine(e);
            }
            Console.ReadLine();
        }
    }

    // Simple class to catch/ deserialise query result.
    // Once I get the hang of things, might try and integrate properly with (inherit from? simply reuse?) DatabaseDemo.Shared.Person.
    class CassandraPerson
    {
        public int PersonID;
        public string FirstName;
        public string LastName;
    }
}
