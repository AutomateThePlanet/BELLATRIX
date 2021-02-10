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
    [Route("api/PlaylistTracks")]
    public class PlaylistTracksController : Controller
    {
        private readonly MediaStoreContext _context;

        public PlaylistTracksController(MediaStoreContext context) => _context = context;

        // GET: api/PlaylistTracks
        [HttpGet]
        public IEnumerable<PlaylistTrack> GetPlaylistTrack() => _context.PlaylistTrack;

        // GET: api/PlaylistTracks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylistTrack([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var playlistTrack = await _context.PlaylistTrack.SingleOrDefaultAsync(m => m.PlaylistId == id);

            if (playlistTrack == null)
            {
                return NotFound();
            }

            return Ok(playlistTrack);
        }

        // PUT: api/PlaylistTracks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylistTrack([FromRoute] long id, [FromBody] PlaylistTrack playlistTrack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playlistTrack.PlaylistId)
            {
                return BadRequest();
            }

            _context.Entry(playlistTrack).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistTrackExists(id))
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

        // POST: api/PlaylistTracks
        [HttpPost]
        public async Task<IActionResult> PostPlaylistTrack([FromBody] PlaylistTrack playlistTrack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PlaylistTrack.Add(playlistTrack);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlaylistTrackExists(playlistTrack.PlaylistId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlaylistTrack", new { id = playlistTrack.PlaylistId }, playlistTrack);
        }

        // DELETE: api/PlaylistTracks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylistTrack([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var playlistTrack = await _context.PlaylistTrack.SingleOrDefaultAsync(m => m.PlaylistId == id);
            if (playlistTrack == null)
            {
                return NotFound();
            }

            _context.PlaylistTrack.Remove(playlistTrack);
            await _context.SaveChangesAsync();

            return Ok(playlistTrack);
        }

        private bool PlaylistTrackExists(long id) => _context.PlaylistTrack.Any(e => e.PlaylistId == id);
    }
}