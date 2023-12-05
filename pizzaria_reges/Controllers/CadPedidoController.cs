using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Experimental.ProjectCache;
using Microsoft.EntityFrameworkCore;
using pizzaria_reges.Data;
using pizzaria_reges.Models;
using System.Runtime.CompilerServices;

namespace pizzaria_reges.Controllers

{
    public class CadPedidoController : Controller
    {
        private readonly IESContext _context;

        public CadPedidoController(IESContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index(long? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Usuário não autenticado!";
                return RedirectToAction("Index", "HomePage");
            }

            var pedidos = await _context.Pedido.Where(c => c.fk_ClienteID == id).ToListAsync();

            if (pedidos == null) 
            {
                TempData["Message"] = "Sem pedidos para esse usuário!";
                return RedirectToAction("Index", "HomePage", new { id });
            }

            ViewBag.clienteId = id;
            return View(pedidos);
        }

        public async Task<IActionResult> Create(long Id, long produtoId)
        {
            if (Id == 0)
            {
                TempData["Message"] = "Usuário não autenticado!";
                return RedirectToAction("Index", "HomePage");
            }

            try
            {
                var findProduto = await _context.Produto.SingleOrDefaultAsync(pro => pro.Id == produtoId);
                System.Diagnostics.Debug.WriteLine(produtoId);

                if (findProduto == null)
                {
                    TempData["Message"] = "Produto não encontrado!";
                    return RedirectToAction("Index", "HomePage", new {Id = Id});
                }

                Pedido pedido = new Pedido
                {
                    fk_ClienteID = Id
                };

                ViewBag.pizza = findProduto;
                return View(pedido);
            }
            catch(DbUpdateException) {
                ModelState.AddModelError("Erro", "Não foi possível inserir os dados");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long? produtoId, Pedido pedido)
        {
            var findProduto = await _context.Produto.SingleOrDefaultAsync(cart => cart.Id == produtoId);

            Random random = new Random();
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime dataHoraLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);

            try
            {
                if (ModelState.IsValid)
                {
                    var valorTotal = pedido.Quantidade * findProduto.Preco;

                    pedido.PedidoData = dataHoraLocal;
                    pedido.ValorTotal = valorTotal;
                    pedido.Numero = random.Next();
                    pedido.PedidoData = dataHoraLocal;

                    Pedido pedidoInstance = new Pedido
                    {
                        Numero = pedido.Numero,
                        ValorTotal = pedido.ValorTotal,
                        PedidoData = pedido.PedidoData,
                        Quantidade = pedido.Quantidade,
                        fk_ClienteID = pedido.fk_ClienteID
                    };

                    _context.Add(pedidoInstance);
                    await _context.SaveChangesAsync();

                    var objectParams = new
                    {
                        clienteId = pedido.fk_ClienteID,
                        produtoId
                    };

                    return RedirectToAction("Create", "CadCarrinho", objectParams);
                }
                else
                {
                    return BadRequest("Modelo inválido!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar alterações: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Exceção interna: {ex.InnerException.Message}");
                }
            }

            return RedirectToAction("Index", "HomePage");
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.SingleOrDefaultAsync(m => m.Id == id);
            var cliente = await _context.Cliente.SingleOrDefaultAsync(m => m.Id == pedido.fk_ClienteID);

            if (pedido == null)
            {
                return NotFound();
            }

            ViewModel viewModel = new ViewModel
            {
                pedido = pedido,
                cliente = cliente
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.SingleOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            var findCarrinho = await _context.CarrinhoCompra.SingleOrDefaultAsync(cart => cart.fk_PedidoID == id);

            var findProduto = await _context.Produto.SingleOrDefaultAsync(pro => pro.Id == findCarrinho.fk_ProdutoID);

            ViewBag.produto = findProduto;
            ViewBag.clientes = new SelectList(_context.Cliente.OrderBy(b => b.Nome), "Id", "Nome",
                pedido.fk_ClienteID);

            return View(pedido);
        }

        public bool PedidoExists(long? id)
        {
            return _context.Pedido.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!PedidoExists(pedido.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.SingleOrDefaultAsync(m => m.Id == id);
            var cliente = await _context.Cliente.SingleOrDefaultAsync(m => m.Id == pedido.fk_ClienteID);

            if (pedido == null)
            {
                return NotFound();
            }

            ViewModel viewModel = new ViewModel
            {
                pedido = pedido,
                cliente = cliente
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var carrinho = await _context.CarrinhoCompra.SingleOrDefaultAsync(cart => cart.fk_PedidoID == id);

            if (carrinho != null) 
            {
                _context.Remove(carrinho);
            }

            var pedido = await _context.Pedido.SingleOrDefaultAsync(m => m.Id == id);
            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Pedido de número {pedido.Id} foi removido!";

            return RedirectToAction(nameof(Index));
        }
    }
}
