using TaskApp.Api.Models;

namespace TaskApp.Api.Interfaces;

public interface ITaskController
{
    public Task<IList<TaskModel>> GetTasksByUserAsync();

    public Task CreateTask(TaskModel newTask);
}
