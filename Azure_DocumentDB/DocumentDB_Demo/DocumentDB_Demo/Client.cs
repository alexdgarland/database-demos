using System;
using System.Linq;
using System.Xml.Linq;

using Microsoft.Azure.Documents.Client;


namespace DocumentDB_Demo
{
    class Client
    {
        /*
        Get method will read a config file stored outside the source control tree,
        read out Azure connection details and use these to initialise a client.

        XML config file needs to be stored in current user home directory, called "DocumentDB_Demo.config"
        and structured as:
         
        <?xml version="1.0" encoding="utf-8" ?>
        <connection>
            <endpointURL>(PUT YOUR AZURE DOCUMENTDB URI HERE)</endpointURL>
            <authKey>(PUT YOUR AZURE AUTH KEY HERE)</authKey>
        </connection>
         
        */
        public static DocumentClient Get()
        {
            String homePath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            
            var document = XDocument.Load(homePath + "\\DocumentDB_Demo.config");
            
            String endpointURL = document.Descendants("endpointURL").Single<XElement>().Value;
            String authKey = document.Descendants("authKey").Single<XElement>().Value;
            
            return new DocumentClient(new Uri(endpointURL), authKey);
        }

    }
}
