using ServerApp.DatabaseStores.Interfaces;
using ServerApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ServerApp.Managers;

public class TaskManager : ITaskManager
{
    private readonly ITaskDatabaseStore _taskStore;
    private readonly ITask2RoleDatabaseStore _task2RoleStore;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<EntityAppUser> _userManager;

    public TaskManager(ITaskDatabaseStore taskStore, ITask2RoleDatabaseStore task2RoleStore,
        UserManager<EntityAppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _taskStore = taskStore;
        _task2RoleStore = task2RoleStore;
        _userManager = userManager;
        _roleManager = roleManager; 
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

    public async Task<IList<EntityTask>> GetTaskByUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _taskStore.GetTaskByUserAsync(userId, cancellationToken);
    }

    public async Task<IList<EntityTask>> GetTasksAsync(CancellationToken cancellationToken = default)
    {
        var tasks = await _taskStore.GetTasksAsync();
        var users = await _userManager.Users.ToListAsync();

        foreach (var task in tasks)
        {
            var user = users.SingleOrDefault(x => x.Id == task.UserId);
            task.UserId = user?.UserName ?? string.Empty;

        }

        return tasks;
    }

    public async Task<IList<EntityTask>> GetTasksByRole(string roleId, CancellationToken cancellationToken = default) {
        var taskIds = await _task2RoleStore.GetTasksByRoleAsync(roleId, cancellationToken);
        
        var tasks = await _taskStore.GetTasksByIdsAsync(taskIds);

        return tasks;
    }

    public async Task CreateTask2RoleAsync(int taskId, string roleId, CancellationToken cancellationToken = default)
    {
        var entity = new EntityTask2Role {
            RoleId = roleId,
            TaskId = taskId,
        };

        await _task2RoleStore.CreateAsync(entity);

    }

    public Task<EntityTask2Role> UpdateTask2RoleAsync(int taskId, string roleId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTask2RoleAsync(EntityTask task, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    Task ITaskManager.UpdateTask2RoleAsync(int taskId, string roleId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<EntityTask>> GetTasksByRoleAsync(string roleId, CancellationToken cancellationToken = default)
    {
        var taskIds = await _task2RoleStore.GetTasksByRoleAsync(roleId, cancellationToken);

        var tasks = await _taskStore.GetTasksByIdsAsync(taskIds, cancellationToken);

        return tasks;
    }
}
