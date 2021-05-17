using BizLibrary.Repositories.Interfaces;
using ModelsLibrary.EF;
using ModelsLibrary.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BizLibrary.Repositories.Implementations
{
	class AccountRepository: IAccountRepository
    {
		private ModelsManager _modelManager = new ModelsManager();

		public void AddAccount(Account addedAccount)
		{
			_modelManager.Accounts.Add(addedAccount);

			_modelManager.Entry(addedAccount).State = EntityState.Added;
			_modelManager.SaveChanges();
		}

		public void ChangeAccount(Account changedAccount)
		{
			var foundToEdit = _modelManager.Accounts.Find(changedAccount.Id);
			foundToEdit = changedAccount; // --question

			_modelManager.Entry(foundToEdit).State = EntityState.Modified;
			_modelManager.SaveChanges();
		}

		public void DelAccount(int accountId)
		{
			var foundToDel = _modelManager.Accounts.Find(accountId);
			_modelManager.Accounts.Remove(foundToDel);

			_modelManager.Entry(foundToDel).State = EntityState.Deleted;
			_modelManager.SaveChanges();
		}

		public Account GetAccountById(int accountId)
		{
			return _modelManager.Accounts.Find(accountId);
		}

		public IEnumerable<Account> GetAllAccounts()
		{
			return _modelManager.Accounts;
		}

		public IEnumerable<Account> GetAllAccountsbyRoleId(int roleId)
		{
			return _modelManager.Accounts.Where(obj => obj.RoleId == roleId);
		}
	}
}
