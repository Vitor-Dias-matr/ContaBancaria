using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Teste.ViewModel;

namespace Teste.Service.Interface
{
    public interface IExtratoService
    {
        Task<IEnumerable<MovimentacaoViewModel>> ObterExtratoPorDiaAsync(DateTime dia, int conta);
        Task<IEnumerable<MovimentacaoViewModel>> ObterExtratoPorMesAsync(int ano, int mes, int conta);
        Task<IEnumerable<MovimentacaoViewModel>> ObterExtratoPorIntervaloAsync(DateTime inicio, DateTime fim, int conta);
    }
}
