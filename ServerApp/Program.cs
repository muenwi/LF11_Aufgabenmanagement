using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.AppContexts;
using ServerApp.DatabaseStores;
using ServerApp.DatabaseStores.Interfaces;
using ServerApp.Entities;
using ServerApp.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorizationBuilder();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite("DataSource=app.db"));
builder.Services.AddDbContext<ServerAppDbContext>(x => x.UseSqlite("DataSource=serverapp.db"));


builder.Services.AddIdentityCore<EntityAppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

// add CORS policy for Wasm client
builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:5001", 
            builder.Configuration["FrontendUrl"] ?? "https://localhost:5002"])
            .AllowAnyMethod()
            .SetIsOriginAllowed(pol => true)
            .AllowAnyHeader()
            .AllowCredentials()));

builder.Services.AddScoped<ITaskDatabaseStore, TaskDatabaseStore>();
builder.Services.AddScoped<ITask2RoleDatabaseStore, Task2RoleDatabaseStore>();
builder.Services.AddScoped<ITaskManager, TaskManager>();

var app = builder.Build();

// create routes for the identity endpoints
app.MapIdentityApi<EntityAppUser>();

// provide an end point to clear the cookie for logout
app.MapPost("/Logout", async (ClaimsPrincipal user, SignInManager<EntityAppUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return TypedResults.Ok();
});

app.MapPost("/task", async ([FromServices]ITaskManager manager, HttpContext context) =>
{
    var task = await manager.CreateTaskAsync(new EntityTask{
        Title = "First Task",
        UserId = new Guid("5b39f0ac-0055-4892-a776-bd711d12e70d"),
        StartDate = DateTime.Now,
        Status = "Bcaklock",
    });

    if (task is null) throw new ArgumentNullException();
    // if (task is null) return TypedResults.NotFound();

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
    // if (task is null) return TypedResults.NotFound();

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

// app.MapPost("/role", async (ClaimsPrincipal user, IRoleManager manager) => {
//     user.AddIdentity(Role)
// });

// activate the CORS policy
app.UseCors("wasm");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.Run();

