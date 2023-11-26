using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.AppContexts;
using ServerApp.DatabaseStores;
using ServerApp.DatabaseStores.Interfaces;
using ServerApp.Dtos;
using ServerApp.Entities;
using ServerApp.Managers;

var builder = WebApplication.CreateBuilder(args);

// add CORS policy for Wasm client
    
// Add services to the container.
builder.Services.AddAuthorizationBuilder();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite("DataSource=app.db"));
builder.Services.AddDbContext<ServerAppDbContext>(x => x.UseSqlite("DataSource=serverapp.db"));

builder.Services.AddIdentityCore<EntityAppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddCors(options => { options.AddPolicy(
    name: "name",
        policy =>
        policy.WithOrigins("http://localhost:5148", "http://localhost:5297")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

builder.Services.AddScoped<ITaskDatabaseStore, TaskDatabaseStore>();
builder.Services.AddScoped<ITask2RoleDatabaseStore, Task2RoleDatabaseStore>();
builder.Services.AddScoped<ITaskManager, TaskManager>();




var app = builder.Build();

// create routes for the identity endpoints

// provide an end point to clear the cookie for logout
app.MapPost("/Logout", async (ClaimsPrincipal user, SignInManager<EntityAppUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return TypedResults.Ok();
});

app.MapPost("/task", async ([FromBody] TaskDto task,[FromServices] ITaskManager manager, HttpContext context) =>
{
    var entity = new EntityTask {
        Title = task.Title,
        Description = task.Description,
        StartDate = DateTime.Now,
        Status = task.Status,
    };

    EntityTask createdTask;

    if (string.IsNullOrWhiteSpace(task.UserId)) {
        if (string.IsNullOrWhiteSpace(task.Role)) throw new ArgumentNullException("The task is not assigned");

        var roleId = (int)Enum.Parse(typeof(EntityRole.RoleName), task.Role);

        entity.UserId = Guid.Empty;

        createdTask = await manager.CreateTaskAsync(entity);

        await manager.CreateTask2RoleAsync(createdTask.Id, roleId);
    } else {
        entity.UserId = new Guid(task.UserId);
        createdTask = await manager.CreateTaskAsync(entity);
    }

    if (task is null) throw new ArgumentNullException();

    return TypedResults.Ok();
});

app.MapGet("/task/user", async ([FromServices]ITaskManager manager, HttpContext context) =>
{
    var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

    _ = userId ?? throw new ArgumentNullException("The current user has no user id.");

    var tasks = await manager.GetTaskByUserAsync(new Guid(userId));

    if (tasks is null) throw new ArgumentNullException();

    return TypedResults.Json(tasks);
});

app.MapGet("/tasks", async ([FromServices]ITaskManager manager, HttpContext context) =>
{
    var tasks = await manager.GetTasksAsync();

    if (tasks is null) throw new ArgumentNullException();

    return TypedResults.Json(tasks);
});

app.MapDelete("/task", async ([FromServices]ITaskManager manager, HttpContext context) =>
{
    var task = await manager.CreateTaskAsync(new EntityTask{
        Title = "First Task",
        UserId = new Guid("5b39f0ac-0055-4892-a776-bd711d12e70d"),
        StartDate = DateTime.Now,
        Status = "Backlock",
    });

    if (task is null) throw new ArgumentNullException();
    // if (task is null) return TypedResults.NotFound();

    return TypedResults.Ok();
});

app.MapIdentityApi<EntityAppUser>();

app.UseCors("name");

app.UseHttpsRedirection();
app.Run();

