﻿using System;
using System.Linq;
using System.IO;
using System.Xml.Linq;

using Npgsql;
using System.Data.SqlClient;

namespace SerializationDemo
{
    public class DemoSetup
    {

        /*
        GetPostgresConnection method will read a config file stored outside the source control tree,
        read out details and use these to initialise an Npgsql connection.

        XML config file needs to be stored in current user home directory, called "Postgres_Demo.config"
        and structured as:
         
        <?xml version="1.0" encoding="utf-8" ?>
        <connection>
            <Server>(PUT YOUR POSTGRES DATABASE SERVER HOSTNAME (TYPICALLY localhost) HERE)</Server>
            <Port>(PUT YOUR POSTGRES PORT (TYPICALLY 5432) HERE)</Port>
            <UserID>(PUT YOUR POSTGRES USER ID HERE)</UserID>
            <Password>(PUT YOUR POSTGRES PASSWORD HERE)</Password>
            <Database>(PUT THE NAME OF THE POSTGRES DB TO USE HERE)</Database>
        </connection>
         
        */
        public static NpgsqlConnection GetPostgresConnection()
        {  
            String homePath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            String configFilePath = homePath + "\\Postgres_Demo.config";

            if (!File.Exists(configFilePath))
            {
                Console.WriteLine("ERROR: Postgres config file not available.");
                Console.WriteLine("Please see code comment in DemoSetup.cs for details of requirement.\n");
                System.Environment.Exit(1);
            }

            var document = XDocument.Load(configFilePath);

            Func<String, String> getConfig = (elemName) => (document.Descendants(elemName).Single<XElement>().Value);

            String connString = String.Format
                                        (
                                        "Server={0};Port={1};User Id={2};Password={3};Database={4};"
                                        ,getConfig("Server")
                                        , getConfig("Port")
                                        , getConfig("UserID")
                                        , getConfig("Password")
                                        , getConfig("Database")
                                        );
            
            return new NpgsqlConnection(connString);
        }


        /*
        GetMSSQLConnection method will read a config file stored outside the source control tree,
        read out details and use these to initialise a SQL Server connection.

        XML config file needs to be stored in current user home directory, called "MSSQL_Demo.config"
        and structured as:
         
        <?xml version="1.0" encoding="utf-8" ?>
        <connection>
            <Server>(PUT YOUR SQL SERVER HOSTNAME (TYPICALLY localhost) HERE)</Server>
            <Database>(PUT THE NAME OF THE DATABASE TO USE HERE)</Database>
        </connection>
         
        */
        public static SqlConnection GetMSSQLConnection()
        {
            String homePath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            String configFilePath = homePath + "\\MSSQL_Demo.config";

            if (!File.Exists(configFilePath))
            {
                Console.WriteLine("ERROR: SQL Server config file not available.");
                Console.WriteLine("Please see code comment in DemoSetup.cs for details of requirement.\n");
                System.Environment.Exit(1);
            }

            var document = XDocument.Load(configFilePath);

            Func<String, String> getConfig = (elemName) => (document.Descendants(elemName).Single<XElement>().Value);

            String connString = String.Format
                                        (
                                        "Data Source={0};Initial Catalog={1};Integrated Security=SSPI;"
                                        , getConfig("Server")
                                        , getConfig("Database")
                                        );

            return new SqlConnection(connString);
        }


    }
}
