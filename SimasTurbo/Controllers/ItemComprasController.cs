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
    //[Authorize(Roles = "Admin,Operador")]
    public class ItemComprasController : Controller
    {
        private readonly SimasContext _context;

        public ItemComprasController(SimasContext context)
        {
            _context = context;
        }

        // GET: ItemCompras
        public async Task<IActionResult> Index(string? id)
        {
            if (id != null)
            {
                ViewData["ItemCompraId"] = id;
            }
            //var caquinha = _context.ItemVenda.Where(v => v.CadVendasId.ToString().Equals(id)).Include(i => i.CadVendas).Include(i => i.Produto);
            var caquinha = _context.ItemCompras.Where(v => v.CadComprasId.ToString().Equals(id)).Include(i => i.CadCompras).Include(i => i.Produto);
            return View(await caquinha.ToListAsync());
        }

        // GET: ItemCompras/Create
        public IActionResult Create(string? id)
        {
            if (id != null)
            {
                ViewData["ItemCompraId"] = id;
            }
            ViewData["CadComprasId"] = new SelectList(_context.CadCompras, "CadComprasId", "CadComprasId");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome");
            return View();
        }

        // GET: ItemCompras/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ItemCompras == null)
            {
                return NotFound();
            }

            var itemCompra = await _context.ItemCompras
                .Include(i => i.CadCompras)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ItemCompraId == id);
            if (itemCompra == null)
            {
                return NotFound();
            }

            return View(itemCompra);
        }


        // POST: ItemCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemCompraId,ProdutoId,PrecoUnit,Quantidade,CadComprasId")] ItemCompra itemCompra)
        {
            if (ModelState.IsValid)
            {
                itemCompra.ItemCompraId = Guid.NewGuid();
                _context.Add(itemCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CadComprasId"] = new SelectList(_context.CadCompras, "CadComprasId", "CadComprasId", itemCompra.CadComprasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", itemCompra.ProdutoId);
            return View(itemCompra);
        }

        // GET: ItemCompras/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ItemCompras == null)
            {
                return NotFound();
            }

            var itemCompra = await _context.ItemCompras.FindAsync(id);
            if (itemCompra == null)
            {
                return NotFound();
            }
            ViewData["CadComprasId"] = new SelectList(_context.CadCompras, "CadComprasId", "CadComprasId", itemCompra.CadComprasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", itemCompra.ProdutoId);
            return View(itemCompra);
        }

        // POST: ItemCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemCompraId,ProdutoId,PrecoUnit,Quantidade,CadComprasId")] ItemCompra itemCompra)
        {
            if (id != itemCompra.ItemCompraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemCompraExists(itemCompra.ItemCompraId))
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
            ViewData["CadComprasId"] = new SelectList(_context.CadCompras, "CadComprasId", "CadComprasId", itemCompra.CadComprasId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", itemCompra.ProdutoId);
            return View(itemCompra);
        }

        // GET: ItemCompras/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ItemCompras == null)
            {
                return NotFound();
            }

            var itemCompra = await _context.ItemCompras
                .Include(i => i.CadCompras)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ItemCompraId == id);
            if (itemCompra == null)
            {
                return NotFound();
            }

            return View(itemCompra);
        }

        // POST: ItemCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ItemCompras == null)
            {
                return Problem("Entity set 'SimasContext.ItemCompras'  is null.");
            }
            var itemCompra = await _context.ItemCompras.FindAsync(id);
            if (itemCompra != null)
            {
                _context.ItemCompras.Remove(itemCompra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemCompraExists(Guid id)
        {
          return (_context.ItemCompras?.Any(e => e.ItemCompraId == id)).GetValueOrDefault();
        }
    }
}
