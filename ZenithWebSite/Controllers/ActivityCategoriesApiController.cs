using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZenithWebSite.Data;
using ZenithWebSite.Models.ZenithModel;

namespace ZenithWebSite.Controllers
{
    [Produces("application/json")]
    [Route("api/ActivityCategories")]
    public class ActivityCategoriesApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityCategoriesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ActivityCategories
        [HttpGet]
        public IEnumerable<ActivityCategory> GetActivityCategories()
        {
            return _context.ActivityCategories;
        }

        // GET: api/ActivityCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityCategory = await _context.ActivityCategories.SingleOrDefaultAsync(m => m.ActivityCategoryId == id);

            if (activityCategory == null)
            {
                return NotFound();
            }

            return Ok(activityCategory);
        }

        // PUT: api/ActivityCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityCategory([FromRoute] int id, [FromBody] ActivityCategory activityCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityCategory.ActivityCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(activityCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityCategoryExists(id))
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

        // POST: api/ActivityCategories
        [HttpPost]
        public async Task<IActionResult> PostActivityCategory([FromBody] ActivityCategory activityCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ActivityCategories.Add(activityCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityCategory", new { id = activityCategory.ActivityCategoryId }, activityCategory);
        }

        // DELETE: api/ActivityCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityCategory = await _context.ActivityCategories.SingleOrDefaultAsync(m => m.ActivityCategoryId == id);
            if (activityCategory == null)
            {
                return NotFound();
            }

            _context.ActivityCategories.Remove(activityCategory);
            await _context.SaveChangesAsync();

            return Ok(activityCategory);
        }

        private bool ActivityCategoryExists(int id)
        {
            return _context.ActivityCategories.Any(e => e.ActivityCategoryId == id);
        }
    }
}