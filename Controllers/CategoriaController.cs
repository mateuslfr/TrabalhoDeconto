using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho.Data;
using Trabalho.Models;

namespace Trabalho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDataContext _context;

        public CategoriaController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Categoria
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategorias()
        {
            return _context.Categorias.ToList();
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public ActionResult<Categoria> GetCategoria(int id)
        {
            var categoria = _context.Categorias.Find(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // POST: api/Categoria
        [HttpPost]
        public ActionResult<Categoria> CreateCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
        }

        // PUT: api/Categoria/5
        [HttpPut("{id}")]
        public IActionResult UpdateCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategoria(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(c => c.Id == id);
        }
    }
}
