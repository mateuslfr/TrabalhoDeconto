using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho.Data;
using Trabalho.Models;

namespace Trabalho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly AppDataContext _context;

        public TarefaController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Tarefa
        [HttpGet]
        public ActionResult<IEnumerable<Tarefa>> GetTarefas()
        {
            return _context.Tarefas.ToList();
        }

        // GET: api/Tarefa/5
        [HttpGet("{id}")]
        public ActionResult<Tarefa> GetTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;
        }

        // GET: api/Tarefa/ByPrazo/{prazo}
        [HttpGet("ByPrazo/{prazo}")]
            public ActionResult<IEnumerable<Tarefa>> GetTarefasByPrazo(DateTime prazo)
        {
            var tarefas = _context.Tarefas.Where(t => t.Prazo.Date == prazo.Date).ToList();

            if (tarefas.Count == 0)
            {
                 return NotFound();
            }

    return tarefas;
}


        // POST: api/Tarefa
      [HttpPost]
[Route("cadastrar")]
public IActionResult Cadastrar([FromBody] Tarefa tarefa)
{
    try
    {
        Categoria categoria = _context.Categorias.Find(tarefa.CategoriaId);
        if (categoria == null)
        {
            return NotFound("Categoria não encontrada");
        }
        tarefa.Categoria = categoria;

        _context.Tarefas.Add(tarefa);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}


        // PUT: api/Tarefa/5
        [HttpPut("{id}")]
        public IActionResult UpdateTarefa(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarefa).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(id))
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

        // DELETE: api/Tarefa/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return NoContent();
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefas.Any(t => t.Id == id);
        }
    }
}
