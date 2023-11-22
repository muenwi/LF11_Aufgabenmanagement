namespace TaskApp.Api.Models;

public class TaskModel
{
    public string Title {get; set;}
    public string Description { get; set; }

    public string Role { get; set; }
    public string User { get; set; }

    public DateTime Created { get; set; }

    public int Status {get; set;}
}