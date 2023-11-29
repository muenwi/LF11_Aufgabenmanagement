using System.Text;
using System.Text.Json;
using Blazorise.Extensions;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using TaskApp.Api.Interfaces;
using TaskApp.Api.Models;

namespace TaskApp.Api;

public class TaskController : ITaskController
{
    private readonly ILogger<TaskController> _logger;
    private readonly JsonSerializerOptions jsonSerializerOptions =
    new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    /// <summary>
    /// Special auth client.
    /// </summary>
    private readonly HttpClient _httpClient;

    public TaskController(IHttpClientFactory httpClientFactory, ILogger<TaskController> logger)
    {
        _httpClient = httpClientFactory.CreateClient("Auth");
        _logger = logger;
    }
       

    [Authorize]
    public async Task CreateTask(TaskModel newTask)
    {
        try
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(newTask), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("/task", stringContent);
            return;
        }catch (Exception ex)
        {
            _logger.LogError(ex, "could not create new task");
            return;
        }
    }

    [Authorize]
    public async Task UpdateTask(TaskModel newTask)
    {
        try
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(newTask), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("/task", stringContent);
            return;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "could not update task");
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

            var allTasksList = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(details)?.ToList();

            if (allTasksList.IsNullOrEmpty())
            {
                _logger.LogError("could not find task");
                return new List<TaskModel>();
            }

            _logger.LogInformation("could find task");

            return allTasksList;
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
            var result = await _httpClient.GetAsync("/task/roleOverview");


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
    public async Task<List<UserModel>> GetAllUserAsync()
    {
        string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

        try
        {
            var allUsers = await _httpClient.GetAsync("/user-names");

            // successful?
            if (!allUsers.IsSuccessStatusCode)
            {
                _logger.LogError("could not load all users");
                return new List<UserModel>();
            }
            var usersDetails = await allUsers.Content.ReadAsStringAsync();

            var allUsersList = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(usersDetails);

            if (allUsersList.IsNullOrEmpty())
            {
                _logger.LogError("could not find task");
            }
            return allUsersList?.ToList() ?? new List<UserModel>();
        }catch(Exception ex)
        {
            _logger.LogError(ex, "http request failed");

            Console.WriteLine("The http request failed...");
        }
        return new List<UserModel>();
    }

    [Authorize]
    public async Task<TaskModel> GetTaskToEdit(int taskId)
    {
        string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

        try
        {
            // make the request
            var task = await _httpClient.GetAsync($"/task?taskId={taskId}");

            // successful?
            if (!task.IsSuccessStatusCode)
            {
                _logger.LogError("could not load task");
                return new TaskModel();
            }

            // body should contain details about why it failed
            var taskDetails = await task.Content.ReadAsStringAsync();
            Console.WriteLine(taskDetails);

            var taskToEdit = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(taskDetails)?.FirstOrDefault();

            if (taskToEdit == null)
            {
                _logger.LogError("could not find task");
                return new TaskModel();
            }

            _logger.LogInformation("could find task");

            return taskToEdit;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "http request failed");

            Console.WriteLine("The http request failed...");
        }
        _logger.LogError("kein task");
        return new TaskModel();
    }


    [Authorize]
    public async Task<List<TaskModel>> GetAllTasksAsync()
    {
        string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

        try
        {
            // make the request
            var allTasks = await _httpClient.GetAsync("/all-tasks");

            // successful?
            if (!allTasks.IsSuccessStatusCode)
            {
                _logger.LogError("could not load all tasks");
                return new List<TaskModel>();
            }

            // body should contain details about why it failed
            var tasksDetails = await allTasks.Content.ReadAsStringAsync();
            Console.WriteLine(tasksDetails);

           
            var allTasksList = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(tasksDetails)?.ToList();

            if (allTasksList.IsNullOrEmpty()) {
                _logger.LogError("could not find task");
                return new List<TaskModel>();
            }
           
            _logger.LogInformation("could find task");

            return allTasksList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "http request failed");

            Console.WriteLine("The http request failed...");
        }
        _logger.LogError("leere liste");
        return new List<TaskModel>();
    }
}