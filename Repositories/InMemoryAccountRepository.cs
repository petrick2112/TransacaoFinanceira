using System;
using System.Collections.Generic;
using System.Linq;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Repositories
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly List<Account> _table;
        private readonly object _lock = new object();

        public InMemoryAccountRepository()
        {
            _table = new List<Account>
            {
                new Account(938485762L, 180),
                new Account(347586970L, 1200),
                new Account(2147483649L, 0),
                new Account(675869708L, 4900),
                new Account(238596054L, 478),
                new Account(573659065L, 787),
                new Account(210385733L, 10),
                new Account(674038564L, 400),
                new Account(563856300L, 1200)
            };
        }

        public Account GetByAccountNumber(long accountNumber)
        {
            lock (_lock)
            {
                return _table.Find(a => a.AccountNumber == accountNumber);
            }
        }

        public bool Update(Account account)
        {
            if (account == null) return false;
            lock (_lock)
            {
                try
                {
                    _table.RemoveAll(a => a.AccountNumber == account.AccountNumber);
                    _table.Add(new Account(account.AccountNumber, account.Balance));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public IEnumerable<Account> GetAll()
        {
            lock (_lock)
            {
                return _table.Select(a => new Account(a.AccountNumber, a.Balance)).ToList();
            }
        }
    }
}
