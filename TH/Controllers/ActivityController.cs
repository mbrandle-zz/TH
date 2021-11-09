using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TH.Classes;
using TH.Data;
using TH.Models;

namespace TH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ActivityController(AppDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddActivity(Activity activity)
        {
            try
            {
                Property property = await _context.Properties.FirstOrDefaultAsync(p => p.id == activity.property_id);

                if (!property.IsValid() || property.IsDisabled())
                {
                    return BadRequest("The Property Is Not Valid");
                }

                if (!await activity.IsScheduleValidAsync(_context))
                {
                    return BadRequest("The Schedule Is Not Valid");
                }

                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();

                return CreatedAtAction("New Activity", "ID: " + activity.id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
            
        }

        [Route("[action]/{id}")]
        [HttpPatch]
        public async Task<IActionResult> UpdateSchedule(int id, DateTime newSchedule)
        {
            Activity activity = await _context.Activities.FirstOrDefaultAsync(a => a.id == id);
            activity.schedule = newSchedule;

            if (!activity.IsActive() )
            {
                return BadRequest("The Activity Is Not Valid");
            }

            if (!await activity.IsScheduleValidAsync(_context))
            {
                return BadRequest("The Schedule Is Not Valid");
            }

            
            _context.Attach(activity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(" Schedule Updated ID: " + id);
        }
    }
}
