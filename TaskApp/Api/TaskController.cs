using System.Text;
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
            var stringContent = new StringContent(JsonConvert.SerializeObject(newTask), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("/task", stringContent);
            return;
        }catch (Exception)
        {
            return;
        }
    }

    [Authorize]
    public async Task<List<TaskModel>> GetTasksByUserAsync()
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

            Console.WriteLine(details);
            var a = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(details)?.ToList();

            return a ?? new List<TaskModel>();
        }
        catch
        {
            Console.WriteLine("The http request failed...");
        }

        return new List<TaskModel>();
    }

    [Authorize]
    public async Task<List<TaskModel>> GetTasksByRoleAsync()
    {
        string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

        try
        {
            // make the request
            var result = await _httpClient.GetAsync("/task/role");


            // successful?
            if (!result.IsSuccessStatusCode)
            {
                return new List<TaskModel>();
            }

            // body should contain details about why it failed
            var details = await result.Content.ReadAsStringAsync();

            Console.WriteLine(details);
            var a = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(details)?.ToList();

            return a ?? new List<TaskModel>();
        }
        catch
        {
            Console.WriteLine("The http request failed...");
        }

        return new List<TaskModel>();
    }

    [Authorize]
    public async Task<GeneralDataModel> GetStatsAsync()
    {
        string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

        try
        {
            // make the request
            var result = await _httpClient.GetAsync("/general-data");


            // successful?
            if (!result.IsSuccessStatusCode)
            {
                return new GeneralDataModel();
            }

            // body should contain details about why it failed
            var details = await result.Content.ReadAsStringAsync();

            Console.WriteLine(details);
            var a = JsonConvert.DeserializeObject<GeneralDataModel>(details);

            return a ?? new GeneralDataModel();
        }
        catch
        {
            Console.WriteLine("The http request failed...");
        }

        return new GeneralDataModel(); ;
    }

    [Authorize]
    public async Task<List<TaskModel>> GetAllTasksAsync()
    {
        string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

        try
        {
            // make the request
            var result = await _httpClient.GetAsync("/all-tasks");


            // successful?
            if (!result.IsSuccessStatusCode)
            {
                return new List<TaskModel>();
            }

            // body should contain details about why it failed
            var details = await result.Content.ReadAsStringAsync();

            Console.WriteLine(details);
            var a = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(details)?.ToList();

            return a ?? new List<TaskModel>();
        }
        catch
        {
            Console.WriteLine("The http request failed...");
        }

        return new List<TaskModel>();
    }
}