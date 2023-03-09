namespace InstaQ.Domain.Users.ValueObjects;

public class Vk
{
    internal Vk(string name, string accessToken)
    {
        Name = name;
        AccessToken = accessToken;
    }

    public string Name { get; }
    public string AccessToken { get; }
    
}