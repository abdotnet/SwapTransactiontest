
using SwapMoney.DeveloperTest.Types;
using SwapMoney.DeveloperTest.Store;


namespace SwapMoney.DeveloperTest.Services
{
    public class TransactionsService 
    {
        private ITransactionRepository _trasactionRepository;
        private IBackupTransactionRepository _backupTransactionRepository;
        public TransactionsService(ITransactionRepository trasactionRepository,
            IBackupTransactionRepository backupTransactionRepository )
        {
            _trasactionRepository = trasactionRepository;
            _backupTransactionRepository = backupTransactionRepository;
        }
        public SendTransactionResponse ProcessTransactionold(SendTransactionRequest request)
        {
            var dataStoreType = "Backup";

            Account account = null;

            if (dataStoreType == "Backup")
            {
                var transactionStore = new BackupTransactionRepository();
                account = transactionStore.GetAccount(request.DebtorAccountNumber);
            }
            else
            {
                var transactionStore = new TransactionRepository();
                account = transactionStore.GetAccount(request.DebtorAccountNumber);
            }

            var result = new SendTransactionResponse();

            if (request.PaymentScheme == PaymentScheme.Bacs)
            {
                if (account == null)
                {
                    result.Success = false;
                }
                else if (account.AllowedPaymentSchemes != AllowedPaymentSchemes.Bacs)
                {
                    result.Success = false;
                }
            }
            else if (request.PaymentScheme == PaymentScheme.FasterPayments)
            {
                if (account == null)
                {
                    result.Success = false;
                }
                else if (account.AllowedPaymentSchemes != AllowedPaymentSchemes.FasterPayments)
                {
                    result.Success = false;
                }
                else if (account.Balance < request.Amount)
                {
                    result.Success = false;
                }
            }
            else if (request.PaymentScheme == PaymentScheme.Chaps)
            {
                if (account == null)
                {
                    result.Success = false;
                }
                else if (account.AllowedPaymentSchemes != AllowedPaymentSchemes.Chaps)
                {
                    result.Success = false;
                }
                else if (account.Status != AccountStatus.Live)
                {
                    result.Success = false;
                }
            }

            if (result.Success)
            {
                account.Balance -= request.Amount;

                if (dataStoreType == "Backup")
                {
                    var transactionStore = new BackupTransactionRepository();
                    transactionStore.UpdateAccount(account);
                }
                else
                {
                    var transactionStore = new TransactionRepository();
                    transactionStore.UpdateAccount(account);
                }
            }

            return result;
        }

        /// <summary>
        /// Re-write ProcessTransaction() method to make it more testable.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SendTransactionResponse ProcessTransaction(SendTransactionV2Request request)
        {

            Account account;

            // This are functional service with process execution
            if (request.DataStoreType == DataStoreDefault.BackupType)
            {
                account = _backupTransactionRepository.GetAccount(request.DebtorAccountNumber);
            }
            else
            {
                account = _trasactionRepository.GetAccount(request.DebtorAccountNumber);
            }


            var result = new SendTransactionResponse();

            if (request.PaymentScheme == PaymentScheme.Bacs)
            {
                if (account == null)
                {
                    result.Success = false;
                }
                else if (account.AllowedPaymentSchemes != AllowedPaymentSchemes.Bacs)
                {
                    result.Success = false;
                }
            }
            else if (request.PaymentScheme == PaymentScheme.FasterPayments)
            {
                if (account == null)
                {
                    result.Success = false;
                }
                else if (account.AllowedPaymentSchemes != AllowedPaymentSchemes.FasterPayments)
                {
                    result.Success = false;
                }
                else if (account.Balance < request.Amount)
                {
                    result.Success = false;
                }
            }
            else if (request.PaymentScheme == PaymentScheme.Chaps)
            {
                if (account == null)
                {
                    result.Success = false;
                }
                else if (account.AllowedPaymentSchemes != AllowedPaymentSchemes.Chaps)
                {
                    result.Success = false;
                }
                else if (account.Status != AccountStatus.Live)
                {
                    result.Success = false;
                }
            }
            else
            {
                result.Success = true;
            }

            if (result.Success)
            {
                account.Balance -= request.Amount;

                if (request.DataStoreType == DataStoreDefault.BackupType)
                {
                    _backupTransactionRepository.UpdateAccount(account);
                }
                else
                {
                    _trasactionRepository.UpdateAccount(account);
                }
            }

            return result;
        }
    }
}
