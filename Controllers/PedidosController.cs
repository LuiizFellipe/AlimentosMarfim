using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlimentosMarfim.AppDbContext;
using AlimentosMarfim.Models;
using System.IO;
using PdfSharpCore.Drawing;

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


        public FileResult GerarRelatorio()
        {
            using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            {
                // <== CONFIGURAÇÕES DO PDF:
                var page = doc.AddPage();
                page.Size = PdfSharpCore.PageSize.A4;
                page.Orientation = PdfSharpCore.PageOrientation.Portrait;
                var graphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);
                var corFonte = PdfSharpCore.Drawing.XBrushes.Black;

                var textFormatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                var fonteOrganizacao = new PdfSharpCore.Drawing.XFont("Arial", 10);
                var fonteDescricao = new PdfSharpCore.Drawing.XFont("Arial", 8, PdfSharpCore.Drawing.XFontStyle.BoldItalic);
                var titulodetalhes = new PdfSharpCore.Drawing.XFont("Arial", 14, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDetalhesDescricao = new PdfSharpCore.Drawing.XFont("Arial", 7);
                // <== CONFIGURAÇÕES ATÉ AQUI.



                // <== DADOS DO PDF:

                //Logo:
                 var logo = @"C:\Users\andre\Pictures\logo.png";

                 XImage image = XImage.FromFile(logo);
                 graphics.DrawImage(image, 20 , 5 , 80 , 80);


                //Título:
                var tituloDetalhes = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                tituloDetalhes.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                tituloDetalhes.DrawString("Marfim Pedidos", titulodetalhes, corFonte, new PdfSharpCore.Drawing.XRect(0, 40, page.Width, page.Height));

                //Colunas:
                var alturaTituloDetalhesY = 140;
                var detalhes = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);

                detalhes.DrawString("Data da Compra", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(80, alturaTituloDetalhesY, page.Width, page.Height));
                detalhes.DrawString("Cliente", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(220, alturaTituloDetalhesY, page.Width, page.Height));
                detalhes.DrawString("Vendedor", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(300, alturaTituloDetalhesY, page.Width, page.Height));
                detalhes.DrawString("Produto Vendido", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(380, alturaTituloDetalhesY, page.Width, page.Height));

                // <== PRINCIPAL

                var context = _context.Pedidos.Include(p => p.Cliente).Include(p => p.Funcionario).Include(p => p.Produtos);
                var pedidos = _context.Pedidos.ToArray();

                // LIGAÇÃO COM O BANCO DE DADOS
                var q = _context.Pedidos.AsQueryable();
             

                //SELECIONADO PRODUTOS
                var data = q.Select(p => p.DataCompra).ToArray();
                var cliente = q.Select(p => p.Cliente.NomeCliente).ToArray();
                var vendedor = q.Select(p => p.Funcionario.NomeFuncionario).ToArray();
                var prod = q.Select(p => p.Produtos.NomeProduto).ToArray();

                

                var alturaDetalhesItens = 160;
                for (int i = 0; i < q.Count(); i++)
                {
                    //IMPLEMENTAR <== DADOS DO BANCO DE DADOS
                    //exemplo:
                    textFormatter.DrawString($"{data[i]}", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(80, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString($"{cliente[i]}", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(220, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString($"{vendedor[i]}", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(300, alturaDetalhesItens, page.Width, page.Height));
                    textFormatter.DrawString($"{prod[i]}", fonteDetalhesDescricao, corFonte, new PdfSharpCore.Drawing.XRect(380, alturaDetalhesItens, page.Width, page.Height));


                    alturaDetalhesItens += 20;
                }
                //PRINCIPLA ATÉ AQUI.


                //Número da Página
                var qtdPaginas = doc.PageCount;
                textFormatter.DrawString(qtdPaginas.ToString(), new PdfSharpCore.Drawing.XFont("Arial", 10), corFonte, new PdfSharpCore.Drawing.XRect(575, 825, page.Width, page.Height));

                // <== DADOS DO PDF ATÉ AQUI.




                using (MemoryStream stream = new MemoryStream())
                {
                    var contantType = "application/pdf";
                    doc.Save(stream, false);

                    var nomeArquivo = "RelatorioMafim.pdf";

                    return File(stream.ToArray(), contantType, nomeArquivo);
                }
            }
        }


    }
}
