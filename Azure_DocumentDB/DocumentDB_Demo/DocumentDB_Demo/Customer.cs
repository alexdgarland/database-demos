using System;
using System.Collections.Generic;

namespace DocumentDB_Demo
{
    public class Customer
    {

        public int CustomerID;

        public String id
            {
                get { return CustomerID.ToString();  }
                set { this.CustomerID = Int32.Parse(value); }
            } 
        
        public String Name;
        public List<String> Addresses = new List<string>();

        public static Customer GetDefault()
        {
            var c = new Customer();
            c.CustomerID = 111;
            c.Name = "John Smith";
            c.Addresses.Add("1 Some Street, Some Town, Somewhere, PO5 CD1");
            c.Addresses.Add("12 Some Road, Some Town, Somewhere, PO5 CD2");
            return c;
        }

        public override string ToString()
        {
            String s = String.Format("Customer.  ID: {0}, Name: {1}", this.CustomerID, this.Name);
            foreach (String a in Addresses)
            {
                s += "\n" + a;
            }
            return s;
        }

    }
}
