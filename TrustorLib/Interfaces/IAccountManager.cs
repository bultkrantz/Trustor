using TrustorLib.Models;

namespace TrustorLib.Interfaces
{
    public interface IAccountManager
    {
        Account CreateAccount(int customerNumber);
        void DeleteAccount(int accountNumber);
        void NewDeposit(int accountNumber, decimal amount);
        decimal NewWithdrawal(int accountNumber, decimal amount);
        int NewTransfer(int fromAccountNumber, int toAccountNumber, decimal amount);
    }
}
