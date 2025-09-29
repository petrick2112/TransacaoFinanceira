using TransacaoFinanceira.Models;
using System.Collections.Generic;

namespace TransacaoFinanceira.Repositories
{
    public interface IAccountRepository
    {
        Account GetByAccountNumber(long accountNumber);
        bool Update(Account account);
        IEnumerable<Account> GetAll();
    }
}
