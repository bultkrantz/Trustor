using System;
using System.Collections.Generic;
using System.Text;
using TrustorLib.Interfaces;
using TrustorLib.Models;

namespace TrustorLib
{
    public class AccountManager : IAccountManager
    {
        private readonly TrustorDb _context;

        public AccountManager(TrustorDb context)
        {
            _context = context;
        }

        public Account CreateAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccount(int accountNumber)
        {
            throw new NotImplementedException();
        }

        public void NewDeposit(int accountNumber, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void NewWithdrawal(int accountNumber, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void NewTransfer(int fromAccountNumber, int toAccountNumber, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
