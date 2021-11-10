using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TH.Controllers;
using TH.Data;
using THTests.Test.Data;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace THTests.Test.Property
{
    
    public class PropertyTests
    {
        AppDBContext _context = new AppDBContext(THTests.Test.Utilities.Utilities.TestDbContextOptions());
        Seed seed = new Seed();

        //Test Get Property
        [Fact]
        public async Task PuedeObtenerLaListaDeProperties()
        {
            await seed.StartSeed(_context);
            PropertyController propertyController = new PropertyController(_context);

            var response = propertyController.GetPropertiesAsync();

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task PuedeObtenerPropertyById()
        {
            await seed.StartSeed(_context);
            PropertyController propertyController = new PropertyController(_context);

            var response = propertyController.GetPropertyByIdAsync(1);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task NoPuedeObtenerPropertySiElIdNoExiste()
        {
            await seed.StartSeed(_context);
            PropertyController propertyController = new PropertyController(_context);

            var response = propertyController.GetPropertyByIdAsync(100);

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }       

        //Test Post Property
        [Fact]
        public async Task PuedeAgregarProperty()
        {
            TH.Models.Property p1 = new TH.Models.Property()
            {
                title = "Title 1",
                address = "Adress 1",
                description = "Description 1",
                creted_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "active"
            };

            PropertyController propertyController = new PropertyController(_context);

            var response = propertyController.AddProperty(p1);
            int totalProperties = await _context.Properties.CountAsync();

            Assert.Equal(1, totalProperties);
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public async Task NoPuedeAgregarPropertySiFaltaUnCampoObligatorio()
        {
            TH.Models.Property p1 = new TH.Models.Property()
            {               
                address = "Adress 1",
                description = "Description 1",
                creted_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "active"
            };

            PropertyController propertyController = new PropertyController(_context);

            var response = propertyController.AddProperty(p1);
            int totalProperties = await _context.Properties.CountAsync();

            Assert.Equal(0, totalProperties);
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }


        //Test Disable Property
        [Fact]
        public async Task PuedeInhabilitarPropertyById()
        {
            await seed.StartSeed(_context);
            PropertyController propertyController = new PropertyController(_context);

            int id = 1;

            var response = propertyController.DisableProperty(id);
            TH.Models.Property property = await _context.Properties.FirstOrDefaultAsync(p => p.id == id);

            Assert.Equal("Disable", property.status);            
            Assert.IsType<AcceptedResult>(response.Result);
        }

        [Fact]
        public async Task NoPuedeInhabilitarPropertySiNoExisteElId()
        {
            await seed.StartSeed(_context);
            PropertyController propertyController = new PropertyController(_context);

            int id = 100;

            var response = propertyController.DisableProperty(id);

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }
    }
}
