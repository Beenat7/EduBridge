using EduBridge.Application.Parents.DTOs.Responses;
using EduBridge.Application.Schools.DTOs.Responses;
using EduBridge.Application.Students.DTOs.Responses;
using EduBridge.Application.Teachers.DTOs.Responses;
using EduBridge.Application.Subjects.DTOs.Responses;
using EduBridge.Application.Classes.DTOs.Responses;
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
                source => source.Gender.ToString())
            .Map(
                destination => destination.ClassName,
                source => source.Class == null ? null : source.Class.Name);

        TypeAdapterConfig<Teacher, TeacherResponseDto>
            .NewConfig()
            .Map(
                destination => destination.Status,
                source => source.Status.ToString())
            .Map(
                destination => destination.ClassName,
                source => source.Class == null ? null : source.Class.Name);

        TypeAdapterConfig<Parent, ParentResponseDto>
            .NewConfig()
            .Map(
                destination => destination.Status,
                source => source.Status.ToString());

        TypeAdapterConfig<Subject, SubjectResponseDto>
            .NewConfig()
            .Map(
                destination => destination.Status,
                source => source.Status.ToString());

        TypeAdapterConfig<Class, ClassResponseDto>
            .NewConfig()
            .Map(
                destination => destination.Status,
                source => source.Status.ToString())
            .Map(
                destination => destination.GradeLevel,
                source => source.GradeLevel.ToString());

        TypeAdapterConfig<Subject, ClassSubjectResponseDto>
            .NewConfig()
            .Map(
                destination => destination.Status,
                source => source.Status.ToString());

        TypeAdapterConfig<Class, SubjectClassResponseDto>
            .NewConfig()
            .Map(
                destination => destination.GradeLevel,
                source => source.GradeLevel.ToString())
            .Map(
                destination => destination.Status,
                source => source.Status.ToString());

        TypeAdapterConfig<Subject, TeacherSubjectResponseDto>
            .NewConfig()
            .Map(
                destination => destination.Status,
                source => source.Status.ToString());

        TypeAdapterConfig<Teacher, SubjectTeacherResponseDto>
            .NewConfig()
            .Map(
                destination => destination.Status,
                source => source.Status.ToString());
    }
}