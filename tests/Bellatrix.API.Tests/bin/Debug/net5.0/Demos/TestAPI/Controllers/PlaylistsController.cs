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
    [Route("api/Playlists")]
    public class PlaylistsController : Controller
    {
        private readonly MediaStoreContext _context;

        public PlaylistsController(MediaStoreContext context) => _context = context;

        // GET: api/Playlists
        [HttpGet]
        public IEnumerable<Playlists> GetPlaylists() => _context.Playlists;

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylists([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var playlists = await _context.Playlists.SingleOrDefaultAsync(m => m.PlaylistId == id);

            if (playlists == null)
            {
                return NotFound();
            }

            return Ok(playlists);
        }

        // PUT: api/Playlists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylists([FromRoute] long id, [FromBody] Playlists playlists)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playlists.PlaylistId)
            {
                return BadRequest();
            }

            _context.Entry(playlists).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistsExists(id))
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

        // POST: api/Playlists
        [HttpPost]
        public async Task<IActionResult> PostPlaylists([FromBody] Playlists playlists)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Playlists.Add(playlists);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlaylistsExists(playlists.PlaylistId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlaylists", new { id = playlists.PlaylistId }, playlists);
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylists([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var playlists = await _context.Playlists.SingleOrDefaultAsync(m => m.PlaylistId == id);
            if (playlists == null)
            {
                return NotFound();
            }

            _context.Playlists.Remove(playlists);
            await _context.SaveChangesAsync();

            return Ok(playlists);
        }

        private bool PlaylistsExists(long id) => _context.Playlists.Any(e => e.PlaylistId == id);
    }
}