namespace InstaQ.Domain.Users.Exceptions;

public class TargetChangeException : Exception
{
    public TimeSpan RestTime { get; }

    public TargetChangeException(TimeSpan restTime) : base("You can't change the target more than once a day") =>
        RestTime = restTime;
}