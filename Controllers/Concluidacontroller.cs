using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho.Data;
using Trabalho.Models;

namespace Trabalho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcluidaController : ControllerBase
    {
        private readonly AppDataContext _context;

        public ConcluidaController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Concluida
        [HttpGet]
        public ActionResult<IEnumerable<Concluida>> GetConcluidas()
        {
            return _context.Concluidas.Include(c => c.Tarefa).ToList();
        }

        // GET: api/Concluida/5
        [HttpGet("{id}")]
        public ActionResult<Concluida> GetConcluida(int id)
        {
            var concluida = _context.Concluidas.Include(c => c.Tarefa).FirstOrDefault(c => c.Id == id);

            if (concluida == null)
            {
                return NotFound();
            }

            return concluida;
        }

        // POST: api/Concluida
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Concluida concluida)
        {
            try
            {
                Tarefa tarefa = _context.Tarefas.Find(concluida.TarefaId);
                if (tarefa == null)
                {
                    return NotFound("Tarefa não encontrada");
                }

                // Verifique se a tarefa já está na tabela EmAndamento
                var tarefaEmAndamento = _context.EmAndamentos.FirstOrDefault(e => e.TarefaId == concluida.TarefaId);
                if (tarefaEmAndamento != null)
                {
                    // Remova a tarefa da tabela EmAndamento
                    _context.EmAndamentos.Remove(tarefaEmAndamento);
                }

                concluida.Tarefa = tarefa;
                _context.Concluidas.Add(concluida);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetConcluida), new { id = concluida.Id }, concluida);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Concluida/5
        [HttpPut("{id}")]
        public IActionResult UpdateConcluida(int id, Concluida concluida)
        {
            if (id != concluida.Id)
            {
                return BadRequest();
            }

            _context.Entry(concluida).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConcluidaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Concluida/5
        [HttpDelete("{id}")]
        public IActionResult DeleteConcluida(int id)
        {
            var concluida = _context.Concluidas.Find(id);
            if (concluida == null)
            {
                return NotFound();
            }

            _context.Concluidas.Remove(concluida);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ConcluidaExists(int id)
        {
            return _context.Concluidas.Any(c => c.Id == id);
        }
    }
}
