namespace Eng_Backend.BusinessLayer.Interfaces;

public interface IGenericService<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T> GetByIdAsync(Guid id);

    Task TAddAsync(T entity);

    Task TUpdateAsync(T entity);

    Task TDeleteAsync(Guid id);
}