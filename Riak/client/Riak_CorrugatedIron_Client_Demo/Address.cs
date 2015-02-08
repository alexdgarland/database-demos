using System;

namespace Riak_CorrugatedIron_Client_Demo
{

    public class Address
    {
        public Address(string AddressLine1, string AddressLine2, string City, string County, string Postcode)
        {
            this.AddressLine1 = AddressLine1;
            this.AddressLine2 = AddressLine2;
            this.City = City;
            this.County = County;
            this.Postcode = Postcode;
        }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}, {3}, {4}", AddressLine1, AddressLine2, City, County, Postcode);
        }
    }

}
