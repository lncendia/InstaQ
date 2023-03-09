using InstaQ.Domain.Abstractions;
using InstaQ.Domain.Participants.Exceptions;

namespace InstaQ.Domain.Participants.Entities;

public class Participant : AggregateRoot
{
    public Participant(Guid userId, string name, string pk, IReadOnlyCollection<Participant> allUsersParticipants)
    {
        if (allUsersParticipants.Any(x => x.UserId != userId))
            throw new ArgumentException(null, nameof(allUsersParticipants));
        if (allUsersParticipants.Count >= 1000) throw new TooManyParticipantsException();
        var anotherParticipant = allUsersParticipants.FirstOrDefault(x => x.Pk == pk);
        if (anotherParticipant != null) throw new ParticipantAlreadyExistsException(anotherParticipant.Pk);
        UserId = userId;
        Name = name;
        Pk = pk;
    }


    public Guid UserId { get; }
    public string Name { get; private set; }
    public string? Notes { get; private set; }
    public string Pk { get; }
    public Guid? ParentParticipantId { get; private set; }
    public bool Vip { get; private set; }

    public void UpdateName(string name) => Name = name;

    /// <exception cref="ChildException"></exception>
    public void SetParent(Participant parent, IEnumerable<Participant> parentChildren)
    {
        if (parent.UserId != UserId)
            throw new ArgumentException("Parent must be from the same user", nameof(parent));
        if (parent.ParentParticipantId.HasValue || parentChildren.Any()) throw new ChildException();
        ParentParticipantId = parent.Id;
    }

    public void DeleteParent() => ParentParticipantId = null;

    public void SetNotes(string notes)
    {
        if (notes.Length > 500) throw new ArgumentException("Notes length must be less than 500");
        Notes = notes;
    }

    public void DeleteNotes() => Notes = null;

    public void SetVip(bool vip) => Vip = vip;
}