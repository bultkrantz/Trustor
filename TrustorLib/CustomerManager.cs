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
        private readonly TrustorDb _context;

        public CustomerManager(TrustorDb context)
        {
            _context = context;
        }
        public Customer CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public int DeleteCustomer(int customerNumber)
        {
            var customerToRemove = _context.Customers.FirstOrDefault(x => x.CustomerNumber == customerNumber);
            if (customerToRemove == null)
            {
                return 1;
            }
            var customerAccounts = _context.Accounts.Where(x => x.CustomerNumber == customerNumber).ToList();
            foreach (var account in customerAccounts)
            {
                if (account.Balance > 0)
                {
                    return 2;
                }
                _context.Accounts.Remove(account);
            }
            _context.Customers.Remove(customerToRemove);
            _context.SaveChanges();
            return 3;
        }

        public List<Customer> SearchCustomer(string search)
        {
            return _context.Customers.Where(e => e.CompanyName.Contains(search) || e.City.Contains(search)).ToList();
        }

        public Tuple<Customer, List<Account>> ShowCustomerInfo(int customerNumber)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.CustomerNumber == customerNumber);
            var account = _context.Accounts.Where(x => customer != null && x.CustomerNumber == customer.CustomerNumber).ToList();

            return Tuple.Create(customer, account);
        }

        public int CreateNewCustomerNumber()
        {
            return _context.Customers.Max(x => x.CustomerNumber) + 1;
        }
    }
}
