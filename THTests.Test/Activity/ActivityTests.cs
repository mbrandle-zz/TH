using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TH.Controllers;
using TH.Data;
using THTests.Test.Data;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace THTests.Test.Activity
{
    public class ActivityTests
    {
        AppDBContext _context = new AppDBContext(THTests.Test.Utilities.Utilities.TestDbContextOptions());
        Seed seed = new Seed();

        //Test Add Activity
        [Fact]
        public async Task PuedeAgregarActivityAsync()
        {
            await seed.StartSeed(_context);
            TH.Models.Activity a1 = new TH.Models.Activity()
            {
                property_id = 1,
                schedule = DateTime.Now.AddMinutes(-60),
                title = "Activity 2",
                creted_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Active"
            };

            ActivityController activityController = new ActivityController(_context);

            var response = activityController.AddActivity(a1);
            int totalActivities = await _context.Activities.CountAsync();

            Assert.Equal(2, totalActivities);
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public async Task NoPuedeAgregarActivityEnElMismoHorarioAsync()
        {
            await seed.StartSeed(_context);
            TH.Models.Activity a1 = new TH.Models.Activity()
            {
                property_id = 1,
                schedule = DateTime.Now.AddMinutes(30),
                title = "Activity 2",
                creted_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Active"
            };

            ActivityController activityController = new ActivityController(_context);

            var response = activityController.AddActivity(a1);
            int totalActivities = await _context.Activities.CountAsync();

            Assert.Equal(1, totalActivities);
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task NoPuedeAgregarActivityEnUnPropertyDesactivadoAsync()
        {
            await seed.StartSeed(_context);
            TH.Models.Activity a1 = new TH.Models.Activity()
            {
                property_id = 3,
                schedule = DateTime.Now.AddMinutes(90),
                title = "Activity 2",
                creted_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Active"
            };

            ActivityController activityController = new ActivityController(_context);

            var response = activityController.AddActivity(a1);
            int totalActivities = await _context.Activities.CountAsync();

            Assert.Equal(1, totalActivities);
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
    }
}
