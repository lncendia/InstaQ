using InstaQ.Domain.Users.Enums;

namespace InstaQ.WEB.ViewModels.Profile;

public class TargetViewModel
{
    public TargetViewModel(string targetName, ParticipantsType type)
    {
        TargetName = targetName;
        Type = type;
    }

    public string TargetName { get; }
    public ParticipantsType Type { get; }
}