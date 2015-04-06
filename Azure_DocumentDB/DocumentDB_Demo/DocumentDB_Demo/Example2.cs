using System;
using System.Linq;

using Shared.Customer;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;


namespace DocumentDB_Demo
{
    public class Example2
    {

        public static void Run()
        {

            // Setup Azure connection -
            // some detail is hidden by static methods specific to this demo
            DocumentClient client = DemoSetup.GetClient();
            Database database = DemoSetup.GetDatabase(client, "CustomerDB");
            DocumentCollection collection = DemoSetup.GetCollection(client, database, "CustomerCollection");


            // Create a customer in memory

            DocumentDBCustomer c1 = Customer.GetDefault<DocumentDBCustomer>();
            Console.WriteLine(String.Format("\nInitial Customer:\n\n{0}\n", c1.ToString()));
            /* Keep a record of the customer's ID and name for use in demo queries */
            int savedCustomerId = c1.CustomerID;
            String savedCustomerName = c1.Name;


            // Save the customer to DocumentDB.  Note that unlike (e.g.) MongoDB, Riak, creating a document is not idempotent.
            // If a document may exist already, we need to check for it and delete/ update.
            // This extra level of safety/ overhead mat or may not be desirable.

            Document document = client.CreateDocumentQuery(collection.DocumentsLink)
                                        .Where(d => d.Id == savedCustomerId.ToString())
                                        .AsEnumerable().SingleOrDefault();
            if (document != null)
            {
                client.DeleteDocumentAsync(document.SelfLink);
            }
            client.CreateDocumentAsync(collection.DocumentsLink, c1,
                                        disableAutomaticIdGeneration: true);    /* Control our own document IDs; DocumentDB can also auto-create. */



            // Retrieve the customer details into a new customer object - USING NEW SCHEMA -
            // using LINQ (on Document ID/ key)

            NewSchemaDocumentDBCustomer c3 = client.CreateDocumentQuery<NewSchemaDocumentDBCustomer>(collection.DocumentsLink)
                                            .Where(c => c.id == savedCustomerId.ToString())
                                            .AsEnumerable().FirstOrDefault();
            Console.WriteLine(String.Format("\nCustomer retrieved into new-format object (using LINQ):\n\n{0}\n", c3.ToString()));


        }

    }

}
