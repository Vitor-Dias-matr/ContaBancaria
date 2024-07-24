using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Teste.Models;
using Teste.Service.Interface;
using Teste.ViewModel;

namespace Teste.Controllers
{
    public class ContaController : Controller
    {
        private readonly IContaService _contaService;
        private readonly IMapper _mapper;

        public ContaController(IContaService contaService, IMapper mapper)
        {
            _contaService = contaService;
            _mapper = mapper;
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
                var conta = _mapper.Map<Conta>(model);
                await _contaService.CriarContaAsync(conta.Responsavel);
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
                var movimentacao = _mapper.Map<Movimentacao>(model);
                await _contaService.DepositarAsync(movimentacao. NumeroContaOrigem, movimentacao.Valor);
                return RedirectToAction("Index", "Home");
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
            if (ModelState.IsValid)
            {
                var movimentacao = _mapper.Map<Movimentacao>(model);
                try
                {
                    await _contaService.SacarAsync(movimentacao.NumeroContaOrigem, movimentacao.Valor);
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
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
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
                var movimentacao = _mapper.Map<Movimentacao>(model);
                await _contaService.TransferirAsync(movimentacao.NumeroContaOrigem, movimentacao.NumeroContaDestino.Value, movimentacao.Valor);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

    }
}
