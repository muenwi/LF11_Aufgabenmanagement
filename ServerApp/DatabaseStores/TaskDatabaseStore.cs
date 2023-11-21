using Microsoft.EntityFrameworkCore;
using ServerApp.AppContexts;
using ServerApp.DatabaseStores.Interfaces;
using ServerApp.Entities;

namespace ServerApp.DatabaseStores;

public class TaskDatabaseStore : ITaskDatabaseStore
{
    private readonly ServerAppDbContext _context;

    private DbSet<EntityTask> tasks => _context.Tasks;
    private DbSet<EntityTask2Role> tasks2Role => _context.Tasks2Roles;

    public TaskDatabaseStore(ServerAppDbContext context) 
    {
        _context = context;
    }

    public async Task CreateAsync(EntityTask entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await tasks.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(EntityTask entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        throw new NotImplementedException();
    }

    public Task DeleteAsync(EntityTask entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        throw new NotImplementedException();
    }

    public Task<EntityTask> GetTaskAsync(int id)
    {
        throw new NotImplementedException();
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
