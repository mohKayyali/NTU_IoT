using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NTU.IoT.Models;

namespace NTU.IoT.DataAccess.DbInitializer
{
	public class DbInitializer : IDbInitializer
	{
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDBContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDBContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }



            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("Super User")).GetAwaiter().GetResult();

                //if roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin",
                    IsActive= true
                }, "adminNTU123!").GetAwaiter().GetResult();


                ApplicationUser user = _db.applicationUsers.FirstOrDefault(u => u.UserName == "admin");

                if(user!=null)
                _userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();

            }

            return;
        }
    }
}

