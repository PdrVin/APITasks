using APITasks.DTO;

namespace APITasks.Interfaces.Base;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(int page);
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(int id, TaskDto entity);
    Task DeleteAsync(int id);
}
