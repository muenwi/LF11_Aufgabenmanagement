using ServerApp.Entities;

namespace ServerApp.Managers;

public interface ITaskManager
{
    public Task<EntityTask> CreateTaskAsync(EntityTask task, CancellationToken cancellationToken = default);
    public Task<EntityTask> UpdateTaskAsync(EntityTask task, CancellationToken cancellationToken = default);
    public Task DeleteTaskAsync(EntityTask task, CancellationToken cancellationToken = default);
    public Task CreateTask2RoleAsync(int taskId, string roleId, CancellationToken cancellationToken = default);
    public Task UpdateTask2RoleAsync(int taskId, string roleId, CancellationToken cancellationToken = default);
    public Task DeleteTask2RoleAsync(EntityTask task, CancellationToken cancellationToken = default);
    public Task<EntityTask> GetTaskAsync(int id, CancellationToken cancellationToken = default);
    public Task<IList<EntityTask>> GetTasksAsync(CancellationToken cancellationToken = default);
    public Task<IList<EntityTask>> GetTaskByUserAsync(string userId, CancellationToken cancellationToken = default);
    public Task<IList<EntityTask2Role>> GetRole2Tasks(CancellationToken cancellationToken = default);
    public Task<IList<EntityTask>> GetTasksByRoleAsync(string roleId, CancellationToken cancellationToken = default);
}
