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
    public class EmAndamentoController : ControllerBase
    {
        private readonly AppDataContext _context;

        public EmAndamentoController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/EmAndamento
        [HttpGet]
        public ActionResult<IEnumerable<EmAndamento>> GetEmAndamentos()
        {
            return _context.EmAndamentos.Include(e => e.Tarefa).ToList();
        }

        // GET: api/EmAndamento/5
        [HttpGet("{id}")]
        public ActionResult<EmAndamento> GetEmAndamento(int id)
        {
            var emAndamento = _context.EmAndamentos.Include(e => e.Tarefa).FirstOrDefault(e => e.Id == id);

            if (emAndamento == null)
            {
                return NotFound();
            }

            return emAndamento;
        }

        // POST: api/EmAndamento
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] EmAndamento emAndamento)
        {
            try
            {
                Tarefa tarefa = _context.Tarefas.Find(emAndamento.TarefaId);
                if (tarefa == null)
                {
                    return NotFound("Tarefa não encontrada");
                }

                // Verifique se a tarefa já está na tabela Concluida
                var tarefaConcluida = _context.Concluidas.FirstOrDefault(c => c.TarefaId == emAndamento.TarefaId);
                if (tarefaConcluida != null)
                {
                    // Remova a tarefa da tabela Concluida
                    _context.Concluidas.Remove(tarefaConcluida);
                }

                emAndamento.Tarefa = tarefa;
                _context.EmAndamentos.Add(emAndamento);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetEmAndamento), new { id = emAndamento.Id }, emAndamento);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/EmAndamento/5
        [HttpPut("{id}")]
        public IActionResult UpdateEmAndamento(int id, EmAndamento emAndamento)
        {
            if (id != emAndamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(emAndamento).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmAndamentoExists(id))
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

        // DELETE: api/EmAndamento/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmAndamento(int id)
        {
            var emAndamento = _context.EmAndamentos.Find(id);
            if (emAndamento == null)
            {
                return NotFound();
            }

            _context.EmAndamentos.Remove(emAndamento);
            _context.SaveChanges();

            return NoContent();
        }

        private bool EmAndamentoExists(int id)
        {
            return _context.EmAndamentos.Any(e => e.Id == id);
        }
    }
}
