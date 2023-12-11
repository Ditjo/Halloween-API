using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Halloween.Repo.Repositories;
using Halloween.Repo.DTO;

namespace Halloween.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroesController : ControllerBase
    {
        private readonly Dbcontext _context;

        public SuperHeroesController(Dbcontext context)
        {
            _context = context;
        }

        // GET: api/SuperHeroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperHero>>> GetSuperHero()
        {
          if (_context.SuperHero == null)
          {
              return NotFound();
          }
            return await _context.SuperHero.ToListAsync();
        }

        // GET: api/SuperHeroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSuperHero(int id)
        {
          if (_context.SuperHero == null)
          {
              return NotFound();
          }
            var superHero = await _context.SuperHero.FindAsync(id);

            if (superHero == null)
            {
                return NotFound();
            }

            return superHero;
        }

        // PUT: api/SuperHeroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuperHero(int id, SuperHero superHero)
        {
            if (id != superHero.Id)
            {
                return BadRequest();
            }

            _context.Entry(superHero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuperHeroExists(id))
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

        // POST: api/SuperHeroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SuperHero>> PostSuperHero(SuperHero superHero)
        {
          if (_context.SuperHero == null)
          {
              return Problem("Entity set 'Dbcontext.SuperHero'  is null.");
          }
            _context.SuperHero.Add(superHero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSuperHero", new { id = superHero.Id }, superHero);
        }

        // DELETE: api/SuperHeroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperHero(int id)
        {
            if (_context.SuperHero == null)
            {
                return NotFound();
            }
            var superHero = await _context.SuperHero.FindAsync(id);
            if (superHero == null)
            {
                return NotFound();
            }

            _context.SuperHero.Remove(superHero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuperHeroExists(int id)
        {
            return (_context.SuperHero?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
