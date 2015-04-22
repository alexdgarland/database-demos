
using System;

using Shared.Customer;


namespace Neo4j_Client_Demo
{
    class Neo4jCustomer : NewSchemaCustomer
    {

        public Neo4jCustomer(int CustomerID, String Name, String NearestStore)
        {
            this.CustomerID = CustomerID;
            this.Name = Name;
            this.NearestStore = NearestStore;
        }

        public Neo4jCustomer() { }      // MUST have a default constructor to use for deserialisation from Neo4j

    }
}
