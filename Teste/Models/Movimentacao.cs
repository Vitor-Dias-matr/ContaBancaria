using System;

namespace Teste.Models
{
    public class Movimentacao
    {
        public Movimentacao(TipoMovimentacao tipo, decimal valor, int numeroContaOrigem, int? numeroContaDestino = null)
        {
            Tipo = tipo;
            Valor = valor;
            NumeroContaOrigem = numeroContaOrigem;
            NumeroContaDestino = numeroContaDestino;
            Data = DateTime.Now;
        }
        public int Id { get; set; } 
        public TipoMovimentacao Tipo { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public int NumeroContaOrigem { get; set; }
        public int? NumeroContaDestino { get; set; }

        public enum TipoMovimentacao
        {
            Deposito,
            Saque,
            Transferencia
        }
    }
}
