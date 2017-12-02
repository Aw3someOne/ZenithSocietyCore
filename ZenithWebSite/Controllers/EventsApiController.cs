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
    [Route("api/Events")]
    public class EventsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public IEnumerable<Event> GetEvents()
        {
            return _context.Events;
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Events.SingleOrDefaultAsync(m => m.EventId == id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // GET: api/Events/WeekOf/0/
        [HttpGet("WeekOf/{week}")]
        public IActionResult GetWeekOf([FromRoute] int week)
        {
            var today = DateTime.Today;
            var monday = GetStartOfWeek(today).AddDays(7 * week);
            return Json(monday);
        }

        // GET: api/Events/Week/0
        [HttpGet("Week/{week}")]
        public IActionResult GetEventsByWeek([FromRoute] int week)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var today = DateTime.Today;
            var monday = GetStartOfWeek(today).AddDays(7 * week);

            var start = monday;
            var end = start.AddDays(7);

            var events = _context.Events
                .Include(m => m.ActivityCategory)
                .Where(m => m.StartTime >= monday && m.EndTime < end && m.IsActive)
                .OrderBy(m => m.StartTime)
                .Select(m => new EventDTO(m));

            //Dictionary<DateTime, List<EventDTO>> groupedEvents = new Dictionary<DateTime, List<EventDTO>>();
            //foreach (EventDTO ev in events)
            //{
            //    var date = ev.StartTime.Date;
            //    if (!groupedEvents.ContainsKey(date))
            //    {
            //        groupedEvents[date] = new List<EventDTO>();
            //    }
            //    groupedEvents[date].Add(ev);
            //}

            return Ok(events);
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] int id, [FromBody] Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.EventId)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.EventId }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Events.SingleOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return Ok(@event);
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            int diff = date.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }
            return date.AddDays(-diff).Date;
        }
    }
}