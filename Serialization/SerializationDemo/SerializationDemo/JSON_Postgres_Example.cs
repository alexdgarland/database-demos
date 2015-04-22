using System;

// http://www.newtonsoft.com/json/help/html/SerializingJSON.htm , "Install-Package Newtonsoft.Json" - popular option but there are others ...
using Newtonsoft.Json;

using Npgsql;       // Postgres driver for .NET - API is very close to that of System.Data.SqlClient
using NpgsqlTypes;

using Shared.Customer;

namespace SerializationDemo
{
    class JSON_Postgres_Example
    {

        public static void Run()
        {
            Customer c1 = Customer.GetDefault();
            Console.WriteLine("Created new customer:\n\n{0}\n\n", c1);
            int savedCustomerId = c1.CustomerID;

            String json = JsonConvert.SerializeObject(c1, Formatting.Indented);
            Console.WriteLine("Serializes to JSON as:\n\n{0}\n\n", json);

            using (var conn = DemoSetup.GetPostgresConnection())
            {
                conn.Open();

                Console.WriteLine("Saving to Postgres ...");
                Console.WriteLine("Done\n");

                Console.WriteLine("Retrieving copy of customer from Postgres ...");
                Customer c2 = RetrieveCustomer(savedCustomerId, conn);
                Console.WriteLine("Done\n");

                Console.WriteLine("Retrieved customer:\n\n{0}\n\n", c2);
            }
        }


        private static void SaveCustomer(Customer customer, NpgsqlConnection conn)
        {
            String customerJSON = JsonConvert.SerializeObject(customer, Formatting.Indented);

            using (var command = new NpgsqlCommand())
            {
                command.Connection = conn;
                
                // Postgres doesn't have a MERGE/ upsert statement so we need a two-part command something like this:
                command.CommandText = 
                        @"UPDATE  public.customer SET object = :objectparam WHERE customer_id = :idparam;
                        
                        INSERT INTO public.customer (customer_id, object)
                        SELECT  :idparam, :objectparam
                        WHERE NOT EXISTS (SELECT 1 FROM public.customer WHERE customer_id = :idparam);";
                
                command.Parameters.AddWithValue("idparam", customer.CustomerID);
                command.Parameters.AddWithValue("objectparam", customerJSON);
                                
                command.ExecuteNonQuery();
            }
        }


        private static Customer RetrieveCustomer(int customerId, NpgsqlConnection conn)
        {
            using (var command = new NpgsqlCommand())
            {
                command.Connection = conn;
                
                command.CommandText = "SELECT object FROM public.customer WHERE customer_id = :idparam;";
                
                command.Parameters.Add(new NpgsqlParameter("idparam", NpgsqlDbType.Integer));
                command.Parameters[0].Value = customerId;
                
                String retrievedJson = (String)command.ExecuteScalar();
                
                return JsonConvert.DeserializeObject<Customer>(retrievedJson);
            }
        }


    }
}
