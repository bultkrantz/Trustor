using System;
using System.Collections.Generic;
using System.Text;
using TrustorLib.Interfaces;
using TrustorLib.Models;

namespace TrustorLib
{
    public class TrustorBankSystem
    {
        private IAccountManager _accountManager;
        private ICustomerManager _customerManager;

        public TrustorBankSystem(IAccountManager accountManager, ICustomerManager customerManager)
        {
            _accountManager = accountManager;
            _customerManager = customerManager;
        }

        public TrustorBankSystem(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        public string SearchCustomer(string search)
        {
            var customers = _customerManager.SearchCustomer(search);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"\nKunder som innehåller '{search}':\n");

            foreach (var customer in customers)
            {
                stringBuilder.AppendLine($"{customer.CustomerNumber}: {customer.CompanyName}");
            }

            stringBuilder.AppendLine("\n\nTryck [Enter] för att fortsätta.");

            return stringBuilder.ToString();
        }
        public Customer ShowCustomerInfo(int customerNumber)
        {
            return _customerManager.ShowCustomerInfo(customerNumber);
        }
        public string CreateCustomer(Customer customer)
        {
            var creationDenied = "får ej lämnas tomt, kunde ej skapa kund. Tryck [Enter] för att fortsätta.";
            if (string.IsNullOrWhiteSpace(customer.CompanyName))
            {
                return $"**** Företagsnamn {creationDenied} ****";
            }
            else if (string.IsNullOrWhiteSpace(customer.OrgNumber))
            {
                return $"**** Organisationsnummer {creationDenied} ****";
            }
            else if (string.IsNullOrWhiteSpace(customer.Address))
            {
                return $"**** Adress {creationDenied} ****";
            }
            else if (string.IsNullOrWhiteSpace(customer.PostalCode))
            {
                return $"**** Postnummer {creationDenied} ****";
            }
            else if (string.IsNullOrWhiteSpace(customer.Region))
            {
                return $"**** Postort {creationDenied} ****";
            }

            customer.CustomerNumber = _customerManager.CreateNewCustomerNumber();
            var newCustomer = _customerManager.CreateCustomer(customer);
            return "**** " + newCustomer.CompanyName + " skapat. Tryck [Enter] för att fortsätta. ****";
        }
        public string DeleteCustomer(int customerNumber)
        {
            var result = _customerManager.DeleteCustomer(customerNumber);
            if (result == 2)
            {
                return "**** Kunden har konton, och kan därför ej raderas. Tryck [Enter] för att fortsätta. ****";
            }
            else if (result == 1)
            {
                return "**** Ingen kund med det kundnummret hittades. Tryck [Enter] för att fortsätta. ****";
            }
            return "**** Kunden har raderats. Tryck [Enter] för att fortsätta. ****";
        }
        public Account CreateAccount(Account account)
        {
            return _accountManager.CreateAccount(account);
        }
        public void DeleteAccount(int accountNumber)
        {
            _accountManager.DeleteAccount(accountNumber);
        }
        public void NewDeposit(int accountNumber, decimal amount)
        {
            _accountManager.NewDeposit(accountNumber, amount);
        }
        public void NewWithdrawal(int accountNumber, decimal amount)
        {
            _accountManager.NewWithdrawal(accountNumber, amount);
        }
        public void NewTransfer(int fromAccountNumber, int toAccountNumber, decimal amount)
        {
            _accountManager.NewTransfer(fromAccountNumber, toAccountNumber, amount);
        }
    }
}
