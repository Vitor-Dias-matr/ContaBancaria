using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Teste.Service.Interface;
using Teste.ViewModel;

namespace Teste.Controllers
{
    public class ContaController : Controller
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarContaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var conta = await _contaService.CriarContaAsync(model.Responsavel);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet] 
        public IActionResult Depositar(int id)
        {
            // Cria o ViewModel com o número da conta
            var model = new DepositarViewModel
            {
                NumeroConta = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Depositar(DepositarViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _contaService.DepositarAsync(model.NumeroConta, model.Valor);
                return RedirectToAction("Index", "Home"); // Redireciona para a lista de contas
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Sacar(int id)
        {
            var model = new SacarViewModel 
            {
                NumeroConta = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Sacar(SacarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            try
            {
                await _contaService.SacarAsync(model.NumeroConta, model.Valor);
                return Json(new { success = true });
            }
            catch (ArgumentException ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Ocorreu um erro inesperado. Tente novamente." });
            }
        }

        [HttpGet]
        public IActionResult Transferir(int id)
        {
            var model = new TransferirViewModel
            {
                ContaOrigem = id 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Transferir(TransferirViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _contaService.TransferirAsync(model.ContaOrigem, model.ContaDestino, model.Valor);
                return RedirectToAction("Index", "Home"); // Redireciona para a lista de contas
            }
            return View(model);
        }

    }
}
