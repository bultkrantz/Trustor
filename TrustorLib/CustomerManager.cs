using System;
using System.Collections.Generic;
using System.Linq;
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
            return customer;
        }

        public void DeleteCustomer(int customerNumber)
        {
            var customerToRemove = _context.Customers.FirstOrDefault(x => x.CustomerNumber == customerNumber);
            if (customerToRemove == null)
            {
                throw new NullReferenceException("**** Ingen kund med det kundnummret hittades.Tryck [Enter] för att fortsätta. ****");
            }
            var customerAccounts = _context.Accounts.Where(x => x.CustomerNumber == customerNumber).ToList();
            foreach (var account in customerAccounts)
            {
                if (account.Balance > 0)
                {
                    throw new Exception("**** Kunden har konton med ett saldo över 0, och kan därför ej raderas. Tryck [Enter] för att fortsätta. ****");
                }
                _context.Accounts.Remove(account);
            }
            _context.Customers.Remove(customerToRemove);
        }

        public List<Customer> SearchCustomer(string search)
        {
            return _context.Customers.Where(e => e.CompanyName.ToLower().Contains(search.ToLower()) || e.City.ToLower().Contains(search.ToLower())).ToList();
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
