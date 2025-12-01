using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DAL.Interfaces; // IGenericDal reference

namespace Eng_Backend.BusinessLayer.Managers;

public class GenericManager<T> : IGenericService<T> where T : class
{
    // Renamed to IGenericDal
    private readonly IGenericDal<T> _dal; 

    public GenericManager(IGenericDal<T> dal)
    {
        _dal = dal;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dal.GetAllAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dal.GetByIdAsync(id);
    }

    public async Task TAddAsync(T entity)
    {
        await _dal.AddAsync(entity);
    }

    // CRITICAL FIX HERE:
    public async Task TDeleteAsync(Guid id)
    {
        // 1. First find the entity to be deleted
        var entity = await _dal.GetByIdAsync(id);
        
        // 2. If the entity exists, send it to DAL to delete
        if (entity != null)
        {
            await _dal.DeleteAsync(entity);
        }
    }

    public async Task TUpdateAsync(T entity)
    {
        await _dal.UpdateAsync(entity);
    }
}