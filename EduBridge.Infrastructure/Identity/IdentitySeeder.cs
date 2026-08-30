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
}