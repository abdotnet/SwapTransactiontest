

using SwapMoney.DeveloperTest.Types;

namespace SwapMoney.DeveloperTest.Store
{
    public class BackupTransactionRepository : IBackupTransactionRepository
    {
        public Account GetAccount(string accountNumber)
        {
            // Access backup data base to retrieve account, code removed for brevity 
            return new Account();
        }

        public void UpdateAccount(Account account)
        {
            // Update account in backup database, code removed for brevity
        }
    }
}
