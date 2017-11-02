using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrustorLib.Interfaces;
using TrustorLib.Models;

namespace TrustorLib
{
    public class CustomerManager : ICustomerManager
    {
        private TrustorDb _context;

        public CustomerManager()
        {
            string fileName = "bankdata.txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"Database\", fileName);
            _context = new TrustorDb(path);
        }
        public Customer CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            return customer;
        }

        public void DeleteCustomer(int customerNumber)
        {
            throw new NotImplementedException();
        }

        public List<Customer> SearchCustomer(string search)
        {
            return _context.Customers.Where(e => e.CompanyName.Contains(search) || e.City.Contains(search)).ToList();
        }

        public Customer ShowCustomerInfo(int customerNumber)
        {
            return _context.Customers.FirstOrDefault(x => x.CustomerNumber == customerNumber);
        }
    }
}
