using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimasTurbo.Data;
using SimasTurbo.Models;

namespace SimasTurbo.Controllers
{
    [Authorize(Roles = "Admin,Gerente")]
    public class RelatProdController : Controller
    {
        private readonly SimasContext _context;

        public RelatProdController(SimasContext context)
        {
            _context = context;
        }

        // GET: RelatProd
        public async Task<IActionResult> Index(string inBusca, Guid? inCategoria)
        {
            List<RelatProd> listItens = new List<RelatProd>();
            var produtos = _context.Produtos.Include(p => p.Categoria);
            foreach (var p in await produtos.ToListAsync())
            {
                RelatProd relatProd = new RelatProd()
                {
                    RelatProdId = p.ProdutoId,
                    Nome = p.Nome,
                    Categoria = p.Categoria.Nome != null ? p.Categoria.Nome : "Categoria não definida",
                    CategoriaId = p.CategoriaId,
                    Quantidade = p.Quantidade,
                    PrecoUnit = p.PreçoUnit
                };
                listItens.Add(relatProd);
            }

            if (inBusca!=null)
            {
                listItens = listItens.Where(i => i.Nome.Contains(inBusca)).ToList();
            }
            //ViewBag.produtoCategoria = _context.Produtos.Include(p => p.Categoria).ToList();
            ViewBag.produtoCategoria = _context.Categorias.ToList();
            if (inCategoria != null)
            {
                listItens = listItens.Where(i => i.CategoriaId == inCategoria).ToList();
            }
            return View("Index", listItens);
        }   






        private bool RelatProdExists(Guid id)
        {
            return (_context.RelatProd?.Any(e => e.RelatProdId == id)).GetValueOrDefault();
        }
    }
}
