using System;

namespace Shared.Customer
{
    public class NewSchemaCustomer : Customer
    {
        // We can change the class to have additional fields - represented here by a subclass but could fully replace "Customer".

        public String NearestStore;

        // When we retrieve an old-style customer from MongoDB,
        // "NearestStore" will be an empty string but code will not fail or set to null.
        // If we want, we can put in simple code (e.g. see below) to expect the change.

        public override string ToString()
        {
            return base.ToString()
                + String.Format
                    (
                    "\nNearest Store: {0}",
                    (String.IsNullOrEmpty(this.NearestStore)) ?  "Not Known" : this.NearestStore
                    );
        }
    
    }

}
