using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teste.Models;
using Teste.Repository.Interface;
using Teste.Service.Interface;
using static Teste.Models.Movimentacao;

namespace Teste.Service
{
    public class ContaService : IContaService
    {
        private readonly IGenericRepository<Conta> _contaRepository;
        private readonly IGenericRepository<Movimentacao> _movimentacaoRepository;

        public ContaService(IGenericRepository<Conta> contaRepository, IGenericRepository<Movimentacao> movimentacaoRepository)
        {
            _contaRepository = contaRepository;
            _movimentacaoRepository = movimentacaoRepository;
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

            // Registrar a movimentação
            var movimentacao = new Movimentacao(
                TipoMovimentacao.Deposito, valor, numeroConta);
            await _movimentacaoRepository.AddAsync(movimentacao);

            await _contaRepository.SaveChangesAsync();
            await _movimentacaoRepository.SaveChangesAsync();
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

            // Registrar a movimentação
            var movimentacao = new Movimentacao(
                TipoMovimentacao.Saque, valor, numeroConta);
            await _movimentacaoRepository.AddAsync(movimentacao);

            await _contaRepository.SaveChangesAsync();
            await _movimentacaoRepository.SaveChangesAsync();
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

            // Registrar a movimentação na conta origem
            var movimentacaoOrigem = new Movimentacao(
                TipoMovimentacao.Transferencia, valor, numeroContaOrigem, numeroContaDestino);
            await _movimentacaoRepository.AddAsync(movimentacaoOrigem);

            // Registrar a movimentação na conta destino
            var movimentacaoDestino = new Movimentacao(
                TipoMovimentacao.Transferencia, valor, numeroContaDestino, numeroContaOrigem);
            await _movimentacaoRepository.AddAsync(movimentacaoDestino);

            await _contaRepository.SaveChangesAsync();
            await _movimentacaoRepository.SaveChangesAsync();
        }
    }
}
