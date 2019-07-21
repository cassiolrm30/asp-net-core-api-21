using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteApi.Models;

namespace TesteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly Contexto _contexto;

        public LivrosController(Contexto contexto)
        {
            _contexto = contexto;

            if (_contexto.Livros.Count() == 0)
            {
                // Create a new TodoItem if collection is empty, which means you can't delete all TodoItems.
                _contexto.Livros.Add(new Livro { Titulo = "Livro I", Autor = "Cássio" });
                _contexto.SaveChanges();
            }
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetAll()
        {
            return await _contexto.Livros.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> Get(int id)
        {
            var livro = await _contexto.Livros.FindAsync(id);
            if (livro == null)
                return NotFound();
            return livro;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<Livro>> Post(Livro livro)
        {
            _contexto.Livros.Add(livro);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = livro.Id }, livro);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Livro item)
        {
            if (id != item.Id)
                return BadRequest();

            _contexto.Entry(item).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var todoItem = await _contexto.Livros.FindAsync(id);
            if (todoItem == null)
                return NotFound();

            _contexto.Livros.Remove(todoItem);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}