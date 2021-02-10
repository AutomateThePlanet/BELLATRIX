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
    [Route("api/InvoiceItems")]
    public class InvoiceItemsController : Controller
    {
        private readonly MediaStoreContext _context;

        public InvoiceItemsController(MediaStoreContext context) => _context = context;

        // GET: api/InvoiceItems
        [HttpGet]
        public IEnumerable<InvoiceItems> GetInvoiceItems() => _context.InvoiceItems;

        // GET: api/InvoiceItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceItems([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceItems = await _context.InvoiceItems.SingleOrDefaultAsync(m => m.InvoiceLineId == id);

            if (invoiceItems == null)
            {
                return NotFound();
            }

            return Ok(invoiceItems);
        }

        // PUT: api/InvoiceItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceItems([FromRoute] long id, [FromBody] InvoiceItems invoiceItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceItems.InvoiceLineId)
            {
                return BadRequest();
            }

            _context.Entry(invoiceItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceItemsExists(id))
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

        // POST: api/InvoiceItems
        [HttpPost]
        public async Task<IActionResult> PostInvoiceItems([FromBody] InvoiceItems invoiceItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InvoiceItems.Add(invoiceItems);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvoiceItemsExists(invoiceItems.InvoiceLineId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvoiceItems", new { id = invoiceItems.InvoiceLineId }, invoiceItems);
        }

        // DELETE: api/InvoiceItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceItems([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceItems = await _context.InvoiceItems.SingleOrDefaultAsync(m => m.InvoiceLineId == id);
            if (invoiceItems == null)
            {
                return NotFound();
            }

            _context.InvoiceItems.Remove(invoiceItems);
            await _context.SaveChangesAsync();

            return Ok(invoiceItems);
        }

        private bool InvoiceItemsExists(long id) => _context.InvoiceItems.Any(e => e.InvoiceLineId == id);
    }
}