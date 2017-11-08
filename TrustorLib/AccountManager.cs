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

        public Account CreateAccount(int customerNumber)
        {
            var account = new Account
            {
                CustomerNumber = customerNumber,
                AccountNumber = CreateNewAccountNumber(),
                Balance = 0
            };

            _context.Accounts.Add(account);

            return account;
        }

        public void DeleteAccount(int accountNumber)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
            var result = _context.Accounts.Remove(account);

            if (account == null)
            {
                throw new Exception($"Inget konto med kontonummer {accountNumber} hittades. Tryck [Enter] för att fortsätta.");
            }
            else if (!result)
            {
                throw new Exception("Kunde ej radera konto.");
            }
            else if (account.Balance > 0)
            {
                throw new Exception("Konto kunde ej raderas då det innehåller ett saldo större än 0.");
            }
        }

        public void NewDeposit(int accountNumber, decimal amount)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

            if (account == null || amount < 0)
            {
                return;
            }

            account.Balance += amount;

        }

        public decimal NewWithdrawal(int accountNumber, decimal amount)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

            if (account == null)
            {
                throw new NullReferenceException($"Konto {accountNumber} finns inte");
            }

            if (account.Balance < amount)
            {
                throw new ArgumentOutOfRangeException($"Saldot på kontot är lägre än {amount}");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException($"Inte ens möjligt.");
            }

            account.Balance -= amount;

            return account.Balance;
        }

        public void NewTransfer(int fromAccountNumber, int toAccountNumber, decimal amount)
        {
            var accounts = _context.Accounts;
            var fromAccount = accounts.FirstOrDefault(x => x.AccountNumber == fromAccountNumber);
            var toAccount = accounts.FirstOrDefault(x => x.AccountNumber == toAccountNumber);
            if (fromAccount == null)
            {
                throw new NullReferenceException($"Konto med kontonummer {fromAccountNumber} hittades inte. Tryck [Enter] för att fortsätta.");
            }
            else if (toAccount == null)
            {
                throw new NullReferenceException($"Konto med kontonummer { toAccountNumber } hittades inte. Tryck [Enter] för att fortsätta.");
            }
            else if (amount < 1)
            {
                throw new Exception($"Beloppet att föra över får ej vara lika med eller mindre än 1kr, du försökte föra över {amount}kr. Tryck [Enter] för att fortsätta.");
            }
            else if (fromAccount.Balance < amount)
            {
                throw new Exception($"Saldot på konto med kontonummer {fromAccountNumber} är mindre än {amount}, transaktion avbruten. Tryck [Enter] för att fortsätta.");
            }

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;
        }

        public int CreateNewAccountNumber()
        {
            return _context.Accounts.Max(x => x.AccountNumber) + 1;
        }
    }
}
