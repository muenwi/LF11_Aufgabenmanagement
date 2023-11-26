using ServerApp.Entities;

namespace ServerApp.Dtos;

public class TaskDto
{
    public TaskDto() {}
    public TaskDto(EntityTask entity) {
        Title = entity.Title;
        Description = entity.Description;
        UserId = entity.UserId.ToString();
        Status = entity.Status;
    }

    public string Title {get; set;} = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Status {get; set;} = string.Empty;
}
