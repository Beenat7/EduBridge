using EduBridge.Application.Parents.DTOs.Responses;
using EduBridge.Application.Schools.DTOs.Responses;
using EduBridge.Application.Students.DTOs.Responses;
using EduBridge.Domain.Entities;
using Mapster;

namespace EduBridge.Api.Mapping;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<School, SchoolResponseDto>
            .NewConfig()
            .Map(
                destination => destination.Status,
                source => source.Status.ToString());

        TypeAdapterConfig<Student, StudentResponseDto>
            .NewConfig()
            .Map(
                destination => destination.Status,
                source => source.Status.ToString())
            .Map(
                destination => destination.Gender,
                source => source.Gender.ToString());

        TypeAdapterConfig<Parent, ParentResponseDto>
            .NewConfig()
            .Map(
                destination => destination.Status,
                source => source.Status.ToString());
    }
}