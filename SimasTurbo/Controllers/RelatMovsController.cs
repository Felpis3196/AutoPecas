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
    //[Authorize(Roles = "Admin,Gerente")]
    public class RelatMovsController : Controller
    {
        private readonly SimasContext _context;

        public RelatMovsController(SimasContext context)
        {
            _context = context;
        }

        // GET: RelatMovs
        public async Task<IActionResult> Index(string? inBusca, string? inMovimento, DateTime? dataInicial, DateTime? dataFinal)
        {
            List<RelatMov> listaItens = new List<RelatMov>();

            //LISTA PARA AS COMPRAS
            var vendas = _context.CadVendas;
            foreach (var v in vendas)
            {
                RelatMov relatMov = new RelatMov
                {
                    RelatMovId = v.CadVendasId,
                    NotaFiscal = v.NotaVenda,
                    DataEHora = v.DataHora,
                    TipoMovimentacao = "Saida",
                };
                listaItens.Add(relatMov);
            }
            var itemVendas = _context.ItemVendas.Include(iv => iv.Produto);
            foreach (var v in await itemVendas.ToListAsync())
            {
                var relatMov = listaItens.FirstOrDefault(item => item.RelatMovId == v.CadVendasId);

                if (relatMov != null)
                {
                    relatMov.Produto = v.Produto.Nome;
                    relatMov.Quantidade = v.Quantidade;
                    relatMov.PrecoUnitario = v.PrecoUnit;
                }
            }
            ///////////////////////////////////////*/

            //LISTA PARA AS COMPRAS
            var compras = _context.CadCompras;
            foreach (var v in compras)
            {
                RelatMov relatMov = new RelatMov
                {
                    RelatMovId = v.CadComprasId,
                    NotaFiscal = v.NotaCompra,
                    DataEHora = v.DataHora,
                    TipoMovimentacao = "Entrada",
                };
                listaItens.Add(relatMov);
            }
            var itemCompras = _context.ItemCompras.Include(ic=>ic.Produto);
            foreach (var v in await itemCompras.ToListAsync())
            {
                var relatMov = listaItens.FirstOrDefault(item => item.RelatMovId == v.CadComprasId);

                if (relatMov != null)
                {
                    relatMov.Produto = v.Produto.Nome;
                    relatMov.Quantidade = v.Quantidade;
                    relatMov.PrecoUnitario = v.PrecoUnit;
                }
            }
            ///////////////////////////////////////*/

            if (inBusca != null)
            {
                listaItens = listaItens.Where(i => i.Produto.ToString().ToUpper().Contains(inBusca.ToUpper())).ToList();
            }

            if(inMovimento != null)
            {
                listaItens = listaItens.Where(i => i.TipoMovimentacao == inMovimento).ToList();
            }

            if(dataInicial  != null && dataFinal != null) 
            {
                listaItens = listaItens.Where(i => i.DataEHora >= dataInicial && i.DataEHora <= dataFinal).ToList();
            }

            return View(listaItens);
        }


        private bool RelatMovExists(Guid id)
        {
            return (_context.RelatMov?.Any(e => e.RelatMovId == id)).GetValueOrDefault();
        }
    }
}
