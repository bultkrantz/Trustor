using System;
using System.Collections.Generic;
using System.Text;
using TrustorLib.Models;

namespace TrustorLib
{
    public class TrustorBankSystem
    {
        public List<Customer> SearchCustomer(string name)
        {
            throw new NotImplementedException();
        }
        public Customer ShowCustomerInfo(int customerNumber)
        {
            throw new NotImplementedException();
        }
        public Customer CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
        public void DeleteCustomer(int customerNumber)
        {
            throw new NotImplementedException();
        }
        public Account CreateAccount(Account account)
        {
            throw new NotImplementedException();
        }
        public void DeleteAccount(int accountNumber)
        {
            throw new NotImplementedException();
        }
        public void NewDeposit(int accountNumber, decimal deposit)
        {
            throw new NotImplementedException();
        }
        public void NewWithdrawal(int accountNumber, decimal withdrawal)
        {
            throw new NotImplementedException();
        }
        public void NewTransfer(int fromAccountNumber, int toAccountNumber, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
