using System.Globalization;

namespace TrustorLib.Models
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public int CustomerNumber { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return AccountNumber + ";" + CustomerNumber + ";" + Balance.ToString(CultureInfo.InvariantCulture);
        }
    }
}
