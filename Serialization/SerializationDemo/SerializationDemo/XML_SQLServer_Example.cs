using System;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;

using Shared.Customer;

namespace SerializationDemo
{

    public class XML_SQLServer_Example
    {


        public static void Run()
        {
            Customer c1 = Customer.GetDefault();
            Console.WriteLine("Created new customer:\n\n{0}\n\n", c1);
            int savedCustomerID = c1.CustomerID;

            Console.WriteLine("Serializes to XML as:\n\n{0}\n\n", SerializeToXMLString(c1));

            using (var conn = DemoSetup.GetMSSQLConnection())
            {
                conn.Open();

                Console.WriteLine("Saving to SQL Server ...");
                SaveCustomer(c1, conn);
                Console.WriteLine("Done\n");

                Console.WriteLine("Retrieving copy of customer from SQL Server ...");
                Customer c2 = RetrieveCustomer(savedCustomerID, conn);
                Console.WriteLine("Done\n");
                
                Console.WriteLine("Retrieved Customer:\n\n{0}\n\n", c2);
            }
        }


        static String SerializeToXMLString(object Object)
        {
            var serializer = new XmlSerializer(Object.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, Object);
            return writer.ToString();
        }


        static T DeserializeFromXMLString<T>(String xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(new StringReader(xml));
        }


        private static void SaveCustomer(Customer customer, SqlConnection conn)
        {
            using (var command = new SqlCommand())
            {
                String xml = SerializeToXMLString(customer);
                
                command.Connection = conn;

                command.CommandText =
                    @"MERGE INTO dbo.Customer AS tgt
                    USING (SELECT @CustomerID AS CustomerID, @XML AS [Object]) AS src
                        ON src.CustomerID = tgt.CustomerID
                    WHEN MATCHED THEN UPDATE SET tgt.[Object] = src.[Object]
                    WHEN NOT MATCHED BY TARGET THEN INSERT (CustomerID, Object) VALUES (@CustomerID, @XML);";
                
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("@XML", xml);
                
                command.ExecuteNonQuery();
            }
        }


        private static Customer RetrieveCustomer(int customerID, SqlConnection conn)
        {
            using (var command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT [Object] FROM dbo.Customer WHERE CustomerID = @CustomerID;";
                command.Parameters.AddWithValue("@CustomerID", customerID);
                
                String retrievedXML = (String)command.ExecuteScalar();

                return DeserializeFromXMLString<Customer>(retrievedXML);
            }
            
        }


    }

}
