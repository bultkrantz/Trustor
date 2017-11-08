using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrustorLib.Interfaces;
using TrustorLib.Models;
using Xunit;

namespace XUnitTest
{
    public class TrustorDbTests
    {
        private readonly TrustorDb _context;

        public TrustorDbTests()
        {
            //var path = Path.Combine(Environment.CurrentDirectory, "../../../bankdata-small.txt");
            //_context = new TrustorDb(path);
            _context = new TrustorDb(Path.Combine(Environment.CurrentDirectory, @"Database\", "bankdata-small.txt"));

        }

        [Fact]
        public void TrustorDb_Loads_Customers()
        {
            var customers = _context.Customers;

            var expectedOutput =
                "1005;559268-7528;Berglunds snabbköp;Berguvsvägen  8;Luleå;;S-958 22;Sweden;0921-12 34 65\r\n1024;556392-8406;Folk och fä HB;Åkergatan 24;Bräcke;;S-844 67;Sweden;0695-34 67 21\r\n1032;551553-1910;Great Lakes Food Market;2732 Baker Blvd.;Eugene;OR;97403;USA;(503) 555-7555";

            var actual = string.Join("\r\n", customers.Select(x => x.ToString()));

            Assert.Equal(expectedOutput,actual);
        }

        public void TrustorDb_Loads_Accounts()
        {
            var accounts = _context.Customers;

            var expectedOutput =
                "13019;1005;1488.80\r\n13020;1005;613.20\r\n13093;1024;695.62\r\n13128;1032;392.20\r\n13130;1032;4807.00";
            var actual = string.Join("\r\n", accounts.Select(x => x.ToString()));

            Assert.Equal(expectedOutput, actual);
        }
    }
}
