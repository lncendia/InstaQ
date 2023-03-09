using InstaQ.Application.Abstractions.Links.DTOs;
using InstaQ.Application.Abstractions.Links.Exceptions;
using InstaQ.Application.Abstractions.Links.ServicesInterfaces;
using InstaQ.Application.Abstractions.Users.Exceptions;
using InstaQ.Domain.Abstractions.UnitOfWorks;
using InstaQ.Domain.Links.Entities;
using InstaQ.Domain.Links.Specification;
using InstaQ.Domain.Users.Specification;

namespace InstaQ.Application.Services.Links;

public class LinkManager : ILinkManager
{
    private readonly IUnitOfWork _unitOfWork;

    public LinkManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddLinkDto> AddAsync(Guid user1, string email)
    {
        var user = (await _unitOfWork.UserRepository.Value.FindAsync(new UserByEmailSpecification(email), null, 0, 1))
            .FirstOrDefault();
        if (user == null) throw new UserNotFoundException();
        var links = await _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdSpecification(user1));
        var link = new Link(user1, links, user.Id);
        await _unitOfWork.LinkRepository.Value.AddAsync(link);
        await _unitOfWork.SaveChangesAsync();
        return new AddLinkDto(link.Id, user.Name);
    }

    public async Task DeleteAsync(Guid user, Guid linkId)
    {
        var link = await _unitOfWork.LinkRepository.Value.GetAsync(linkId);
        if (link == null || !link.IsUserLink(user)) throw new LinkNotFoundException();
        await _unitOfWork.LinkRepository.Value.DeleteAsync(linkId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AcceptAsync(Guid user, Guid linkId)
    {
        var links = await _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdSpecification(user));
        var link = links.FirstOrDefault(x => x.Id == linkId);
        if (link == null || link.User2Id != user) throw new LinkNotFoundException();
        link.Confirm(links);
        await _unitOfWork.LinkRepository.Value.UpdateAsync(link);
        await _unitOfWork.SaveChangesAsync();
    }
}