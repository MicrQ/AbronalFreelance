﻿using AbronalFreelance.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace AbronalFreelance.Server;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        string[] roleNames = { "Admin", "Client", "Freelancer" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create an admin user if necessary
        var adminUser = new User
        {
            UserName = "adminX",
            Email = "admin@admin.com",
            FirstName = "Admin",
            LastName = "User",
            LocationId = 1,
            CreatedAt = DateTime.Now,
            IsActive = true
        };

        var user = await userManager.FindByEmailAsync(adminUser.Email);
        if (user == null)
        {
            var createAdminUser = await userManager.CreateAsync(adminUser, "Admin@123");
            if (createAdminUser.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
