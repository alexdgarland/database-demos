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

            var serializer = new XmlSerializer(c1.GetType());
            var writer = new StringWriter();
            serializer.Serialize(writer, c1);
            var xml = writer.ToString();
            Console.WriteLine("Serialized to XML:\n\n{0}\n\n", xml);

            String retrievedXML;

            using (var conn = DemoSetup.GetMSSQLConnection())
            {
                conn.Open();

                Console.WriteLine("Saving to SQL Server ...");

                using (var command = new SqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText =   @"
                                                MERGE INTO dbo.Customer AS tgt
                                                USING (SELECT  @CustomerID AS CustomerID, @XML AS [Object]) AS src
                                                    ON src.CustomerID = tgt.CustomerID
                                                WHEN MATCHED THEN UPDATE
                                                    SET tgt.[Object] = src.[Object]
                                                WHEN NOT MATCHED BY TARGET THEN INSERT (CustomerID, Object)
                                                    VALUES (@CustomerID, @XML);
                                                ";
                        command.Parameters.AddWithValue("@CustomerID", c1.CustomerID);
                        command.Parameters.AddWithValue("@XML", xml);
                        command.ExecuteNonQuery();
                    }

                Console.WriteLine("Done\n");


                Console.WriteLine("Retrieving copy of customer from SQL Server ...");

                using (var command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "SELECT [Object] FROM dbo.Customer WHERE CustomerID = @CustomerID;";
                    command.Parameters.AddWithValue("@CustomerID", savedCustomerID);
                    retrievedXML = (String)command.ExecuteScalar();
                }

                Console.WriteLine("Done\n");

            }   // </sql connection>

            var c2 = serializer.Deserialize(new StringReader(xml));
            Console.WriteLine("Retrieved Customer:\n\n{0}\n\n", c2);

        }

    }

}
