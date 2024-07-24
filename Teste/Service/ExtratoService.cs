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

        public async Task<IEnumerable<MovimentacaoViewModel>> ObterExtratoPorDiaAsync(DateTime dia)
        {
            return await _context.Movimentacoes
                .Where(m => m.Data.Date == dia.Date)
                .Select(m => new MovimentacaoViewModel
                {
                    Id = m.Id,
                    Tipo = (int)m.Tipo,
                    Data = m.Data,
                    Valor = m.Valor,
                    NumeroContaOrigem = m.NumeroContaOrigem,
                    NumeroContaDestino = m.NumeroContaDestino
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MovimentacaoViewModel>> ObterExtratoPorMesAsync(int ano, int mes)
        {
            return await _context.Movimentacoes
                .Where(m => m.Data.Year == ano && m.Data.Month == mes)
                .Select(m => new MovimentacaoViewModel
                {
                    Id = m.Id,
                    Tipo = (int)m.Tipo,
                    Data = m.Data,
                    Valor = m.Valor,
                    NumeroContaOrigem = m.NumeroContaOrigem,
                    NumeroContaDestino = m.NumeroContaDestino
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MovimentacaoViewModel>> ObterExtratoPorIntervaloAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Movimentacoes
                .Where(m => m.Data >= inicio && m.Data <= fim)
                .Select(m => new MovimentacaoViewModel
                {
                    Id = m.Id,
                    Tipo = (int)m.Tipo,
                    Data = m.Data,
                    Valor = m.Valor,
                    NumeroContaOrigem = m.NumeroContaOrigem,
                    NumeroContaDestino = m.NumeroContaDestino
                })
                .ToListAsync();
        }
    }
}
