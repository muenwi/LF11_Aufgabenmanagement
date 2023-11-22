using ServerApp.DatabaseStores.Interfaces;
using ServerApp.Entities;

namespace ServerApp.Managers;

public class TaskManager : ITaskManager
{
    private readonly ITaskDatabaseStore _taskStore;
    private readonly ITask2RoleDatabaseStore _task2RoleStore;

    public TaskManager(ITaskDatabaseStore taskStore, ITask2RoleDatabaseStore task2RoleStore)
    {
        _taskStore = taskStore;
        _task2RoleStore = task2RoleStore;
    }

    public async Task<EntityTask> CreateTaskAsync(EntityTask task, CancellationToken cancellationToken = default)
    {
        _ = task ?? throw new ArgumentNullException(nameof(EntityTask));

        await _taskStore.CreateAsync(task);

        return task;
    }

    public async Task<EntityTask> UpdateTaskAsync(EntityTask task, CancellationToken cancellationToken = default)
    {
        _ = task ?? throw new ArgumentNullException(nameof(EntityTask));

        await _taskStore.UpdateAsync(task, cancellationToken);

        return task;
    }

    public async Task DeleteTaskAsync(EntityTask task, CancellationToken cancellationToken = default)
    {
        _ = task ?? throw new ArgumentNullException(nameof(EntityTask));

        await _taskStore.DeleteAsync(task);
    }

    public async Task<EntityTask> GetTaskAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _taskStore.GetTaskAsync(id, cancellationToken);
    }

    public async Task<IList<EntityTask>> GetTaskByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _taskStore.GetTaskByUserAsync(userId, cancellationToken);
    }

    public async Task<IList<EntityTask>> GetTasksAsync(CancellationToken cancellationToken = default)
    {
        return await _taskStore.GetTasksAsync();
    }

    public async Task<IList<EntityTask>> GetTasksByRole(int roleId, CancellationToken cancellationToken = default) {
        var taskIds = await _task2RoleStore.GetTasksByRoleAsync(roleId, cancellationToken);
        
        var tasks = await _taskStore.GetTasksByIdsAsync(taskIds);

        return tasks;
    }

}
