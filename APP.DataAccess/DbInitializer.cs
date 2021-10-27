using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APP.DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Companies.Any())
            {
                return;   // database has been seeded
            }

            context.Companies.Add(new Entities.Company
            {
                Code = "XYZ",
                Name = "Company1",
                PhoneNumber = 125555,
            });
            context.SaveChanges();

        }
    }
}
