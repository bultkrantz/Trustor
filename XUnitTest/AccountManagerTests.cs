using System;
using System.IO;
using System.Linq;
using TrustorLib;
using TrustorLib.Interfaces;
using TrustorLib.Models;
using Xunit;

namespace XUnitTest
{
    public class AccountManagerTests
    {
        private readonly TrustorDb _context;
        private readonly IAccountManager _accountManager;

        public AccountManagerTests()
        {
            _context = new TrustorDb(Path.Combine(Environment.CurrentDirectory, @"Database\", "bankdata-small.txt"));
            _accountManager = new AccountManager(_context);
        }
        [Fact]
        public void Delete_Account_Test()
        {

            var accountToRemove = _accountManager.CreateAccount(99);

            _accountManager.DeleteAccount(accountToRemove.AccountNumber);

            Assert.DoesNotContain(accountToRemove, _context.Accounts);
        }

        [Fact]
        public void NewDeposit_Test()
        {
            var accountToDepositOn = _context.Accounts.FirstOrDefault();
            var cashBeforeDeposit = accountToDepositOn.Balance;
            var cashToDeposit = 100;

            var expectedBalance = cashToDeposit + cashBeforeDeposit;

            _accountManager.NewDeposit(accountToDepositOn.AccountNumber,cashToDeposit);

            Assert.Equal(expectedBalance,accountToDepositOn.Balance);
        }

        [Fact]
        public void NewWithdrawl_Test()
        {
            var account = _context.Accounts.FirstOrDefault();
            var cashBeforeWithdrawl = account.Balance;
            var cashToWithdrawl = account.Balance / 2;

            var expectedBalance = cashBeforeWithdrawl - cashToWithdrawl;

            _accountManager.NewWithdrawal(account.AccountNumber, cashToWithdrawl);

            Assert.Equal(expectedBalance,account.Balance);
        }

        [Fact]
        public void New_Deposit_Test()
        {
            var account = _context.Accounts.FirstOrDefault();
        
            _accountManager.NewDeposit(account.AccountNumber, 100);

            var expectedAmount = account.Balance += 100;

            Assert.Equal(account.Balance, expectedAmount);
        }
    }
}
