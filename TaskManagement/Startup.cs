﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TaskManagement.Models;

[assembly: OwinStartupAttribute(typeof(TaskManagement.Startup))]
namespace TaskManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            System.Diagnostics.Debug.WriteLine("da");
            System.Diagnostics.Debug.WriteLine(System.Environment.GetEnvironmentVariable("SENDGRID_API_KEY"));
            CreateAdminUserAndApplicationRoles();
        }
        private void CreateAdminUserAndApplicationRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // Se adauga rolurile aplicatiei
            if (!roleManager.RoleExists("Admin"))
            {
                // Se adauga rolul de administrator
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                // se adauga utilizatorul administrator
                var user = new ApplicationUser();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";
                var adminCreated = UserManager.Create(user, "!1Admin");
                if (adminCreated.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
}
