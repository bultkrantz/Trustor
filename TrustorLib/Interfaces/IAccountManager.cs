using TrustorLib.Models;

namespace TrustorLib.Interfaces
{
    public interface IAccountManager
    {
        Account CreateAccount(Account account);
        void DeleteAccount(int accountNumber);
        void NewDeposit(int accountNumber, decimal amount);
        void NewWithdrawal(int accountNumber, decimal amount);
        int NewTransfer(int fromAccountNumber, int toAccountNumber, decimal amount);
    }
}
