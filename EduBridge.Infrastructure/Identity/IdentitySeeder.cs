using EduBridge.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EduBridge.Infrastructure.Identity;

public static class IdentitySeeder
{
    private static readonly string[] Roles =
    [
        "PlatformAdmin",
        "SchoolAdmin",
        "Teacher",
        "Parent"
    ];

    public static async Task SeedRolesAsync(
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(
                    new IdentityRole<Guid>(role));
            }
        }
    }

    public static async Task SeedDevelopmentUserAsync(
        UserManager<EduBridgeUser> userManager)
    {
        const string email = "admin@edubridge.com";
        const string password = "Admin123!";

        var user = await userManager.FindByEmailAsync(email);

        if (user is not null)
        {
            return;
        }

        var developmentUser = new EduBridgeUser
        {
            Id = Guid.NewGuid(),
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(
            developmentUser,
            password);

        if (!result.Succeeded)
        {
            var errors = string.Join(
                ", ",
                result.Errors.Select(error => error.Description));

            throw new InvalidOperationException(
                $"Failed to create development user: {errors}");
        }

        await userManager.AddToRoleAsync(
            developmentUser,
            "PlatformAdmin");
    }
}