using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimasTurbo.Data;
using SimasTurbo.Models;

namespace SimasTurbo.Controllers
{
    [Authorize(Roles = "Admin,Operador")]
    public class ItemVendasController : Controller
    {
        private readonly SimasContext _context;

        public ItemVendasController(SimasContext context)
        {
            _context = context;
        }

        // GET: ItemVendas
        public async Task<IActionResult> Index(string? id)
        {
            if (id != null)
            {
                ViewData["ItemVendaId"] = id;
            }
            //var caquinha = _context.ItemVenda.Where(v => v.CadVendasId.ToString().Equals(id)).Include(i => i.CadVendas).Include(i => i.Produto);
            // brigado sérgio
            var dataModel = _context.ItemVenda.Where(v => v.CadVendasId.ToString().Equals(id)).Include(i => i.CadVendas).Include(i => i.Produto);
            return View(await dataModel.ToListAsync());
        }

        // GET: ItemVendas/Create
        public IActionResult Create(string? id)
        {
            if (id != null)
            {
                ViewData["ItemVendaId"] = id;
            }
            ViewData["CadVendasId"] = new SelectList(_context.CadVendas, "CadVendasId", "CadVendasId");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome");
            return View();
        }

        // GET: ItemVendas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ItemVenda == null)
            {
                return NotFound();
            }

            var itemVenda = await _context.ItemVenda
                .Include(i => i.CadVendas)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ItemVendaId == id);
            if (itemVenda == null)
            {
                return NotFound();
            }

            return View(itemVenda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemVendaId,ProdutoId,PrecoUnit,Quantidade,CadVendasId")] ItemVenda itemVenda)
        {
            if (ModelState.IsValid)
            {
                itemVenda.ItemVendaId = Guid.NewGuid();
                _context.Add(itemVenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CadVendasId"] = new SelectList(_context.CadVendas, "CadVendasId", "CadVendasId", itemVenda.CadVendasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", itemVenda.ProdutoId);
            return View(itemVenda);
        }



        // GET: ItemVendas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ItemVenda == null)
            {
                return NotFound();
            }

            var itemVenda = await _context.ItemVenda.FindAsync(id);
            if (itemVenda == null)
            {
                return NotFound();
            }
            ViewData["CadVendasId"] = new SelectList(_context.CadVendas, "CadVendasId", "CadVendasId", itemVenda.CadVendasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", itemVenda.ProdutoId);
            return View(itemVenda);
        }

        // POST: ItemVendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemVendaId,ProdutoId,PrecoUnit,Quantidade,CadVendasId")] ItemVenda itemVenda)
        {
            if (id != itemVenda.ItemVendaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemVenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemVendaExists(itemVenda.ItemVendaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CadVendasId"] = new SelectList(_context.CadVendas, "CadVendasId", "CadVendasId", itemVenda.CadVendasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", itemVenda.ProdutoId);
            return View(itemVenda);
        }

        // GET: ItemVendas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ItemVenda == null)
            {
                return NotFound();
            }

            var itemVenda = await _context.ItemVenda
                .Include(i => i.CadVendas)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ItemVendaId == id);
            if (itemVenda == null)
            {
                return NotFound();
            }

            return View(itemVenda);
        }

        // POST: ItemVendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ItemVenda == null)
            {
                return Problem("Entity set 'SimasContext.ItemVenda'  is null.");
            }
            var itemVenda = await _context.ItemVenda.FindAsync(id);
            if (itemVenda != null)
            {
                _context.ItemVenda.Remove(itemVenda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemVendaExists(Guid id)
        {
            return (_context.ItemVenda?.Any(e => e.ItemVendaId == id)).GetValueOrDefault();
        }
    }
}
