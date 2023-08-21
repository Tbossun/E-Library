using E_Library.Data.Context;
using E_Library.Data.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Seeder
{
    public static class Seeder
    {
        public static async void SeedDataBase(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            try
            {
                await context.Database.EnsureCreatedAsync();
                if (!context.Roles.Any())
                {
                    string roles = File.ReadAllText(@"JsonFiles/Roles.json");
                    List<IdentityRole> listOfRoles = JsonConvert.DeserializeObject<List<IdentityRole>>(roles);
                    //List<string> listOfRoles = new List<string> { "Admin", "Regular" };

                    foreach (var role in listOfRoles)
                    {
                        await roleManager.CreateAsync(role);
                    }

                }
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
