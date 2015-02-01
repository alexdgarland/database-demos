using CorrugatedIron;
using CorrugatedIron.Models;
using DatabaseDemo.Shared;
using System;

namespace Riak_CorrugatedIron_Client_Demo
{
    partial class Program
    {
        static void SavePerson(Person p, IRiakClient client)
        {
            // Create Riak object to handle saving to client
            var saveObject = new RiakObject
                (
                "persons",                  // Specify bucket
                p.PersonID.ToString(),      // Specify string to use as key
                p                           // The object itself
                );

            client.Put(saveObject);
        }

        static Person GetPerson(int PersonID, IRiakClient client)
        {
            var result = client.Get("persons", PersonID.ToString());
            // For deserialisation of simple objects, use the generic GetObject method as-is.
            return result.Value.GetObject<Person>();
            // Custom serialisation and deserialisation are also possible.
        }

        public static void ShowObjectHandling(IRiakClient client)
        {
            Console.WriteLine("\nCreating Default Person ...\n");
            var person = DatabaseDemo.Shared.Static.GetDefaultPerson();
            Console.WriteLine(person);

            Console.WriteLine("\nSaving Serialised Person to Riak ...\n");
            SavePerson(person, client);

            Console.WriteLine("\nGetting & Deserialising Person from Riak ...\n");
            var retrievedPerson = GetPerson(1234, client);
            Console.WriteLine(retrievedPerson);
        }
    }
}
