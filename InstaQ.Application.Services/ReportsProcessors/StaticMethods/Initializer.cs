using InstaQ.Application.Abstractions.InstagramRequests.ServicesInterfaces;
using InstaQ.Application.Abstractions.ReportsProcessors.Exceptions;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.Participants.Specification;
using InstaQ.Domain.Participants.Specification.Visitor;
using InstaQ.Domain.Reposts.PublicationReport.DTOs;
using InstaQ.Domain.Reposts.PublicationReport.Entities;
using InstaQ.Domain.Specifications;
using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.Specification;
using InstaQ.Domain.Users.Specification.Visitor;

namespace InstaQ.Application.Services.ReportsProcessors.StaticMethods;

internal static class Initializer
{
    public static async Task<IEnumerable<PublicationDto>> GetPublicationsAsync(PublicationReport report, IPublicationsService publicationGetterService, CancellationToken token)
    {
        var publications =
            await publicationGetterService.GetAsync(report.Hashtag, 500, token);
        return publications.Select(x => new PublicationDto(x.PublicationId, x.OwnerId));
    }

    ///<exception cref="LinkedUserNotFoundException">User in coauthors list not found</exception>
    public static async Task<IEnumerable<ChatParticipants>> GetParticipantsAsync(PublicationReport report,
        IUnitOfWork unitOfWork)
    {
        ISpecification<User, IUserSpecificationVisitor> userSpec = new UserByIdSpecification(report.UserId);

        ISpecification<Participant, IParticipantSpecificationVisitor> participantSpec =
            new ParticipantsByUserIdSpecification(report.UserId);

        participantSpec = report.LinkedUsers.Aggregate(participantSpec,
            (current, user) =>
                new OrSpecification<Participant, IParticipantSpecificationVisitor>(current,
                    new ParticipantsByUserIdSpecification(user)));
        userSpec = report.LinkedUsers.Aggregate(userSpec,
            (current, user) => new OrSpecification<User, IUserSpecificationVisitor>(current,
                new UserByIdSpecification(user)));

        var participants = await unitOfWork.ParticipantRepository.Value.FindAsync(participantSpec);
        var chats = await unitOfWork.UserRepository.Value.FindAsync(userSpec);
        return participants.GroupBy(x => x.UserId)
            .Select(x => new ChatParticipants(x.ToList(),
                chats.FirstOrDefault(y => y.Id == x.Key)?.Name ?? throw new LinkedUserNotFoundException(x.Key)));
    }
}