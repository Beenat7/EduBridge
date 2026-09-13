using EduBridge.Application.Interfaces;
using EduBridge.Domain.Common.Enums;
using EduBridge.Domain.Entities;
using MediatR;

namespace EduBridge.Application.DirectMessages.Commands;

public sealed record CreateDirectMessageCommand(
    Guid SchoolId,
    Guid StudentId,
    Guid SenderId,
    DirectMessageParticipantType SenderType,
    Guid RecipientId,
    DirectMessageParticipantType RecipientType,
    string Body)
    : IRequest<DirectMessage>;

public sealed class CreateDirectMessageCommandHandler
    : IRequestHandler<CreateDirectMessageCommand, DirectMessage>
{
    private readonly IDirectMessageRepository _directMessageRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IParentRepository _parentRepository;
    private readonly ITeacherRepository _teacherRepository;

    public CreateDirectMessageCommandHandler(
        IDirectMessageRepository directMessageRepository,
        IStudentRepository studentRepository,
        IParentRepository parentRepository,
        ITeacherRepository teacherRepository)
    {
        _directMessageRepository = directMessageRepository;
        _studentRepository = studentRepository;
        _parentRepository = parentRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<DirectMessage> Handle(
        CreateDirectMessageCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(
            request.StudentId,
            cancellationToken);

        if (student is null)
        {
            throw new InvalidOperationException(
                "Student not found.");
        }

        if (student.SchoolId != request.SchoolId)
        {
            throw new InvalidOperationException(
                "Student does not belong to the specified school.");
        }

       if (request.SenderId == request.RecipientId &&
       request.SenderType == request.RecipientType)
        {
            throw new InvalidOperationException(
                "Sender and recipient must be different participants.");
        }

        if (request.SenderType == DirectMessageParticipantType.Parent)
        {
            var sender = await _parentRepository.GetByIdAsync(
                request.SenderId,
                cancellationToken);

            if (sender is null)
            {
                throw new InvalidOperationException(
                    "Parent not found.");
            }

            if (sender.SchoolId != request.SchoolId)
            {
                throw new InvalidOperationException(
                    "Parent does not belong to the specified school.");
            }

            if (student.ParentId != request.SenderId)
            {
                throw new InvalidOperationException(
                    "Parent is not linked to the selected student.");
            }
        }
        else
        {
            var sender = await _teacherRepository.GetByIdAsync(
                request.SenderId,
                cancellationToken);

            if (sender is null)
            {
                throw new InvalidOperationException(
                    "Teacher not found.");
            }

            if (sender.SchoolId != request.SchoolId)
            {
                throw new InvalidOperationException(
                    "Teacher does not belong to the specified school.");
            }

            if (student.ClassId is not null && sender.ClassId is not null &&
                student.ClassId != sender.ClassId)
            {
                throw new InvalidOperationException(
                    "Teacher is not assigned to the student's class.");
            }
        }

        if (request.RecipientType == DirectMessageParticipantType.Parent)
        {
            var recipient = await _parentRepository.GetByIdAsync(
                request.RecipientId,
                cancellationToken);

            if (recipient is null)
            {
                throw new InvalidOperationException(
                    "Parent not found.");
            }

            if (recipient.SchoolId != request.SchoolId)
            {
                throw new InvalidOperationException(
                    "Parent does not belong to the specified school.");
            }

            if (student.ParentId != request.RecipientId)
            {
                throw new InvalidOperationException(
                    "Parent is not linked to the selected student.");
            }
        }
        else
        {
            var recipient = await _teacherRepository.GetByIdAsync(
                request.RecipientId,
                cancellationToken);

            if (recipient is null)
            {
                throw new InvalidOperationException(
                    "Teacher not found.");
            }

            if (recipient.SchoolId != request.SchoolId)
            {
                throw new InvalidOperationException(
                    "Teacher does not belong to the specified school.");
            }

            if (student.ClassId is not null && recipient.ClassId is not null &&
                student.ClassId != recipient.ClassId)
            {
                throw new InvalidOperationException(
                    "Teacher is not assigned to the student's class.");
            }
        }

        var directMessage = new DirectMessage(
            request.SchoolId,
            request.StudentId,
            request.SenderId,
            request.SenderType,
            request.RecipientId,
            request.RecipientType,
            request.Body);

        await _directMessageRepository.AddAsync(
            directMessage,
            cancellationToken);

        await _directMessageRepository.SaveChangesAsync(
            cancellationToken);

        return directMessage;
    }
}
