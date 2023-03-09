namespace InstaQ.Domain.Users.Exceptions;

public class VkIsNotSetException:Exception
{
    public VkIsNotSetException():base("Vk is not set")
    {
    }
}