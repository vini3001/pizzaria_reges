using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizzaria_reges.Data;
using pizzaria_reges.Models;
using System.Runtime.CompilerServices;

namespace pizzaria_reges.Controllers
{
    public class CadCarrinhoController : Controller
    {
        private readonly IESContext _context;

        public CadCarrinhoController(IESContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<Produto> findProductById(long id)
        {
            var produto= await _context.Produto.SingleOrDefaultAsync(m => m.Id== id);

            return produto;
        }

        public async Task<IActionResult> Create(long clienteId, long produtoId)
        {

            try
            {
                var findPedido = await _context.Pedido.OrderBy(seq => seq.Id).LastAsync(cart => cart.fk_ClienteID == clienteId);
                var findProduto = await _context.Produto.SingleOrDefaultAsync(cart => cart.Id == produtoId);

                CarrinhoCompra carrinhoComp = new CarrinhoCompra
                {
                    fk_PedidoID = findPedido.Id,
                    fk_ProdutoID = findProduto.Id
                };
                if (ModelState.IsValid)
                {
                    _context.Add(carrinhoComp);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "HomePage", new { Id = clienteId});
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Erro", "Não foi possível inserir os dados");
            }

            return View();
        }
    }
}
