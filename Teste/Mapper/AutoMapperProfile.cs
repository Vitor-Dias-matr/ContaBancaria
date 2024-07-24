using AutoMapper;
using Teste.Models;
using Teste.ViewModel;

namespace Teste.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeamento entre CriarContaViewModel e Conta
            CreateMap<CriarContaViewModel, Conta>()
                .ForMember(dest => dest.NumeroConta, opt => opt.Ignore()); // Ignorar se o número da conta é gerado automaticamente

            // Mapeamento entre Conta e CriarContaViewModel
            CreateMap<Conta, CriarContaViewModel>()
                .ReverseMap();
            // Mapeamento entre DepositarViewModel e Movimentacao (Deposito)
            CreateMap<DepositarViewModel, Movimentacao>()
                .ConstructUsing(src => new Movimentacao(Movimentacao.TipoMovimentacao.Deposito,
                    src.Valor,
                    src.NumeroConta,
                    null)) // Ajusta os parâmetros do construtor
                .ForMember(dest => dest.Data, opt => opt.Ignore()) // Data é gerada automaticamente
                .ForMember(dest => dest.NumeroContaDestino, opt => opt.Ignore()); // Não aplicável para depósito


            CreateMap<SacarViewModel, Movimentacao>()
                .ConstructUsing(src => new Movimentacao(
                    Movimentacao.TipoMovimentacao.Saque,
                    src.Valor,
                    src.NumeroConta,
                    null)) // Ajusta os parâmetros do construtor
                .ForMember(dest => dest.Data, opt => opt.Ignore()) // Data é gerada automaticamente
                .ForMember(dest => dest.NumeroContaDestino, opt => opt.Ignore()); // Não aplicável para saque

            // Mapeamento entre TransferirViewModel e Movimentacao (Transferencia)
            CreateMap<TransferirViewModel, Movimentacao>()
                .ConstructUsing(src => new Movimentacao(
                    Movimentacao.TipoMovimentacao.Transferencia,
                    src.Valor,
                    src.ContaOrigem,
                    src.ContaDestino)) // Ajusta os parâmetros do construtor
                .ForMember(dest => dest.Data, opt => opt.Ignore()); // Data é gerada automaticamente
        }
    }
}
