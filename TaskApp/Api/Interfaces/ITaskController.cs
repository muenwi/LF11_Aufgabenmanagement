namespace TaskApp.Api.Interfaces;

public interface ITaskController
{
        public Task<string> GetTasksByUserAsync();
}
