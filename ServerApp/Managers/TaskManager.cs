using ServerApp.DatabaseStores.Interfaces;
using ServerApp.Entities;

namespace ServerApp.Managers;

public class TaskManager : ITaskManager
{
    private readonly ITaskDatabaseStore _taskStore;

    public TaskManager(ITaskDatabaseStore taskStore)
    {
        _taskStore = taskStore;
    }

    public async Task<EntityTask> CreateTaskAsync(EntityTask task, CancellationToken cancellationToken = default)
    {
        _ = task ?? throw new ArgumentNullException(nameof(EntityTask));

        await _taskStore.CreateAsync(task);

        return task;
    }

    public Task<EntityTask> UpdateTaskAsync(EntityTask task, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<EntityTask> DeleteTaskAsync(EntityTask task, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<EntityTask> GetTaskAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<EntityTask>> GetTaskByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _taskStore.GetTaskByUserAsync(userId);
    }

    public async Task<IList<EntityTask>> GetTasksAsync(CancellationToken cancellationToken = default)
    {
        return await _taskStore.GetTasksAsync();
    }

}
