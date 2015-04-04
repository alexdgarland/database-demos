using System;
using System.Collections.Generic;

using Shared.Customer;


namespace MongoDBDemo
{
    public class MongoDBCustomer : Customer
    {

        /* Provide access to the CustomerID field to use as a key as required by MongoDB */

        public int Id
            {
                get { return CustomerID; }
                set { this.CustomerID = value;  }
            }

    }
}
