using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R52_Last_Evidence_API.Models;

namespace R52_Last_Evidence_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecsController : ControllerBase
    {
        private readonly DeviceDbContext _context;

        public SpecsController(DeviceDbContext context)
        {
            _context = context;
        }

        // GET: api/Specs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spec>>> GetSpecs()
        {
          if (_context.Specs == null)
          {
              return NotFound();
          }
            return await _context.Specs.ToListAsync();
        }
        //Custom
       
        /// ///////////////////////////////
  
        [HttpGet("Device/Specs/{id}")]
        public async Task<ActionResult<IEnumerable<Spec>>> GetDeviceSpecs(int id)
        {
            if (_context.Specs == null)
            {
                return NotFound();
            }
            return await _context.Specs.Where(s=> s.DeviceId== id).ToListAsync();
        }
        // GET: api/Specs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Spec>> GetSpec(int id)
        {
          if (_context.Specs == null)
          {
              return NotFound();
          }
            var spec = await _context.Specs.FindAsync(id);

            if (spec == null)
            {
                return NotFound();
            }

            return spec;
        }

        // PUT: api/Specs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpec(int id, Spec spec)
        {
            if (id != spec.SpecId)
            {
                return BadRequest();
            }

            _context.Entry(spec).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecExists(id))
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

        // POST: api/Specs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Spec>> PostSpec(Spec spec)
        {
          if (_context.Specs == null)
          {
              return Problem("Entity set 'DeviceDbContext.Specs'  is null.");
          }
            _context.Specs.Add(spec);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpec", new { id = spec.SpecId }, spec);
        }

        // DELETE: api/Specs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpec(int id)
        {
            if (_context.Specs == null)
            {
                return NotFound();
            }
            var spec = await _context.Specs.FindAsync(id);
            if (spec == null)
            {
                return NotFound();
            }

            _context.Specs.Remove(spec);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecExists(int id)
        {
            return (_context.Specs?.Any(e => e.SpecId == id)).GetValueOrDefault();
        }
    }
}
