﻿using TaskApp.Api.Models;

namespace TaskApp.Api.Interfaces;

public interface ITaskController
{
    public Task<List<TaskModel>> GetTasksByUserAsync();

    public Task CreateTask(TaskModel newTask);

    public Task<List<TaskModel>> GetTasksByRoleAsync();
    public Task<List<TaskModel>> GetAllTasksAsync();
    public Task<GeneralDataModel> GetStatsAsync();
    public Task<List<UserModel>> GetAllUserAsync();
    public Task<TaskModel> GetTaskToEdit(int taskId);
    public Task UpdateTask(TaskModel newTask);
    public Task DeleteTask(TaskModel task);


}
