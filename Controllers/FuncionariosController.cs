﻿using System;
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
    public class FuncionariosController : Controller
    {
        private readonly Context _context;

        public FuncionariosController(Context context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            var context = _context.Funcionarios.Include(f => f.Cargo).Include(f => f.Setor).Include(f => f.Turno);
            return View(await context.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .Include(f => f.Cargo)
                .Include(f => f.Setor)
                .Include(f => f.Turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            ViewData["CargoId"] = new SelectList(_context.Cargos, "Id", "NomeCargo");
            ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "NomeSetor");
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "NomeTurno");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeFuncionario,PIS,Telefone,Salario,CargoId,SetorId,TurnoId")] Funcionario funcionario)
        {
            try
            {

                _context.Add(funcionario);
                await _context.SaveChangesAsync();

                ViewData["CargoId"] = new SelectList(_context.Cargos, "Id", "NomeCargo", funcionario.CargoId);
                ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "NomeSetor", funcionario.SetorId);
                ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "NomeTurno", funcionario.TurnoId);

                return RedirectToAction(nameof(Index));


            }
            catch (Exception)
            {
                return View(funcionario);
                throw;
            }
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            ViewData["CargoId"] = new SelectList(_context.Cargos, "Id", "NomeCargo", funcionario.CargoId);
            ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "NomeSetor", funcionario.SetorId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "NomeTurno", funcionario.TurnoId);
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFuncionario,PIS,Telefone,Salario,CargoId,SetorId,TurnoId")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(funcionario);
                await _context.SaveChangesAsync();

                ViewData["CargoId"] = new SelectList(_context.Cargos, "Id", "NomeCargo", funcionario.CargoId);
                ViewData["SetorId"] = new SelectList(_context.Setores, "Id", "NomeSetor", funcionario.SetorId);
                ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "NomeTurno", funcionario.TurnoId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(funcionario.Id))
                {
                    return NotFound();
                }
                else
                {
                    return View(funcionario);
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .Include(f => f.Cargo)
                .Include(f => f.Setor)
                .Include(f => f.Turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }
    }
}
