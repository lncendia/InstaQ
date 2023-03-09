using InstaQ.Application.Abstractions.Links.ServicesInterfaces;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Links.Entities;
using InstaQ.Domain.Links.Specification;
using InstaQ.Domain.Links.Specification.Visitor;
using InstaQ.Domain.Specifications;
using InstaQ.Domain.Specifications.Abstractions;
using InstaQ.Domain.Users.Specification;

namespace InstaQ.Application.Services.Links;

public class UserLinksService : IUserLinksService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserLinksService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<List<(Guid id, string name)>> GetUserLinksAsync(Guid userId)
    {
        ISpecification<Link, ILinkSpecificationVisitor> linksSpec = new LinkByUserIdSpecification(userId);
        linksSpec = new AndSpecification<Link, ILinkSpecificationVisitor>(linksSpec, new AcceptedLinkSpecification());
        var links = await _unitOfWork.LinkRepository.Value.FindAsync(linksSpec);
        var usersIds = links.Select(x => x.User1Id == userId ? x.User2Id : x.User1Id).ToList();
        var usersSpec = new UserByIdsSpecification(usersIds);
        var users = await _unitOfWork.UserRepository.Value.FindAsync(usersSpec);
        return users.Select(x => (x.Id, x.Name)).ToList();
    }
}