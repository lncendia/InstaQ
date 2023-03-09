using InstaQ.WEB.ViewModels.Elements.Base;

namespace InstaQ.WEB.ViewModels.Elements;

public class CommentElementsViewModel
{
    public CommentElementsViewModel(IEnumerable<CommentElementViewModel> elements,
        List<PublicationViewModel> publications)
    {
        Elements = elements.OrderByDescending(x => x.IsAccepted);
        Publications = publications;
    }

    public IEnumerable<CommentElementViewModel> Elements { get; }
    public List<PublicationViewModel> Publications { get; }
}

public class CommentElementViewModel : PublicationElementViewModel
{
    public CommentElementViewModel(string name, string pk, string likeChatName, Guid participantId, bool isAccepted,
        bool vip, string? note, IEnumerable<CommentElementViewModel> children, IEnumerable<CommentViewModel> comments) :
        base(name, pk, likeChatName, participantId, isAccepted, vip, note)
    {
        Comments = comments.OrderByDescending(x => x.IsConfirmed).ToList();
        Children = children.OrderByDescending(x => x.IsAccepted).ToList();
    }

    public List<CommentViewModel> Comments { get; }
    public List<CommentElementViewModel> Children { get; }
}

public class CommentViewModel
{
    public CommentViewModel(int publicationId, string? text, bool isConfirmed)
    {
        PublicationId = publicationId;
        Text = text;
        IsConfirmed = isConfirmed;
    }

    public int PublicationId { get; }
    public bool IsConfirmed { get; }
    public string? Text { get; }
}