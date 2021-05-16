using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models;

namespace BizLibrary.Repositories.Interfaces
{
    interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();
        IEnumerable<Role> GetAllAccountsbyRoleId(int roleId);
        Account GetAccountById(int accountId);
        void AddAccount(Account addedAccount);
        void ChangeAccount(Account changedAccount);
        void DelAccount(int accountId);
    }
}
