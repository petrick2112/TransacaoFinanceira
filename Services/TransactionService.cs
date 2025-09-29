using System;
using TransacaoFinanceira.Repositories;
using TransacaoFinanceira.Utils;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Services
{
    public class TransactionService
    {
        private readonly IAccountRepository _repo;
        private readonly SimpleLogger _logger;

        public TransactionService(IAccountRepository repo, SimpleLogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public void Transferir(int correlationId, long contaOrigem, long contaDestino, decimal valor)
        {
            if (valor <= 0)
            {
                _logger.Error($"Transacao numero {correlationId} foi cancelada: valor inválido.");
                return;
            }

            var origem = _repo.GetByAccountNumber(contaOrigem);
            if (origem == null)
            {
                _logger.Info($"Transacao numero {correlationId} foi cancelada: conta de origem {contaOrigem} não encontrada.");
                return;
            }

            if (origem.Balance < valor)
            {
                _logger.Info($"Transacao numero {correlationId} foi cancelada por falta de saldo");
                return;
            }

            var destino = _repo.GetByAccountNumber(contaDestino);
            if (destino == null)
            {
                _logger.Info($"Transacao numero {correlationId} foi cancelada: conta de destino {contaDestino} não encontrada.");
                return;
            }

            origem.Balance -= valor;
            destino.Balance += valor;

            var ok1 = _repo.Update(origem);
            var ok2 = _repo.Update(destino);

            if (ok1 && ok2)
            {
                _logger.Info($"Transacao numero {correlationId} foi efetivada com sucesso! Novos saldos: Conta Origem:{origem.Balance} | Conta Destino: {destino.Balance}");
            }
            else
            {
                _logger.Error($"Transacao numero {correlationId} falhou ao atualizar dados.");
            }
        }
    }
}
