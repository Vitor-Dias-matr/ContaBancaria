using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Teste.Context;
using Teste.Service.Interface;
using Teste.ViewModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Teste.Service
{
    public class ExtratoService : IExtratoService
    {
        private readonly Contexto _context;

        public ExtratoService(Contexto context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovimentacaoViewModel>> ObterExtratoPorDiaAsync(DateTime dia, int conta)
        {
            return await _context.Movimentacoes
                .Where(m => m.Data.Date == dia.Date && m.NumeroContaOrigem == conta)
                .Select(m => new MovimentacaoViewModel
                {
                    Id = m.Id,
                    Tipo = m.Tipo,
                    Data = m.Data,
                    Valor = m.Valor,
                    NumeroContaOrigem = m.NumeroContaOrigem,
                    NumeroContaDestino = m.NumeroContaDestino
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MovimentacaoViewModel>> ObterExtratoPorMesAsync(int ano, int mes , int conta)
        {
            return await _context.Movimentacoes
                .Where(m => m.Data.Year == ano && m.Data.Month == mes && m.NumeroContaOrigem == conta)
                .Select(m => new MovimentacaoViewModel
                {
                    Id = m.Id,
                    Tipo = m.Tipo,
                    Data = m.Data,
                    Valor = m.Valor,
                    NumeroContaOrigem = m.NumeroContaOrigem,
                    NumeroContaDestino = m.NumeroContaDestino
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MovimentacaoViewModel>> ObterExtratoPorIntervaloAsync(DateTime inicio, DateTime fim, int conta)
        {
            return await _context.Movimentacoes
                .Where(m => m.Data >= inicio && m.Data <= fim && m.NumeroContaOrigem == conta)
                .Select(m => new MovimentacaoViewModel
                {
                    Id = m.Id,
                    Tipo = m.Tipo,
                    Data = m.Data,
                    Valor = m.Valor,
                    NumeroContaOrigem = m.NumeroContaOrigem,
                    NumeroContaDestino = m.NumeroContaDestino
                })
                .ToListAsync();
        }
    }
}
