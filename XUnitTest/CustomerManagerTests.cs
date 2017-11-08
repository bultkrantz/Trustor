using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrustorLib;
using TrustorLib.Interfaces;
using TrustorLib.Models;
using Xunit;

namespace XUnitTest
{
    public class CustomerManagerTests
    {
        private readonly TrustorDb _context;
        private readonly ICustomerManager _sut;

        public CustomerManagerTests()
        {
            _context = new TrustorDb(Path.Combine(Environment.CurrentDirectory, @"Database\", "bankdata-small.txt"));
            _sut = new CustomerManager(_context);
        }

        [Fact]
        public void Search_Customer_Test()
        {
            var customerCount = _context.Customers.Where(e => e.CompanyName.Contains("F") || e.City.Contains("F")).Count();

            Assert.Equal(customerCount, _sut.SearchCustomer("F").Count);
        }

        [Fact]
        public void Show_Customer_Info_Test()
        {
            var customer = _context.Customers.FirstOrDefault(x => x.CustomerNumber == 1050);
            var customerAccounts = _context.Accounts.Where(x => x.CustomerNumber == 1050).ToList();
            
            Assert.Equal(Tuple.Create(customer, customerAccounts), _sut.ShowCustomerInfo(1050));
        }
    }
}
