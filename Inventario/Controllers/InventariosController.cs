using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Models;

namespace Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InventariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Inventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventarios>>> GetInventarios()
        {
            return await _context.Inventarios.ToListAsync();
        }

        // GET: api/Inventarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventarios>> GetInventarios(int id)
        {
            var inventarios = await _context.Inventarios.FindAsync(id);

            if (inventarios == null)
            {
                return NotFound();
            }

            return inventarios;
        }

        // PUT: api/Inventarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventarios(int id, Inventarios inventarios)
        {
            if (id != inventarios.IdInventario)
            {
                return BadRequest();
            }

            _context.Entry(inventarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventariosExists(id))
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

        // POST: api/Inventarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Inventarios>> PostInventarios(Inventarios inventarios)
        {
            _context.Inventarios.Add(inventarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventarios", new { id = inventarios.IdInventario }, inventarios);
        }

        // DELETE: api/Inventarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Inventarios>> DeleteInventarios(int id)
        {
            var inventarios = await _context.Inventarios.FindAsync(id);
            if (inventarios == null)
            {
                return NotFound();
            }

            _context.Inventarios.Remove(inventarios);
            await _context.SaveChangesAsync();

            return inventarios;
        }

        private bool InventariosExists(int id)
        {
            return _context.Inventarios.Any(e => e.IdInventario == id);
        }
    }
}
