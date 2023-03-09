using InstaQ.Domain.Reposts.ParticipantReport.Enums;
using InstaQ.WEB.ViewModels.Elements.Base;

namespace InstaQ.WEB.ViewModels.Elements;

public class ParticipantElementViewModel : ElementViewModel
{
    public ParticipantElementViewModel(string name, string pk, string? newName, ElementType? type,
        IEnumerable<ParticipantElementViewModel> children) : base(name, pk)
    {
        NewName = newName;
        Type = type;
        Children = children.ToList();
    }

    public string? NewName { get; }
    public ElementType? Type { get; }
    public List<ParticipantElementViewModel> Children { get; }
}