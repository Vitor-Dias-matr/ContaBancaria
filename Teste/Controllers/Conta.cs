using Microsoft.AspNetCore.Mvc;

namespace Teste.Controllers
{
    public class Conta : Controller
    {
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Depositar()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Sacar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Tranferir()
        {
            return View();
        }

    }
}
