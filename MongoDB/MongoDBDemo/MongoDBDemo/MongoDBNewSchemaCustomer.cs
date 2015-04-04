using System;

using Shared.Customer;

namespace MongoDBDemo
{
    class MongoDBNewSchemaCustomer : NewSchemaCustomer
    {
        /*
        Provide access to the CustomerID field to use as a key as required by MongoDB.
        
        Because we're mocking up fundamental change to the class interface over time,
        have to repeat this boilerplate.
        If we were making changes to a real class, this wouldn't be needed.
         */

        public int Id
        {
            get { return CustomerID; }
            set { this.CustomerID = value; }
        }

    }
}
