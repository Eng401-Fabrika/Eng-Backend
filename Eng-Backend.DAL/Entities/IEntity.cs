namespace Eng_Backend.DAL.Entities;

public interface IEntity<T> where T : class
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}