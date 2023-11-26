using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServerApp.AppContexts;
using ServerApp.DatabaseStores;
using ServerApp.DatabaseStores.Interfaces;
using ServerApp.Entities;
using ServerApp.Extension;
using ServerApp.Managers;

var builder = WebApplication.CreateBuilder(args);

// add CORS policy for Wasm client
    
// Add services to the container.
builder.Services.AddAuthorizationBuilder();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite("DataSource=app.db"));
builder.Services.AddDbContext<ServerAppDbContext>(x => x.UseSqlite("DataSource=serverapp.db"));

builder.Services.AddIdentityCore<EntityAppUser>()
    .AddRoles<IdentityRole>()
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

app.AddPostMethodes();
app.AddGetMethodes();
app.AddDeleteMethodes();


app.MapIdentityApi<EntityAppUser>();

app.UseCors("name");

app.UseHttpsRedirection();
app.Run();

