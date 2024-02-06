using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMC.API.Data;
using MMC.API.Models;
using MMC.API.Services;

namespace MMC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly AppDbContext _context; // Assurez-vous d'injecter le DbContext approprié ici
        //private readonly IEventService _eventService;

        //public EventController(AppDbContext context, IEventService eventService)
        //{
        //    _context = context;
        //    _eventService = eventService;
        //}
        //// GET: api/Event
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        //{
        //    var events = await _eventService.GetAllEventsAsync();
        //    return Ok(events);
        //}

        //// GET: api/Event/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Event>> GetEvent(int id)
        //{
        //    var @event = await _context.Events.FindAsync(id);

        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(@event);
        //}

        //// POST: api/Event
        //[HttpPost]
        //public async Task<ActionResult<Event>> PostEvent(Event @event)
        //{
        //    _context.Events.Add(@event);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        //}

        //// PUT: api/Event/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEvent(int id, Event @event)
        //{
        //    if (id != @event.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(@event).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EventExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// DELETE: api/Event/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEvent(int id)
        //{
        //    var @event = await _context.Events.FindAsync(id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Events.Remove(@event);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool EventExists(int id)
        //{
        //    return _context.Events.Any(e => e.Id == id);
        //}

    }
}
