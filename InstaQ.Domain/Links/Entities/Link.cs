using InstaQ.Domain.Abstractions;
using InstaQ.Domain.Links.Exceptions;

namespace InstaQ.Domain.Links.Entities;

public class Link : AggregateRoot
{
    /// <exception cref="SameUsersException"></exception>
    public Link(Guid sender, IReadOnlyCollection<Link> senderLinks, Guid receiver)
    {
        if (sender == receiver) throw new SameUsersException();
        if (senderLinks.Count >= 20) throw new TooManyLinksForUserException(User1Id);
        if (senderLinks.Any(x => x.IsUserLink(receiver)))
            throw new LinkAlreadyExistsException();
        User1Id = sender;
        User2Id = receiver;
    }

    public Guid User1Id { get; }
    public Guid User2Id { get; }
    public bool IsConfirmed { get; private set; }

    public void Confirm(IEnumerable<Link> user2Links)
    {
        if (IsConfirmed) throw new LinkAlreadyConfirmedException();
        var count = user2Links.Count(x => x.Id != Id);
        if (count >= 20) throw new TooManyLinksForUserException(User2Id);
        IsConfirmed = true;
    }

    public bool IsUserLink(Guid userId) => User1Id == userId || User2Id == userId;
}