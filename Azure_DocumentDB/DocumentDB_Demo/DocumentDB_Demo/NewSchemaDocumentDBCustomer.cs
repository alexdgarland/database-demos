using System;
using System.Collections.Generic;

using Shared.Customer;


namespace DocumentDB_Demo
{
    public class NewSchemaDocumentDBCustomer : NewSchemaCustomer
    {
        /* Provide access to the CustomerID field to use as a key as required by DocumentDB */
        public String id
        {
            get { return this.CustomerID.ToString(); }
            set { this.CustomerID = Int32.Parse(value); }
        }

    }
}
