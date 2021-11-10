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

        [HttpPut]
        public async Task<IActionResult> AddActivityAsync(Activity activity)
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

                return Ok(activity);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
            
        }

        [Route("{id}")]
        [HttpPatch]
        public async Task<IActionResult> UpdateScheduleAsync(int id, DateTime newSchedule)
        {
            try
            {
                if(newSchedule == null)
                {
                    return BadRequest("The Schedule Is Not Valid");
                }

                Activity activity = await _context.Activities.FirstOrDefaultAsync(a => a.id == id);
                activity.schedule = newSchedule;

                if (!activity.IsActive())
                {
                    return BadRequest("The Activity Is Not Valid");
                }

                if (!await activity.IsScheduleValidAsync(_context))
                {
                    return BadRequest("The Schedule Is Not Valid");
                }

                activity.updated_at = DateTime.Now;
                _context.Attach(activity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(" Schedule Updated ID: " + id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> CancelarActivityAsync(int id)
        {
            try
            {
                Activity activity = await _context.Activities.FirstOrDefaultAsync(a => a.id == id);

                if (!activity.IsActive())
                {
                    return BadRequest("The Activity Is Not Valid");
                }

                activity.status = "Cancel";
                _context.Attach(activity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(" Schedule Cancel ID: " + id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetActividadesAsync(DateTime? startDate = null, DateTime? endDate = null, string? status = null)
        {
            try
            {
                if (startDate == null)
                {
                    startDate = DateTime.Now.AddDays(-3).Date;
                }

                if (endDate == null)
                {
                    endDate = DateTime.Now.AddDays(14).Date;
                }

                List<Activity> activities = await _context.Activities
                    .Include(p => p.Property)
                    .Where(a => a.schedule >= startDate && a.schedule <= endDate)
                    .ToListAsync();

                if (status != null)
                {
                    activities = activities.Where(a => a.status.Equals(status)).ToList();
                }

                List<ActivityResponse> listResponse = new List<ActivityResponse>();

                foreach(Activity item in activities)
                {
                    PropertyResponse property = new PropertyResponse()
                    {
                        ID = item.Property.id,
                        address = item.Property.address,
                        title = item.Property.title
                    };

                    ActivityResponse response = new ActivityResponse()
                    {
                        ID = item.id,
                        schedule = item.schedule,
                        title = item.title,
                        created_at = item.created_at,
                        status = item.status,
                        condition = item.GetCondition(),
                        property = property,
                        survey = $"https://localhost:44327/api/Survey/{item.id}"
                    };
                    listResponse.Add(response);
                }

                return Ok(listResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
