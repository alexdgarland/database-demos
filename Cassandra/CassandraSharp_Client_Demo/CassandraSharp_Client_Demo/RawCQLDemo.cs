using CassandraSharp;
using CassandraSharp.CQLPoco;
using System;
using System.Linq;

namespace CassandraSharp_Client_Demo
{
    class RawCQLDemo
    {
        public static void Run(ICluster cluster)
        {

            ICqlCommand cmd = cluster.CreatePocoCommand();
                    
            
            string insertCQL = "INSERT INTO databasedemos.Persons (PersonID, FirstName, LastName) VALUES (1234, 'Demo', 'Person');";
            
            Console.WriteLine("Inserting data using raw CQL statement:\n\n{0}\n", insertCQL);
                    
            cmd.Execute(insertCQL).AsFuture().Wait();


            string selectCQL = "SELECT PersonID, FirstName, LastName FROM databasedemos.Persons;";
            
            Console.WriteLine("Retrieving person using raw CQL statement:\n\n{0}\n", selectCQL);
                    
            var retrievedPerson = cmd.Execute<Person>(selectCQL).AsFuture().Result.First();
            
            Console.WriteLine("Got Person - {0}", retrievedPerson);
            
        }
    }

}
