using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrustorLib.Interfaces;
using TrustorLib.Models;

namespace TrustorLib
{
    public class TrustorBankSystem
    {
        private IAccountManager _accountManager;
        private ICustomerManager _customerManager;

        public TrustorBankSystem(ICustomerManager customerManager, IAccountManager accountManager)
        {
            _accountManager = accountManager;
            _customerManager = customerManager;
        }

        public string SearchCustomer(string search)
        {
            var customers = _customerManager.SearchCustomer(search);

            var stringBuilder = new StringBuilder();

            if (customers.Count != 0)
            {
                stringBuilder.AppendLine($"\nKunder som innehåller '{search}':\n");

                foreach (var customer in customers)
                {
                    stringBuilder.AppendLine($"{customer.CustomerNumber}: {customer.CompanyName}");
                }
            }
            else
            {
                stringBuilder.AppendLine("\nInga kunder hittades");
            }

            stringBuilder.AppendLine("\n\nTryck [Enter] för att fortsätta.");

            return stringBuilder.ToString();
        }

        public string ShowCustomerInfo(int customerNumber)
        {
            var customerInfo = _customerManager.ShowCustomerInfo(customerNumber);
            var customer = customerInfo.Item1;
            var accounts = customerInfo.Item2;
            var stringBuilder = new StringBuilder();

            if (customer != null)
            {
                stringBuilder.AppendLine($"Kundnummer {customer.CustomerNumber}");
                stringBuilder.AppendLine($"Organisationsnummer: {customer.OrgNumber}");
                stringBuilder.AppendLine($"Namn: {customer.CompanyName}");
                stringBuilder.AppendLine($"Address: {customer.Address}");
                stringBuilder.AppendLine("\n\nKonton:\n");

                foreach (var account in accounts)
                {
                    stringBuilder.AppendLine($"{account.AccountNumber}: {account.Balance} kr");
                }

                stringBuilder.AppendLine($"\nTotala tillgångar: {accounts.Sum(x => x.Balance)} kr");
            }
            else
            {
                stringBuilder.AppendLine($"\nKund med kundnummer {customerNumber} hittades ej.");
            }

            stringBuilder.AppendLine("\n\nTryck [Enter] för att fortsätta");

            return stringBuilder.ToString();
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
            _accountManager.CreateAccount(customer.CustomerNumber);
            var newCustomer = _customerManager.CreateCustomer(customer);
            return "**** " + newCustomer.CompanyName + " skapat. Tryck [Enter] för att fortsätta. ****";
        }
        public string DeleteCustomer(int customerNumber)
        {
            try
            {
                _customerManager.DeleteCustomer(customerNumber);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "**** Kunden har raderats. Tryck [Enter] för att fortsätta. ****";
        }
        public string CreateAccount(int customerNumber)
        {
            var customer = _customerManager.ShowCustomerInfo(customerNumber);

            if (customer.Item1 != null)
            {
                var account = _accountManager.CreateAccount(customerNumber);
                return $"\nNytt konto med nummer {account.AccountNumber} har skapats.\n\n**** Tryck [Enter] för att fortsätta. ****";
            }

            return "Kunden kunde inte hittas.\n\n**** Tryck [Enter] för att fortsätta. ****";
        }
        public string DeleteAccount(int accountNumber)
        {
            try
            {
                _accountManager.DeleteAccount(accountNumber);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "**** Kontot har raderats. Tryck [Enter] för att fortsätta. ****";
        }
        public void NewDeposit(int accountNumber, decimal amount)
        {
            _accountManager.NewDeposit(accountNumber, amount);
        }
        public string NewWithdrawal(int accountNumber, decimal amount)
        {
            decimal result = 0;

            try
            {
                result = _accountManager.NewWithdrawal(accountNumber, amount);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return $"Saldo kvar på konto {accountNumber} är {result}. Du tog ut {amount}";
        }
        public string NewTransfer(int fromAccountNumber, int toAccountNumber, decimal amount)
        {
            try
            {
                _accountManager.NewTransfer(fromAccountNumber, toAccountNumber, amount);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return $"**** {amount}kr överfört från konto {fromAccountNumber} till konto {toAccountNumber}. Tryck [Enter] för att fortsätta. ****";
        }
    }
}
