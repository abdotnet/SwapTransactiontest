using System;

namespace SwapMoney.DeveloperTest.Types
{
    public class SendTransactionRequest
    {
        public string CreditorAccountNumber { get; set; }

        public string DebtorAccountNumber { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentScheme PaymentScheme { get; set; }
    }

    public class SendTransactionV2Request : SendTransactionRequest
    {
        public string DataStoreType { get; set; } = "Backup";
    }
}
