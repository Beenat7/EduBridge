using EduBridge.Application.Schools.DTOs.Responses;
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
    }
}