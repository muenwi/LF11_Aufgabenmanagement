using TaskApp.Api.Models;

namespace TaskApp.Api.Interfaces;

public interface ITaskController
{
    public Task<List<TaskModel>> GetTasksByUserAsync();

    public Task CreateTask(TaskModel newTask);
}
