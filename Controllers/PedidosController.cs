using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlimentosMarfim.AppDbContext;
using AlimentosMarfim.Models;

namespace AlimentosMarfim.Controllers
{
    public class PedidosController : Controller
    {
        private readonly Context _context;

        public PedidosController(Context context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var context = _context.Pedidos.Include(p => p.Cliente).Include(p => p.Funcionario).Include(p => p.Produtos);
            return View(await context.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Funcionario)
                .Include(p => p.Produtos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NomeCliente");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "NomeFuncionario");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "NomeProduto");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataCompra,ClienteId,FuncionarioId,ProdutoId")] Pedido pedido)
        {
            try
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NomeCliente", pedido.ClienteId);
                ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "NomeFuncionario", pedido.FuncionarioId);
                ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "NomeProduto", pedido.ProdutoId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(pedido);
                throw;
            }
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NomeCliente", pedido.ClienteId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "NomeFuncionario", pedido.FuncionarioId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "NomeProduto", pedido.ProdutoId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataCompra,ClienteId,FuncionarioId,ProdutoId")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(pedido);
                await _context.SaveChangesAsync();

                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NomeCliente", pedido.ClienteId);
                ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "NomeFuncionario", pedido.FuncionarioId);
                ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "NomeProduto", pedido.ProdutoId);

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(pedido.Id))
                {
                    return NotFound();
                }
                else
                {

                    return View(pedido);
                    throw;
                }
            }
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Funcionario)
                .Include(p => p.Produtos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
