using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizzaria_reges.Data;
using pizzaria_reges.Models;

namespace pizzaria_reges.Controllers
{
    public class CadClienteController : Controller
    {
        private readonly IESContext _context;

        public CadClienteController(IESContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View("Views/CadCliente/Create.cshtml");
        }

        public IActionResult showPerfil()
        {

            return View();
        }

        public async Task<IActionResult> Create(long? Id)
        {
            Cliente cliente = new Cliente
            {
                fk_EnderecoID = Id,
            };

            ViewModel viewModel = new ViewModel
            {
                cliente = cliente,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();

                    return RedirectToIndex(cliente.Id);
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Erro", "Não foi possível inserir os dados");
            }
            return View(cliente);
        }

        public IActionResult RedirectToIndex(long? Id)
        {
            return RedirectToAction("Index", "HomePage", new { id = Id });
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.SingleOrDefaultAsync(m => m.Id == id);
            var endereco = await _context.Endereco_cli.SingleOrDefaultAsync(end => end.Id == cliente.fk_EnderecoID);

            if (cliente == null)
            {
                return NotFound();
            }

            ViewModel viewModel = new ViewModel
            {
                cliente = cliente,
                endereco = endereco
            };

            return View(viewModel);
        }

        public bool ClienteExists(long? id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction("Index", "HomePage");
            }
            return BadRequest("Não foi possível executar a operação");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUsuario(Cliente cliente) 
        {
            if (cliente != null)
            {
                try
                {
                    var clienteLogin = await _context.Cliente.SingleOrDefaultAsync(m => m.email == cliente.email && m.Senha == cliente.Senha);

                    if (clienteLogin == null)
                    {
                        RedirectToAction("Index", "HomePage");
                    }

                    return RedirectToAction("Index", "HomePage", new { Id = clienteLogin.Id });
                }
                catch (DbUpdateException) 
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }

                    return RedirectToAction("Index", "HomePage");
                }
            }
            return RedirectToAction("Index", "HomePage");
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Usuário não autenticado!";
                return RedirectToAction("Index", "HomePage");
            }

            var cliente = await _context.Cliente.SingleOrDefaultAsync(m => m.Id == id);
            var endereco = await _context.Endereco_cli.SingleOrDefaultAsync(end => end.Id == cliente.fk_EnderecoID);

            if (cliente == null)
            {
                return NotFound();
            }

            ViewModel viewModel = new ViewModel
            {
                cliente = cliente,
                endereco = endereco
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? Id)
        {
            var cliente = await _context.Cliente.SingleOrDefaultAsync(m => m.Id == Id);
            var endereco = await _context.Endereco_cli.SingleOrDefaultAsync(end => end.Id == cliente.fk_EnderecoID);
            var pedidos = _context.Pedido.Where(ped => ped.fk_ClienteID == cliente.Id).ToList();

            if (pedidos != null)
            {
                TempData["Message"] = "Apague seus pedidos associados ao usuário antes de deletar a sua conta!";
                return RedirectToAction("Index", "CadPedido", new { Id });
            };
            
            _context.Cliente.Remove(cliente);
            _context.Endereco_cli.Remove(endereco);

            await _context.SaveChangesAsync();

            TempData["Message"] = $"CLiente {cliente.Nome.ToUpper()} foi removido!";

            return RedirectToAction("Index", "HomePage");
        }
    }
}
