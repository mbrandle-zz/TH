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
                creted_at = DateTime.Now,
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
                creted_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = "Active"
            };

            _context.Properties.Add(p2);
            await _context.SaveChangesAsync();
        }
    }
}
