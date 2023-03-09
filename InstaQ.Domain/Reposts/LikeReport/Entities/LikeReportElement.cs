using InstaQ.Domain.Reposts.LikeReport.Exceptions;
using InstaQ.Domain.Reposts.LikeReport.ValueObjects;
using InstaQ.Domain.Reposts.PublicationReport.Entities;

namespace InstaQ.Domain.Reposts.LikeReport.Entities;

public class LikeReportElement : PublicationReportElement
{
    internal LikeReportElement(string name, string likeChatName, string pk, Guid participantId, bool vip, string? note,
        LikeReportElement? parent, int id) : base(name, likeChatName, pk, participantId, vip, note, parent, id)
    {
    }

    public new LikeReportElement? Parent => base.Parent as LikeReportElement;

    private readonly List<LikeInfo> _likes = new();
    public IReadOnlyCollection<LikeInfo> Likes => _likes.AsReadOnly();

    internal void AddLike(LikeInfo like)
    {
        if (_likes.Any(x => x.PublicationId == like.PublicationId))
            throw new LikeAlreadyExistException();
        _likes.Add(like);
    }
}