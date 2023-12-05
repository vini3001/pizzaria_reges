using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizzaria_reges.Data;
using pizzaria_reges.Models;

namespace pizzaria_reges.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IESContext _context;

        public HomePageController(IESContext context)
        {
            this._context = context;
        }
        public IActionResult Index(long? Id)
        {
            var pizzas = _context.Produto.OrderBy(i => i.Nome).ToList();

            Cliente cliente = new Cliente
            {
                Id = Id,
            };

            ViewModel viewModel = new ViewModel
            {
                cliente = cliente
            };

            ViewBag.clienteId = Id;
            ViewBag.pizzas = pizzas;
            return View(viewModel);
        }

        public IActionResult RedirectToCart(long Id)
        {
            var objectParams = new
            {
                Id
            };
            return RedirectToAction("Create", "CadPedido", objectParams);
        }
    }
}
