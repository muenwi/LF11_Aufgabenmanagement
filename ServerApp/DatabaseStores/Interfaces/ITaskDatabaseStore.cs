using ServerApp.Entities;

namespace ServerApp.DatabaseStores.Interfaces;

public interface ITaskDatabaseStore
{
    public Task CreateAsync(EntityTask entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(EntityTask entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(EntityTask entity, CancellationToken cancellationToken = default);
    public Task<EntityTask> GetTaskAsync(int id, CancellationToken cancellationToken = default);
    public Task<IList<EntityTask>> GetTasksAsync(CancellationToken cancellationToken = default);
    public Task<IList<EntityTask>> GetTaskByUserAsync(string userId, CancellationToken cancellationToken = default);
    public Task<IList<EntityTask>> GetTasksByIdsAsync(IList<int> ids, CancellationToken cancellationToken = default);
}
