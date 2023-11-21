using ServerApp.Entities;

namespace ServerApp.DatabaseStores.Interfaces;

public interface ITaskDatabaseStore
{
    public Task CreateAsync(EntityTask entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(EntityTask entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(EntityTask entity, CancellationToken cancellationToken = default);
    public Task<EntityTask> GetTaskAsync(int id);
    public Task<IList<EntityTask>> GetTasksAsync(CancellationToken cancellationToken = default);
    public Task<IList<EntityTask>> GetTaskByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
