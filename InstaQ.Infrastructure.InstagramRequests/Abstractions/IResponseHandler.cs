namespace InstaQ.Infrastructure.InstagramRequests.Abstractions;

public interface IResponseHandler<out T>
{
    public T MapResponse(string data);
}