using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region Additional Namespaces
using WebApp.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Configuration;
#endregion

namespace WebApp.Security
{
    public class SecurityDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {
            #region Seed the roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var startupRoles = ConfigurationManager.AppSettings["startupRoles"].Split(';');
            foreach (var role in startupRoles)
                roleManager.Create(new IdentityRole { Name = role });
            #endregion

            #region Seed the users
            string adminUser = ConfigurationManager.AppSettings["adminUserName"];
            string adminRole = ConfigurationManager.AppSettings["adminRole"];
            string adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            string adminPassword = ConfigurationManager.AppSettings["adminPassword"];
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var result = userManager.Create(new ApplicationUser
            {
                UserName = adminUser,
                Email = adminEmail,
                EmployeeId = null,
                CustomerId = null
            }, adminPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(adminUser).Id, adminRole);

            // Employee via appsettings
            string theUser = ConfigurationManager.AppSettings["emp1UserName"];
            string theRole = ConfigurationManager.AppSettings["Manager"];
            string theEmail = ConfigurationManager.AppSettings["emp1Email"];
            string thePassword = ConfigurationManager.AppSettings["thePassword"];
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            result = userManager.Create(new ApplicationUser
            {
                UserName = theUser,
                Email = theEmail,
                EmployeeId = 2,
                CustomerId = null
            }, thePassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(theUser).Id, theRole);

            // Employee via local hard coded values
             theUser = "JPeacock";
             theRole = "Sale";
             theEmail = "JPeaCock@Chinook.nait.ca";
             thePassword = "123456";
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            result = userManager.Create(new ApplicationUser
            {
                UserName = theUser,
                Email = theEmail,
                EmployeeId = 3,
                CustomerId = null
            }, thePassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(theUser).Id, theRole);

            // Customer via values
             theUser = "HansenB";
             theRole = "Customer";
             theEmail = "bjon.hansen@yahoo.no";
             thePassword = ConfigurationManager.AppSettings["thePassword"];
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            result = userManager.Create(new ApplicationUser
            {
                UserName = theUser,
                Email = theEmail,
                EmployeeId = null,
                CustomerId = 4
            }, thePassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(theUser).Id, theRole);
            #endregion

            // ... etc. ...

            base.Seed(context);
        }
    }
}