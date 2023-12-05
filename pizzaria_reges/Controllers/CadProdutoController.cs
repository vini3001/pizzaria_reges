using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizzaria_reges.Data;
using pizzaria_reges.Models;

namespace pizzaria_reges.Controllers
{
    public class CadProdutoController : Controller
    {
        private readonly IESContext _context;

        public CadProdutoController(IESContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produto.OrderBy(p => p.Id).ToListAsync());
        }

        public IActionResult Create()
        {
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime dataHoraLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);

            Produto modelo = new Produto
            {
                dataCadastro = dataHoraLocal,
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime dataHoraLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);

            if (ModelState.IsValid)
            {
                try
                {
                    produto.dataCadastro = dataHoraLocal;
                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex) 
                {
                    Console.WriteLine($"Erro ao salvar alterações: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Exceção interna: {ex.InnerException.Message}");
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(produto);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto= await _context.Produto.SingleOrDefaultAsync(m => m.Id == id);
            var carrinhos = _context.CarrinhoCompra.Where(cart => cart.fk_ProdutoID == id).ToList();

            if (produto == null)
            {
                return NotFound();
            }

            ViewBag.carrinho = carrinhos;
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long? id) {
            var produto = await _context.Produto.FindAsync(id);

            return View(produto);
        }

        public bool ProdutoExists(long? id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            var produto = await _context.Produto.FindAsync(id);
            var findCarrinhos = await _context.CarrinhoCompra.Where(cart => cart.fk_ProdutoID == id).ToListAsync();

            foreach (var carrinho in findCarrinhos) 
            {
                var findPedido = _context.Pedido.SingleOrDefault(p => p.Id == carrinho.fk_PedidoID);
                _context.Pedido.Remove(findPedido);
            }

            _context.CarrinhoCompra.RemoveRange(findCarrinhos);
            await _context.SaveChangesAsync();

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Produto produto)
        {
            var findProduto = await _context.Produto.SingleOrDefaultAsync(m => m.Id == produto.Id);
            var carrinho = await _context.CarrinhoCompra.Where(cart => cart.fk_ProdutoID == findProduto.Id).ToListAsync();

            if (carrinho.Any())
            {
                TempData["Message"] = "Delete seus pedidos associados ao usuário antes de deletar a sua conta!";
                return RedirectToAction("Index", "HomePage");
            };

            _context.Produto.Remove(findProduto);

            await _context.SaveChangesAsync();

            TempData["Message"] = $"O produto {findProduto.Nome} foi removido!";

            return RedirectToAction(nameof(Index));
        }
    }
}
