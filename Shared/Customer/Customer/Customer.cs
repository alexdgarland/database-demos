using System;
using System.Collections.Generic;

namespace Shared.Customer
{

    public class Customer
    {

        public int CustomerID;

        public String Name;
        public List<String> Addresses = new List<string>();

        public override string ToString()
        {
            String s = String.Format("Customer.  ID: {0}, Name: {1}", this.CustomerID, this.Name);
            foreach (String a in Addresses)
            {
                s += "\n" + a;
            }
            return s;
        }

        public static T GetDefault<T>()
            where T:Customer, new()
        {
            var c = new T();
            c.CustomerID = 111;
            c.Name = "John Smith";
            c.Addresses.Add("1 Some Street, Some Town, Somewhere, PO5 CD1");
            c.Addresses.Add("12 Some Road, Some Town, Somewhere, PO5 CD2");
            return c;
        }

        public static Customer GetDefault()
        {
            return GetDefault<Customer>();
        }

    }

}
