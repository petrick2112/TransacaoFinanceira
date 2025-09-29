using System;

namespace TransacaoFinanceira.Models
{
    public class Account
    {
        public Account(long accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public long AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
