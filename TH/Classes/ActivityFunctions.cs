using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TH.Data;
using TH.Models;

namespace TH.Classes
{
    public static class ActivityFunctions
    {

        public static async Task<bool> isScheduleValidAsync(this Activity activiy, AppDBContext _context)
        {
            Activity activityAvailable = await _context.Activities
                .FirstOrDefaultAsync
                (
                    a => a.property_id == activiy.property_id &&
                    ( a.schedule <= activiy.schedule && a.schedule.AddMinutes(60) >= activiy.schedule)
                );

            if(activityAvailable == null)
            {
                return true;
            }

            return false;
        }

    }
}
