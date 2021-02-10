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
    [Route("api/Genres")]
    public class GenresController : Controller
    {
        private readonly MediaStoreContext _context;

        public GenresController(MediaStoreContext context) => _context = context;

        // GET: api/Genres
        [HttpGet]
        public IEnumerable<Genres> GetGenres() => _context.Genres;

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenres([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genres = await _context.Genres.SingleOrDefaultAsync(m => m.GenreId == id);

            if (genres == null)
            {
                return NotFound();
            }

            return Ok(genres);
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenres([FromRoute] long id, [FromBody] Genres genres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genres.GenreId)
            {
                return BadRequest();
            }

            _context.Entry(genres).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenresExists(id))
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

        // POST: api/Genres
        [HttpPost]
        public async Task<IActionResult> PostGenres([FromBody] Genres genres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Genres.Add(genres);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GenresExists(genres.GenreId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGenres", new { id = genres.GenreId }, genres);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenres([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genres = await _context.Genres.SingleOrDefaultAsync(m => m.GenreId == id);
            if (genres == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genres);
            await _context.SaveChangesAsync();

            return Ok(genres);
        }

        private bool GenresExists(long id) => _context.Genres.Any(e => e.GenreId == id);
    }
}