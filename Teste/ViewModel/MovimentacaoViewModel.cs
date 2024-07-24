using System;

namespace Teste.ViewModel
{
    public class MovimentacaoViewModel
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public int NumeroContaOrigem { get; set; }
        public int? NumeroContaDestino { get; set; }
    }
}
