using SwapMoney.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwapMoney.DeveloperTest.Store
{
    public interface IRepository
    {
        Account GetAccount(string accountNumber);
        void UpdateAccount(Account account);
    }
}
