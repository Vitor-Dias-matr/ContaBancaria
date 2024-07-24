using Microsoft.AspNetCore.Mvc;

namespace Teste.Controllers
{
    public class Movimentacao : Controller
    {
        public IActionResult Extrato()
        {
            return View();
        }
    }
}
