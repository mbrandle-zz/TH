using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TH.Data;
using TH.Models;

namespace TH.Classes
{
    public static class ActivityFunctions
    {

        public static async Task<bool> IsScheduleValidAsync(this Activity activiy, AppDBContext _context)
        {
            Activity activityAvailable = await _context.Activities
                .FirstOrDefaultAsync
                (
                    a => a.property_id == activiy.property_id &&
                    a.status.Equals("Active") &&
                    (
                        (a.schedule <= activiy.schedule && a.schedule.AddMinutes(60) >= activiy.schedule) ||
                        (a.schedule <= activiy.schedule.AddMinutes(60) && a.schedule.AddMinutes(60) >= activiy.schedule.AddMinutes(60))
                    )
                );

            if (activityAvailable == null)
            {
                return true;
            }

            if(activityAvailable.id == activiy.id)
            {
                return true;
            }

            return false;
        }

        public static bool IsActive(this Activity activity)
        {
            if (activity.status.Equals("Active"))
            {
                return true;
            }

            return false;
        }

        public static string GetCondition(this Activity activity)
        {
            string condition = "";

            if (activity.status.Equals("Done"))
            {
                condition = "Finalizada";
            }

            if (activity.status.Equals("Active") && activity.schedule >= DateTime.Now)
            {
                condition = "Pendiente a realizar";
            }
            else if (activity.status.Equals("Active") && activity.schedule < DateTime.Now)
            {
                condition = "Atrasada";
            }

            return condition;
        }

    }
}
