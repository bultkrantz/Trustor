using System;
using System.Collections.Generic;
using System.Text;

namespace Trustor.Models
{
    class Kund
    {
        public int KundNummer { get; set; }
        public string OrgNummer { get; set; }
        public string FöretagsNamn { get; set; }
        public string Adress { get; set; }
        public string Stad { get; set; }
        public string Region { get; set; }
        public string PostNummer { get; set; }
        public string Land { get; set; }
        public string Telefon { get; set; }
    }
}
