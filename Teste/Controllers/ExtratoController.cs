using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Teste.Service.Interface;

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
        public async Task<IActionResult> PorDia(DateTime dia)
        {
            var extrato = await _extratoService.ObterExtratoPorDiaAsync(dia);
            return View(extrato);
        }

        [HttpGet]
        public async Task<IActionResult> PorMes(int ano, int mes)
        {
            var extrato = await _extratoService.ObterExtratoPorMesAsync(ano, mes);
            return View(extrato);
        }

        [HttpGet]
        public async Task<IActionResult> PorIntervalo(DateTime inicio, DateTime fim)
        {
            var extrato = await _extratoService.ObterExtratoPorIntervaloAsync(inicio, fim);
            return View(extrato);
        }
    }
}
