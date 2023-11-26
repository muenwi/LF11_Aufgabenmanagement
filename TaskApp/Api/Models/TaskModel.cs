namespace TaskApp.Api.Models;

public class TaskModel
{
    public int Id {get; set;} = 0;
    public string Title {get; set;} = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Created { get; set; } = string.Empty;
    public string Status {get; set;} = string.Empty;
}