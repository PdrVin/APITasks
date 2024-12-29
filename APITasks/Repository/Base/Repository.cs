using Microsoft.EntityFrameworkCore;
using APITasks.Database;
using APITasks.Interfaces.Base;
using APITasks.Models.Errors;
using APITasks.DTO;

namespace APITasks.Repository.Base;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly TaskContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(TaskContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(int page)
    {
        page = (page < 1) ? 1 : page;
        int limit = 10;
        int offset = (page - 1) * limit;
        return await _dbSet.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var task = await _dbSet.FindAsync(id);
        if (task == null)
            throw new TaskError("Tarefa Não Encontrada!");
        return task;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(int id, TaskDto taskDto)
    {
        var existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity == null)
            throw new TaskError($"Entity with id {id} not found.");

        _context.Entry(existingEntity).CurrentValues.SetValues(taskDto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            throw new TaskError("Tarefa Não Encontrada!");

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
