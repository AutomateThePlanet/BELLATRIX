using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaStore.Demo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.Demo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Invoices")]
    public class InvoicesController : Controller
    {
        private readonly MediaStoreContext _context;

        public InvoicesController(MediaStoreContext context) => _context = context;

        // GET: api/Invoices
        [HttpGet]
        public IEnumerable<Invoices> GetInvoices() => _context.Invoices;

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoices([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoices = await _context.Invoices.SingleOrDefaultAsync(m => m.InvoiceId == id);

            if (invoices == null)
            {
                return NotFound();
            }

            return Ok(invoices);
        }

        // PUT: api/Invoices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoices([FromRoute] long id, [FromBody] Invoices invoices)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoices.InvoiceId)
            {
                return BadRequest();
            }

            _context.Entry(invoices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoicesExists(id))
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

        // POST: api/Invoices
        [HttpPost]
        public async Task<IActionResult> PostInvoices([FromBody] Invoices invoices)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Invoices.Add(invoices);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvoicesExists(invoices.InvoiceId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvoices", new { id = invoices.InvoiceId }, invoices);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoices([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoices = await _context.Invoices.SingleOrDefaultAsync(m => m.InvoiceId == id);
            if (invoices == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoices);
            await _context.SaveChangesAsync();

            return Ok(invoices);
        }

        private bool InvoicesExists(long id) => _context.Invoices.Any(e => e.InvoiceId == id);
    }
}