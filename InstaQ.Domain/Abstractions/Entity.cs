namespace InstaQ.Domain.Abstractions;

public abstract class Entity
{
    protected Entity(int id) => Id = id;
    public int Id { get; }
}