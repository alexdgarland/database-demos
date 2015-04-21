using System;

using Shared.Customer;

// http://www.newtonsoft.com/json/help/html/SerializingJSON.htm , "Install-Package Newtonsoft.Json" - popular option but there are others ...
using Newtonsoft.Json;

using Npgsql;       // Postgres driver for .NET
using NpgsqlTypes;


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
            Console.WriteLine("Serialized to JSON:\n\n{0}\n\n", json);

            

            String retrievedJson;   // Need this outside disposable block scope

            using (var conn = DemoSetup.GetPostgresConnection())
            {
                conn.Open();

                Console.WriteLine("Saving to Postgres ...");
                
                using   (var command = new NpgsqlCommand
                                    (
                                    // Postgres doesn't have a MERGE/ upsert statement so we have to do something like this:
                                    @"
                                    -- Update if already exists
                                    UPDATE  public.customer
                                    SET     object = :objectparam
                                    WHERE   customer_id = :idparam;
                                    -- Insert if does not exist
                                    INSERT INTO public.customer (customer_id, object)
                                    SELECT  :idparam, :objectparam
                                    WHERE NOT EXISTS (SELECT 1 FROM public.customer WHERE customer_id = :idparam);
                                    "
                                    ,conn
                                    )
                        )
                {
                    command.Parameters.Add(new NpgsqlParameter("idparam", NpgsqlDbType.Integer));
                    command.Parameters[0].Value = c1.CustomerID;
                    command.Parameters.Add(new NpgsqlParameter("objectparam", NpgsqlDbType.Json));
                    command.Parameters[1].Value = json;
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Done\n");


                Console.WriteLine("Retrieving copy of customer from Postgres ...");

                using (var command = new NpgsqlCommand("SELECT object FROM public.customer WHERE customer_id = :idparam;", conn))
                {
                    command.Parameters.Add(new NpgsqlParameter("idparam", NpgsqlDbType.Integer));
                    command.Parameters[0].Value = c1.CustomerID;
                    retrievedJson = (String)command.ExecuteScalar();
                }

            }   // </postgres connection>

            Customer c2 = JsonConvert.DeserializeObject<Customer>(retrievedJson);

            Console.WriteLine("Done\n");

            Console.WriteLine("Retrieved customer:\n\n{0}\n\n", c2);

        }

    }
}
