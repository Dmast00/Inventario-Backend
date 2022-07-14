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
        WriteTxt write = new WriteTxt();

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
        [HttpPut]
        public async Task<ActionResult<Inventarios>> PostInventarios(Inventarios inventarios)
        {
            if (inventarios != null)
            {
                var productId = await _context.Inventarios.FirstOrDefaultAsync(x => x.FK_IdProducto == inventarios.FK_IdProducto);
                if(productId != null)
                {
                    productId.P_Existencia += inventarios.P_Existencia;
                    productId.P_InventarioMinimo = inventarios.P_InventarioMinimo;
                    productId.FechaModificacion = DateTime.Now;
                    
                    _context.Entry(productId).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    write.GenerarTxt();
                    write.EscribirTxt($"Se modifico el producto con id {productId.FK_IdProducto} del inventario, se agregaron {inventarios.P_Existencia} productos.");
                    return Ok();
                }
                else
                {
                    Inventarios AddInventario = new Inventarios()
                    {
                        IdInventario = inventarios.IdInventario,
                        FK_IdProducto = inventarios.FK_IdProducto,
                        P_Existencia = +inventarios.P_Existencia,
                        P_InventarioMinimo = inventarios.P_InventarioMinimo,
                        FechaModificacion= DateTime.Now 
                    };
                    _context.Add(AddInventario);
                    await _context.SaveChangesAsync();
                    write.GenerarTxt();
                    write.EscribirTxt("Se agrego producto a inventario");
                    return CreatedAtAction("GetInventarios", new { id = inventarios.IdInventario }, inventarios);
                }
            }
            else { return NoContent(); }
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
