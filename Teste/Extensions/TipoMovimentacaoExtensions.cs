using Teste.Models;

namespace Teste.Extensions
{
    public static class TipoMovimentacaoExtensions
    {
        public static string ToFriendlyString(this Movimentacao.TipoMovimentacao tipo)
        {
            switch (tipo)
            {
                case Movimentacao.TipoMovimentacao.Deposito:
                    return "Depósito";
                case Movimentacao.TipoMovimentacao.Saque:
                    return "Saque";
                case Movimentacao.TipoMovimentacao.Transferencia:
                    return "Transferência";
                default:
                    return tipo.ToString();
            }
        }
    }
}
