using EduBridge.Application.Interfaces;
using EduBridge.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduBridge.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<EduBridgeDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ISchoolRepository, SchoolRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IParentRepository, ParentRepository>();

        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IClassRepository, ClassRepository>();
        services.AddScoped<IClassSubjectRepository, ClassSubjectRepository>();
        services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();

        return services;
    }
}