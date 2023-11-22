using Microsoft.EntityFrameworkCore;
using ServerApp.AppContexts;
using ServerApp.DatabaseStores.Interfaces;
using ServerApp.Entities;

namespace ServerApp.DatabaseStores;

public class TaskDatabaseStore : ITaskDatabaseStore
{
    private readonly ServerAppDbContext _context;

    private DbSet<EntityTask> tasks => _context.Tasks;

    public TaskDatabaseStore(ServerAppDbContext context) 
    {
        _context = context;
    }

    public async Task CreateAsync(EntityTask entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await tasks.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EntityTask entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        tasks.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EntityTask entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        tasks.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<EntityTask> GetTaskAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var task = await tasks.FirstOrDefaultAsync(x => x.Id == id);

        if (task is null) throw new ArgumentException("Task was null");

        return task;   
    }

    public async Task<IList<EntityTask>> GetTasksByIdsAsync(IList<int> ids, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var taskList = await tasks.Where(x => ids.Contains(x.Id)).ToListAsync();

        if (tasks is null) throw new ArgumentException("Task was null");

        return taskList;   
    }

    public async Task<IList<EntityTask>> GetTasksAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await tasks.ToListAsync();
    }

    public async Task<IList<EntityTask>> GetTaskByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var taskList = await tasks
            .Where(x => x.UserId.Equals(userId))
            .ToListAsync();

        return taskList;
    }
}
