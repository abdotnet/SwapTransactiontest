using SwapMoney.DeveloperTest.Types;

namespace SwapMoney.DeveloperTest.Store
{
    public class TransactionRepository : ITransactionRepository
    {
        public Account GetAccount(string accountNumber)
        {
            return new Account();
        }

        public void UpdateAccount(Account account)
        {
        }
    }
}
