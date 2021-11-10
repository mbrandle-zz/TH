using System;
using System.Threading.Tasks;
using TH.Data;

namespace THTests.Test.Data
{
    public class Seed
    {
        public async Task StartSeed (AppDBContext _context)
        {
           TH.Models.Property p1 = new TH.Models.Property()
            {
                title = "Title 1",
                address = "Adress 1",
                description = "Description 1",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Active"
            };

            _context.Properties.Add(p1);
            await _context.SaveChangesAsync();

            TH.Models.Property p2 = new TH.Models.Property()
            {
                title = "Title 2",
                address = "Adress 2",
                description = "Description 2",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Active"
            };

            _context.Properties.Add(p2);
            await _context.SaveChangesAsync();

            TH.Models.Property p3 = new TH.Models.Property()
            {
                title = "Title 3",
                address = "Adress 3",
                description = "Description 3",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Disable",
                disabled_at = DateTime.Now
            };

            _context.Properties.Add(p3);
            await _context.SaveChangesAsync();

            TH.Models.Activity a1 = new TH.Models.Activity()
            {
                property_id = p1.id,
                schedule = DateTime.Now,
                title = "Activity 1",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Active"
            };
            _context.Activities.Add(a1);
            await _context.SaveChangesAsync();

            TH.Models.Activity a2 = new TH.Models.Activity()
            {
                property_id = p1.id,
                schedule = DateTime.Now.AddHours(2),
                title = "Activity 2",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Active"
            };
            _context.Activities.Add(a2);
            await _context.SaveChangesAsync();

            TH.Models.Activity a3 = new TH.Models.Activity()
            {
                property_id = p1.id,
                schedule = DateTime.Now.AddHours(3),
                title = "Activity 3",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Cancel"
            };
            _context.Activities.Add(a3);
            await _context.SaveChangesAsync();
        }
    }
}
