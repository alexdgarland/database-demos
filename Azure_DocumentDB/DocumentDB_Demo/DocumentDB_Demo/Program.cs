﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;


// Demo of DocumentDB / SDK preview - see http://azure.microsoft.com/en-gb/documentation/articles/documentdb-get-started/
// Main example code from Microsoft is available here - https://github.com/Azure/azure-documentdb-net/tree/master/tutorials/get-started
// and uses a lot of the async/ await features of the SDK; I have removed some of this for maximum simplicity.


namespace DocumentDB_Demo
{
    class Program
    {

        static void Main(string[] args)
        {

            // Setup Azure connection -
            // some detail is hidden by static methods specific to this demo
            DocumentClient client = DemoSetup.GetClient();
            Database database = DemoSetup.GetDatabase(client, "CustomerDB");
            DocumentCollection collection = DemoSetup.GetCollection(client, database, "CustomerCollection");


            // Create a customer in memory

            Customer c1 = Customer.GetDefault();
            Console.WriteLine(String.Format("\nInitial Customer:\n\n{0}\n", c1.ToString()));
            /* Keep a record of the customer's ID and name for use in demo queries */
            int savedCustomerId = c1.CustomerID;
            String savedCustomerName = c1.Name; 


            // Save the customer to DocumentDB.
            // Note that unlike (e.g.) MongoDB, Riak, creating a document is not idempotent (PUT semantics).
            // If a document may exist already, we need to check for it and delete/ update.
            // This extra level of safety/ overhead mat or may not be desirable.

            Document document = client.CreateDocumentQuery(collection.DocumentsLink)
                                    .Where(d => d.Id == savedCustomerId.ToString())
                                    .AsEnumerable()
                                    .SingleOrDefault();
            if (document != null)
            {
                client.DeleteDocumentAsync(document.SelfLink);
            }

            client.CreateDocumentAsync
                (
                collection.DocumentsLink
                , c1
                , disableAutomaticIdGeneration : true    /* Control our own document IDs; DocumentDB can also auto-create. */
                ); ;


            // Retrieve the customer details into a new customer object using SQL query (on Document ID/ key)

            String sqlQuery = String.Format("SELECT * FROM Customers c WHERE c.id = '{0}'", savedCustomerId);
            Console.WriteLine("\nQuerying for customer using SQL string \"" + sqlQuery + "\"");

            Customer c2 = client.CreateDocumentQuery<Customer>(collection.DocumentsLink, sqlQuery)
                            .AsEnumerable()
                            .SingleOrDefault();
            Console.WriteLine(String.Format("\nCustomer retrieved using SQL:\n\n{0}\n", c2.ToString()));


            // Retrieve the customer details into a new customer object using LINQ (on Document ID/ key)

            Customer c3 = client.CreateDocumentQuery<Customer>(collection.DocumentsLink)
                            .Where(c => c.id == savedCustomerId.ToString())
                            .AsEnumerable()
                            .FirstOrDefault();

            Console.WriteLine(String.Format("\nCustomer retrieved using LINQ:\n\n{0}\n", c3.ToString()));


            // Query on something other than the key field -
            // would advise some awareness of indexing/ performance implications!
            // This uses LINQ, when indexing is on same can be done with SQL queries.

            Customer c4 = client.CreateDocumentQuery<Customer>(collection.DocumentsLink)
                            .Where(c => c.Name == savedCustomerName)
                            .AsEnumerable()
                            .FirstOrDefault();

            Console.WriteLine(String.Format("\nCustomer retrieved using LINQ against non-key field (Name):\n\n{0}\n", c4.ToString()));

        }

    }
}


