using EduBridge.Application;
using EduBridge.Infrastructure.Persistence;
using Scalar.AspNetCore;
using EduBridge.Api.Exceptions;
using Mapster;
using MapsterMapper;
using EduBridge.Api.Mapping;
using EduBridge.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using EduBridge.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddPersistence(builder.Configuration);

builder.Services
    .AddIdentityCore<EduBridgeUser>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;

        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.AllowedForNewUsers = true;

    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<EduBridgeDbContext>();

builder.Services.AddApplication();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

MappingConfig.RegisterMappings();
builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    await IdentitySeeder.SeedRolesAsync(roleManager);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}


app.UseCors("AngularClient");
app.UseExceptionHandler();

app.MapControllers();
app.Run();