using System;
using System.IO;
using System.Xml.Serialization;

using Shared.Customer;

namespace SerializationDemo
{

    public class XML_SQLServer_Example
    {

        public static void Run()
        {
            Customer c = Customer.GetDefault();
            Console.WriteLine("Created new customer:\n\n{0}\n\n", c);

            var serializer = new XmlSerializer(c.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, c);
            var xml = writer.ToString();
            Console.WriteLine("Serialized to XML:\n\n{0}\n\n", xml);
 
            var retrievedCustomer = serializer.Deserialize(new StringReader(xml));
            Console.WriteLine("Retrieved Customer:\n\n{0}\n\n", c);
        }

    }

}
