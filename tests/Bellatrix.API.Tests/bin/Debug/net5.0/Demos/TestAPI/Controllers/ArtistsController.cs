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
    [Route("api/Artists")]
    public class ArtistsController : Controller
    {
        private readonly MediaStoreContext _context;

        public ArtistsController(MediaStoreContext context) => _context = context;

        // GET: api/Artists
        [HttpGet]
        public IEnumerable<Artists> GetArtists() => _context.Artists;

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtists([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artists = await _context.Artists.SingleOrDefaultAsync(m => m.ArtistId == id);

            if (artists == null)
            {
                return NotFound();
            }

            return Ok(artists);
        }

        // PUT: api/Artists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtists([FromRoute] long id, [FromBody] Artists artists)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artists.ArtistId)
            {
                return BadRequest();
            }

            _context.Entry(artists).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistsExists(id))
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

        // POST: api/Artists
        [HttpPost]
        public async Task<IActionResult> PostArtists([FromBody] Artists artists)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Artists.Add(artists);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArtistsExists(artists.ArtistId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArtists", new { id = artists.ArtistId }, artists);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtists([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artists = await _context.Artists.SingleOrDefaultAsync(m => m.ArtistId == id);
            if (artists == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artists);
            await _context.SaveChangesAsync();

            return Ok(artists);
        }

        private bool ArtistsExists(long id) => _context.Artists.Any(e => e.ArtistId == id);
    }
}