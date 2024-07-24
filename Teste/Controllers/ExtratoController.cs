using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Teste.Service.Interface;
using Teste.ViewModel;

namespace Teste.Controllers
{
    public class ExtratoController : Controller
    {
        private readonly IExtratoService _extratoService;

        public ExtratoController(IExtratoService extratoService)
        {
            _extratoService = extratoService;
        }

        [HttpGet]
        public IActionResult Index(int numeroConta)
        {
            var model = new ExtratoSearchViewModel
            {
                NumeroConta = numeroConta
            };
            return View(model);

        }
        [HttpGet] 
        public async Task<IActionResult> PorDia(DateTime dia, int numeroConta)
        {
            var extrato = await _extratoService.ObterExtratoPorDiaAsync(dia, numeroConta);
            return View(extrato);
        }

        [HttpGet]
        public async Task<IActionResult> PorMes(int ano, int mes, int numeroConta)
        {
            var extrato = await _extratoService.ObterExtratoPorMesAsync(ano, mes, numeroConta);
            return View(extrato);
        }

        [HttpGet]
        public async Task<IActionResult> PorIntervalo(DateTime inicio, DateTime fim, int numeroConta)
        {
            var extrato = await _extratoService.ObterExtratoPorIntervaloAsync(inicio, fim, numeroConta);
            return View(extrato);
        }
    }
}
