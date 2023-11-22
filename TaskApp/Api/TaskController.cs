using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using TaskApp.Api.Interfaces;
using TaskApp.Api.Models;

namespace TaskApp.Api;

public class TaskController : ITaskController
{

    private readonly JsonSerializerOptions jsonSerializerOptions =
    new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    /// <summary>
    /// Special auth client.
    /// </summary>
    private readonly HttpClient _httpClient;

    public TaskController(IHttpClientFactory httpClientFactory)
        => _httpClient = httpClientFactory.CreateClient("Auth");

    [Authorize]
    public async Task CreateTask(TaskModel newTask)
    {
        try
        {
            return;
        }catch (Exception ex)
        {
            return;
        }
    }

    [Authorize]
    public async Task<IList<TaskModel>> GetTasksByUserAsync()
    {
        string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

        try
        {
            // make the request
            var result = await _httpClient.GetAsync("/task/user");


            // successful?
            if (!result.IsSuccessStatusCode)
            {
                return new List<TaskModel>();
            }

            // body should contain details about why it failed
            var details = await result.Content.ReadAsStringAsync();
            var a = JsonConvert.DeserializeObject<IList<TaskModel>>(details);

            return a ?? new List<TaskModel>();
        }
        catch
        {
            Console.WriteLine("The http request failed...");
        }

        return new List<TaskModel>();
    }
}