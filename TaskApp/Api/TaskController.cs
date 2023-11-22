using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
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
    public void CreateTask(TaskModel newTask)
    {
        try
        {

        }catch (Exception ex)
        {

        }
    }

    [Authorize]
    public async Task<string> GetTasksByUserAsync()
    {
        string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

        try
        {
            // make the request
            var result = await _httpClient.GetAsync("/task/user");


            // successful?
            if (!result.IsSuccessStatusCode)
            {
                return "Das ist ja mal nicht so gut gelaufen :,(";
            }

            // body should contain details about why it failed
            var details = await result.Content.ReadAsStringAsync();
            return details;
            //var problemDetails = JsonDocument.Parse(details);
            //var errors = new List<string>();
            //var errorList = problemDetails.RootElement.GetProperty("errors");

            // foreach (var errorEntry in errorList.EnumerateObject())
            // {
            //     if (errorEntry.Value.ValueKind == JsonValueKind.String)
            //     {
            //         errors.Add(errorEntry.Value.GetString()!);
            //     }
            //     else if (errorEntry.Value.ValueKind == JsonValueKind.Array)
            //     {
            //         errors.AddRange(
            //             errorEntry.Value.EnumerateArray().Select(
            //                 e => e.GetString() ?? string.Empty)
            //             .Where(e => !string.IsNullOrEmpty(e)));
            //     }
            // }

            // return the error list
            // return new FormResult
            // {
            //     Succeeded = false,
            //     ErrorList = problemDetails == null ? defaultDetail : [.. errors]
            // };
        }
        catch
        {
            Console.WriteLine("The http request failed...");
        }

        return "Das ist ja mal nicht so gut gelaufen Rüdigar!";
    }
}