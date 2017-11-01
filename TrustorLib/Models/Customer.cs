namespace TrustorLib.Models
{
    public class Customer
    {
        public int CustomerNumber { get; set; }
        public string OrgNumber { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return CustomerNumber + ";" + OrgNumber + ";" + CompanyName + ";" + Address + ";" + City + ";" + Region +
                   ";" + PostalCode + ";" + Country + ";" + Phone;
        }
    }
}
