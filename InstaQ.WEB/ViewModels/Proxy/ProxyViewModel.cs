namespace InstaQ.WEB.ViewModels.Proxy;

public class ProxyViewModel
{
    public ProxyViewModel(Guid id, string host, int port, string login, string password)
    {
        Id = id;
        Host = host;
        Port = port;
        Login = login;
        Password = password;
    }

    public Guid Id { get; }
    public string Host { get; }
    public int Port { get; }
    public string Login { get; }
    public string Password { get; }
}