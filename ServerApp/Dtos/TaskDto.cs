﻿using ServerApp.Entities;

namespace ServerApp.Dtos;

public class TaskDto
{
    public TaskDto() {}
    public TaskDto(EntityTask entity) {
        Id = entity.Id;
        Title = entity.Title;
        Description = entity.Description;
        UserId = entity.UserId.ToString();
        Status = entity.Status;
    }

    public int Id {get; set;} = 0;
    public string Title {get; set;} = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Status {get; set;} = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public DateTime Created => StartDate == null ? DateTime.Now : DateTime.Parse(StartDate);


}
