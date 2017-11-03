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
            var newCustomer = _customerManager.CreateCustomer(customer);
            return "**** " + newCustomer.CompanyName + " skapat. Tryck [Enter] för att fortsätta. ****";
        }
        public string DeleteCustomer(int customerNumber)
        {
            var result = _customerManager.DeleteCustomer(customerNumber);
            if (result == 2)
            {
                return "**** Kunden har konton med ett saldo över 0, och kan därför ej raderas. Tryck [Enter] för att fortsätta. ****";
            }
            else if (result == 1)
            {
                return "**** Ingen kund med det kundnummret hittades. Tryck [Enter] för att fortsätta. ****";
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
        public string NewTransfer(int fromAccountNumber, int toAccountNumber, decimal amount)
        {
            var result = _accountManager.NewTransfer(fromAccountNumber, toAccountNumber, amount);
            if (result == 1)
            {
                return $"Konto med kontonummer {fromAccountNumber} hittades inte. Tryck [Enter] för att fortsätta.";
            }
            else if (result == 2)
            {
                return $"Konto med kontonummer {toAccountNumber} hittades inte. Tryck [Enter] för att fortsätta.";
            }
            else if (result == 3)
            {
                return $"Saldot på konto med kontonummer {fromAccountNumber} är mindre än {amount}, transaktion avbruten. Tryck [Enter] för att fortsätta.";
            }
            return $"**** {amount}kr överfört från konto {fromAccountNumber} till konto {toAccountNumber}. Tryck [Enter] för att fortsätta. ****";
        }
    }
}
