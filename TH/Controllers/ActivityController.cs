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

        [HttpGet]
        public async Task<IActionResult> AddActivity(Activity activity)
        {
            try
            {
                Property property = await _context.Properties.FirstOrDefaultAsync(p => p.id == activity.property_id);

                if (!property.IsValid() || property.IsDisabled())
                {
                    return BadRequest("The Property Is Not Valid");
                }

                if (!await activity.isScheduleValidAsync(_context))
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
    }
}
