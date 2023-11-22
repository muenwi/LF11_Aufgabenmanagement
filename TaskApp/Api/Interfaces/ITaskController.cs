using TaskApp.Api.Models;

namespace TaskApp.Api.Interfaces;

public interface ITaskController
{
    public Task<string> GetTasksByUserAsync();

    public void CreateTask(TaskModel newTask);
}
