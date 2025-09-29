// using Xunit;
// using TransacaoFinanceira.Repositories;
// using TransacaoFinanceira.Services;
// using TransacaoFinanceira.Utils;

// namespace TransacaoFinanceira.Tests
// {
//     public class TestTransacaoFinanceira
//     {
//         private TransactionService CreateService(out InMemoryAccountRepository repo)
//         {
//             repo = new InMemoryAccountRepository();
//             var logger = new SimpleLogger(); // ainda escreve no console
//             return new TransactionService(repo, logger);
//         }

//         [Fact]
//         public void Transferir_DeveFalhar_SeSaldoInsuficiente()
//         {
//             var service = CreateService(out var repo);

//             // Conta 210385733 começa com saldo 10
//             service.Transferir(1, 210385733L, 938485762L, 1000m);

//             var origem = repo.GetByAccountNumber(210385733L);
//             Assert.Equal(10m, origem.Balance); // saldo não alterado
//         }

//         [Fact]
//         public void Transferir_DeveEfetivar_SeSaldoSuficiente()
//         {
//             var service = CreateService(out var repo);

//             // Conta 938485762 começa com saldo 180
//             service.Transferir(2, 938485762L, 2147483649L, 50m);

//             var origem = repo.GetByAccountNumber(938485762L);
//             var destino = repo.GetByAccountNumber(2147483649L);

//             Assert.Equal(130m, origem.Balance); // 180 - 50
//             Assert.Equal(50m, destino.Balance); // 0 + 50
//         }

//         [Fact]
//         public void Transferir_DeveFalhar_SeContaOrigemNaoExistir()
//         {
//             var service = CreateService(out var repo);

//             service.Transferir(3, 999999999L, 2147483649L, 10m);

//             var destino = repo.GetByAccountNumber(2147483649L);
//             Assert.Equal(0m, destino.Balance); // saldo intacto
//         }

//         [Fact]
//         public void Transferir_DeveFalhar_SeContaDestinoNaoExistir()
//         {
//             var service = CreateService(out var repo);

//             // Conta origem existe e tem saldo
//             service.Transferir(4, 938485762L, 999999999L, 10m);

//             var origem = repo.GetByAccountNumber(938485762L);
//             Assert.Equal(180m, origem.Balance); // saldo intacto
//         }

//         [Fact]
//         public void Transferir_DeveFalhar_SeValorInvalido()
//         {
//             var service = CreateService(out var repo);

//             service.Transferir(5, 938485762L, 2147483649L, 0m);

//             var origem = repo.GetByAccountNumber(938485762L);
//             var destino = repo.GetByAccountNumber(2147483649L);

//             Assert.Equal(180m, origem.Balance); // não alterou
//             Assert.Equal(0m, destino.Balance);
//         }
//     }
// }
