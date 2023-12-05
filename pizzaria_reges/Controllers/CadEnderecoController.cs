using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using pizzaria_reges.Data;
using pizzaria_reges.Models;

namespace pizzaria_reges.Controllers
{
    public class CadEnderecoController : Controller
    {
        private readonly IESContext _context;

        public CadEnderecoController(IESContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(long? Id)
        {
            return RedirectToAction("Create", "CadCliente", new { Id = Id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Endereco_cli endereco)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(endereco);
                    await _context.SaveChangesAsync();
                    ViewData["enderecoId"] = endereco.Id;
                    return Create(endereco.Id); 
                        //View("Views/CadCliente/Create.cshtml");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Erro", "Não foi possível inserir os dados");
            }
            return View("Views/CadCliente/Create.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Endereco_cli = await _context.Endereco_cli.SingleOrDefaultAsync(m => m.Id == id);

            if (Endereco_cli == null)
            {
                return NotFound();
            }

            return View(Endereco_cli);
        }

        public bool Endereco_cliExists(long? id)
        {
            return _context.Endereco_cli.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, Endereco_cli endereco_cli)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(endereco_cli);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!Endereco_cliExists(endereco_cli.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(endereco_cli);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco_cli = await _context.Endereco_cli.SingleOrDefaultAsync(m => m.Id == id);

            if (endereco_cli == null)
            {
                return NotFound();
            }

            return View(endereco_cli);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco_cli = await _context.Endereco_cli.SingleOrDefaultAsync(m => m.Id == id);

            if (endereco_cli == null)
            {
                return NotFound();
            }

            return View(endereco_cli);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var endereco_cli = await _context.Endereco_cli.SingleOrDefaultAsync(m => m.Id == id);
            _context.Endereco_cli.Remove(endereco_cli);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"O endereço do cliente {endereco_cli.Rua.ToUpper()} foi removido!";

            return RedirectToAction(nameof(Index));
        }
    }
}
