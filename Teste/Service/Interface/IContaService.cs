using System.Collections.Generic;
using System.Threading.Tasks;
using Teste.Models;

namespace Teste.Service.Interface
{
    public interface IContaService
    {
        Task<IEnumerable<Conta>> ObterTodasContasAsync();
        Task<Conta> CriarContaAsync(string responsavel);
        Task DepositarAsync(int numeroConta, decimal valor);
        Task SacarAsync(int numeroConta, decimal valor);
        Task TransferirAsync(int numeroContaOrigem, int numeroContaDestino, decimal valor);
    }
}
