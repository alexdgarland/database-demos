
using System;

using Shared.Customer;

using Neo4jClient;


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

        
        //// Private inner class, just for deserialising Store nodes from Cypher queries as part of "SaveToGraph" method
        //private class Neo4jStore { public String Name; }


        public void SaveToGraph(GraphClient client)
        {
            // Save the customer details to Neo4j.
            // We could combine some of these Cypher queries for greater efficiency,
            // but let's keep them separate so as to stay simple for demo purposes:


            // Merge in the associated store so we know it exists once and only once in the graph
            client.Cypher
                .Merge("(store:Store { Name: { StoreName } })")
                .WithParam("StoreName", this.NearestStore)
                .ExecuteWithoutResults();

            // Add the Customer to the graph (merging on CustomerID which we take as unique)
            client.Cypher
                .Merge("(customer:Customer { CustomerID: { customerIDParam }})")
                .OnCreate()
                .Set("customer = { customerParam }")
                .WithParams(new { customerIDParam = this.CustomerID, customerParam = this })
                .ExecuteWithoutResults();

            // Create the relationship (again - merge as unique)
            client.Cypher
                .Match("(customer:Customer { CustomerID : { customerIDParam } } )", "(store:Store { Name : { storeNameParam } } )")
                .WithParam("customerIDParam", this.CustomerID)
                .WithParam("storeNameParam", this.NearestStore)
                .CreateUnique("customer-[:NEAREST_STORE_IS]->store")
                .ExecuteWithoutResults();
            // In a real system, if updating customers we would also want to delete possible outdated links to "NearestStore"
            // if this link might have changed - but can leave that out for now.

        }

    }
}
