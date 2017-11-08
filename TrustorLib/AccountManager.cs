using System;
using System.Collections.Generic;
using System.Linq;
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

        public int NewTransfer(int fromAccountNumber, int toAccountNumber, decimal amount)
        {
            var accounts = _context.Accounts;
            var fromAccount = accounts.FirstOrDefault(x => x.AccountNumber == fromAccountNumber);
            var toAccount = accounts.FirstOrDefault(x => x.AccountNumber == toAccountNumber);
            if (fromAccount == null)
            {
                return 1;
            }
            else if (toAccount == null)
            {
                return 2;
            }
            else if (fromAccount.Balance < amount)
            {
                return 3;
            }

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;
            return 4;
        }
    }
}
