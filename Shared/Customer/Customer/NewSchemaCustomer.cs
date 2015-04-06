using System;

namespace Shared.Customer
{
    public class NewSchemaCustomer : Customer
    {
        // We can change the class to have additional fields - represented here by a subclass but could fully replace "Customer".

        public String NearestStore;

        // When we retrieve an old-style customer from our various data stores,
        // "NearestStore" will be either null or an empty string,
        // but code should not fail for most of these data stores.
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
