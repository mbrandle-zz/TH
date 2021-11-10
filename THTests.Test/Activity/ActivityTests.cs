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
        public async Task PuedeAgregarActivity()
        {
            await seed.StartSeed(_context);
            TH.Models.Activity a1 = new TH.Models.Activity()
            {
                property_id = 1,
                schedule = DateTime.Now.AddDays(3),
                title = "Activity 2",
                creted_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Active"
            };

            ActivityController activityController = new ActivityController(_context);

            var response = activityController.AddActivityAsync(a1);
            int totalActivities = await _context.Activities.CountAsync();

            Assert.Equal(4, totalActivities);
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public async Task NoPuedeAgregarActivityEnElMismoHorario()
        {
            await seed.StartSeed(_context);
            TH.Models.Activity a1 = new TH.Models.Activity()
            {
                property_id = 1,
                schedule = DateTime.Now.AddMinutes(90),
                title = "Activity 2",
                creted_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Active"
            };

            ActivityController activityController = new ActivityController(_context);

            var response = activityController.AddActivityAsync(a1);
            int totalActivities = await _context.Activities.CountAsync();

            Assert.Equal(3, totalActivities);
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task NoPuedeAgregarActivityEnUnPropertyDesactivado()
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

            var response = activityController.AddActivityAsync(a1);
            int totalActivities = await _context.Activities.CountAsync();

            Assert.Equal(3, totalActivities);
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        //Test Reagendar Actividades
        [Fact]
        public async Task PuedeReagendar()
        {
            await seed.StartSeed(_context);
            int id = 1;
            TH.Models.Activity activity = await _context.Activities.FirstOrDefaultAsync(a => a.id == id);

            ActivityController activityController = new ActivityController(_context);

            var response = activityController.UpdateScheduleAsync(id, DateTime.Now.AddMinutes(15));

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task NoPuedeReagendarSiLaFechaNoEstaDisponible()
        {
            await seed.StartSeed(_context);
            int id = 1;
            TH.Models.Activity activity = await _context.Activities.FirstOrDefaultAsync(a => a.id == id);

            ActivityController activityController = new ActivityController(_context);

            var response = activityController.UpdateScheduleAsync(id, DateTime.Now.AddMinutes(90));

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        //Test Cancelar Activity
        [Fact]
        public async Task PuedeCancelarUnaActividad()
        {
            await seed.StartSeed(_context);
            int id = 1;            

            ActivityController activityController = new ActivityController(_context);

            var response = activityController.CancelarActivityAsync(id);
            TH.Models.Activity activity = await _context.Activities.FirstOrDefaultAsync(a => a.id == id);

            Assert.Equal("Cancel", activity.status);
            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task NoPuedeCancelarUnaActividadInactiva()
        {
            await seed.StartSeed(_context);
            int id = 3;

            ActivityController activityController = new ActivityController(_context);

            var response = activityController.CancelarActivityAsync(id);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
    }
}
