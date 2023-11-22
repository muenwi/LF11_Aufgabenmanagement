using ServerApp.Entities;

namespace ServerApp.Managers;

public interface ITaskManager
{
    public Task<EntityTask> CreateTaskAsync(EntityTask task, CancellationToken cancellationToken = default);
    public Task<EntityTask> UpdateTaskAsync(EntityTask task, CancellationToken cancellationToken = default);
    public Task DeleteTaskAsync(EntityTask task, CancellationToken cancellationToken = default);
    public Task<EntityTask> GetTaskAsync(int id, CancellationToken cancellationToken = default);
    public Task<IList<EntityTask>> GetTasksAsync(CancellationToken cancellationToken = default);
    public Task<IList<EntityTask>> GetTaskByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
