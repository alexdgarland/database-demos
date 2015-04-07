using System;
using System.Linq;
using System.Xml.Linq;
using System.IO;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace DocumentDB_Demo
{
    class DemoSetup
    {

        /*
        GetClient method will read a config file stored outside the source control tree,
        read out Azure connection details and use these to initialise a client.

        XML config file needs to be stored in current user home directory, called "DocumentDB_Demo.config"
        and structured as:
         
        <?xml version="1.0" encoding="utf-8" ?>
        <connection>
            <endpointURL>(PUT YOUR AZURE DOCUMENTDB URI HERE)</endpointURL>
            <authKey>(PUT YOUR AZURE AUTH KEY HERE)</authKey>
        </connection>
         
        */
        public static DocumentClient GetClient()
        {
            String homePath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            String configFilePath = homePath + "\\DocumentDB_Demo.config";

            if (!File.Exists(configFilePath))
            {
                Console.WriteLine("ERROR: Azure config file not available.");
                Console.WriteLine("Please see code comment in DemoSetup.cs for details of requirement.\n");
                System.Environment.Exit(1);
            }

            var document = XDocument.Load(configFilePath);
            
            String endpointURL = document.Descendants("endpointURL").Single<XElement>().Value;
            String authKey = document.Descendants("authKey").Single<XElement>().Value;
            
            return new DocumentClient(new Uri(endpointURL), authKey);
        }


        /*
        Simple method to get a database connection, creating the database if it doesn't already exist.
        */
        public static Database GetDatabase(DocumentClient client, String databaseId)
        {
            var database = client.CreateDatabaseQuery()
                .Where(db => db.Id == databaseId)
                .AsEnumerable<Database>()
                .FirstOrDefault();
            
            if (database == null)
            {
                database = client.CreateDatabaseAsync(new Database { Id = databaseId }).Result;
            }
            
            return database;
        }

        public static DocumentCollection GetCollection(DocumentClient client, Database database, String collectionId)
        {
            var documentCollection = client.CreateDocumentCollectionQuery(database.CollectionsLink)
                .Where(c => c.Id == collectionId)
                .AsEnumerable<DocumentCollection>()
                .FirstOrDefault();

            if (documentCollection == null)
            {
                documentCollection = client.CreateDocumentCollectionAsync
                    (
                    database.CollectionsLink,
                    new DocumentCollection { Id = collectionId }
                    ).Result;
            }

            return documentCollection;
        }

    }
}
