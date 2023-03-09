namespace InstaQ.WEB.ViewModels.Participants;

public class ParticipantViewModel
{
    public ParticipantViewModel(Guid id, string name, string? note, bool vip, IEnumerable<ParticipantViewModel>? children)
    {
        Id = id;
        Name = name;
        Note = note?[..Math.Min(note.Length, 50)];
        Vip = vip;
        if (children != null) Children.AddRange(children);
    }

    public Guid Id { get; }
    public string Name { get; }
    public string? Note { get; }
    public bool Vip { get; }

    public List<ParticipantViewModel> Children { get; } = new();
}