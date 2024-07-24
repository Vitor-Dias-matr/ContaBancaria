using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Teste.Models;
using Teste.Service.Interface;
using Teste.ViewModel;

namespace Teste.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContaService _contaService;

        public HomeController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contas = await _contaService.ObterTodasContasAsync();
            var model = new ContaListViewModel
            {
                Contas = contas
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
