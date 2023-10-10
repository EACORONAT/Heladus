using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heladus.Context;
using Heladus.Models;

namespace Heladus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeladoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HeladoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Helado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Helado>>> GetHelado()
        {
          if (_context.Helado == null)
          {
              return NotFound();
          }
            return await _context.Helado.ToListAsync();
        }

        // GET: api/Helado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Helado>> GetHelado(int id)
        {
          if (_context.Helado == null)
          {
              return NotFound();
          }
            var helado = await _context.Helado.FindAsync(id);

            if (helado == null)
            {
                return NotFound();
            }

            return helado;
        }

        // PUT: api/Helado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelado(int id, Helado helado)
        {
            if (id != helado.Id)
            {
                return BadRequest();
            }

            _context.Entry(helado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeladoExists(id))
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

        // POST: api/Helado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Helado>> PostHelado(Helado helado)
        {
          if (_context.Helado == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Helado'  is null.");
          }
            _context.Helado.Add(helado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHelado", new { id = helado.Id }, helado);
        }

        // DELETE: api/Helado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHelado(int id)
        {
            if (_context.Helado == null)
            {
                return NotFound();
            }
            var helado = await _context.Helado.FindAsync(id);
            if (helado == null)
            {
                return NotFound();
            }

            _context.Helado.Remove(helado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HeladoExists(int id)
        {
            return (_context.Helado?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
