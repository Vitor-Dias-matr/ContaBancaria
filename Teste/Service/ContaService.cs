using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teste.Models;
using Teste.Repository.Interface;
using Teste.Service.Interface;

namespace Teste.Service
{
    public class ContaService : IContaService
    {
        private readonly IGenericRepository<Conta> _contaRepository;

        public ContaService(IGenericRepository<Conta> contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<Conta> CriarContaAsync(string responsavel)
        {
            var novaConta = new Conta(responsavel);
            await _contaRepository.AddAsync(novaConta);
            return novaConta;
        }

        public async Task<IEnumerable<Conta>> ObterTodasContasAsync()
        {
            return await _contaRepository.GetAllAsync();
        }

        public async Task DepositarAsync(int numeroConta, decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do depósito deve ser positivo.");
            }

            var conta = await _contaRepository.GetByIdAsync(numeroConta);
            if (conta == null)
            {
                throw new InvalidOperationException("Conta não encontrada.");
            }

            conta.Saldo += valor;
            await _contaRepository.UpdateAsync(conta);
            await _contaRepository.SaveChangesAsync();
        }

        public async Task SacarAsync(int numeroConta, decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do saque deve ser positivo.");
            }

            var conta = await _contaRepository.GetByIdAsync(numeroConta);
            if (conta == null)
            {
                throw new InvalidOperationException("Conta não encontrada.");
            }

            if (conta.Saldo < valor)
            {
                throw new InvalidOperationException("Saldo insuficiente.");
            }

            conta.Saldo -= valor;
            await _contaRepository.UpdateAsync(conta);
            await _contaRepository.SaveChangesAsync();
        }

        public async Task TransferirAsync(int numeroContaOrigem, int numeroContaDestino, decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor da transferência deve ser positivo.");
            }

            var contaOrigem = await _contaRepository.GetByIdAsync(numeroContaOrigem);
            var contaDestino = await _contaRepository.GetByIdAsync(numeroContaDestino);

            if (contaOrigem == null || contaDestino == null)
            {
                throw new InvalidOperationException("Uma ou ambas as contas não foram encontradas.");
            }

            if (contaOrigem.Saldo < valor)
            {
                throw new InvalidOperationException("Saldo insuficiente.");
            }

            contaOrigem.Saldo -= valor;
            contaDestino.Saldo += valor;

            await _contaRepository.UpdateAsync(contaOrigem);
            await _contaRepository.UpdateAsync(contaDestino);
            await _contaRepository.SaveChangesAsync();
        }
    }
}
