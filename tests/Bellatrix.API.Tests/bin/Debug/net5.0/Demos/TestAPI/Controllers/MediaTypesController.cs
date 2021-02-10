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
    [Route("api/MediaTypes")]
    public class MediaTypesController : Controller
    {
        private readonly MediaStoreContext _context;

        public MediaTypesController(MediaStoreContext context) => _context = context;

        // GET: api/MediaTypes
        [HttpGet]
        public IEnumerable<MediaTypes> GetMediaTypes() => _context.MediaTypes;

        // GET: api/MediaTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMediaTypes([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mediaTypes = await _context.MediaTypes.SingleOrDefaultAsync(m => m.MediaTypeId == id);

            if (mediaTypes == null)
            {
                return NotFound();
            }

            return Ok(mediaTypes);
        }

        // PUT: api/MediaTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMediaTypes([FromRoute] long id, [FromBody] MediaTypes mediaTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mediaTypes.MediaTypeId)
            {
                return BadRequest();
            }

            _context.Entry(mediaTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaTypesExists(id))
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

        // POST: api/MediaTypes
        [HttpPost]
        public async Task<IActionResult> PostMediaTypes([FromBody] MediaTypes mediaTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MediaTypes.Add(mediaTypes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MediaTypesExists(mediaTypes.MediaTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMediaTypes", new { id = mediaTypes.MediaTypeId }, mediaTypes);
        }

        // DELETE: api/MediaTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMediaTypes([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mediaTypes = await _context.MediaTypes.SingleOrDefaultAsync(m => m.MediaTypeId == id);
            if (mediaTypes == null)
            {
                return NotFound();
            }

            _context.MediaTypes.Remove(mediaTypes);
            await _context.SaveChangesAsync();

            return Ok(mediaTypes);
        }

        private bool MediaTypesExists(long id) => _context.MediaTypes.Any(e => e.MediaTypeId == id);
    }
}