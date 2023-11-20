using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorizationBuilder();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite("DataSource=app.db"));

builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

Console.WriteLine(builder.Configuration["BackendUrl"]);

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

var app = builder.Build();

// create routes for the identity endpoints
app.MapIdentityApi<AppUser>();

// provide an end point to clear the cookie for logout
app.MapPost("/Logout", async (ClaimsPrincipal user, SignInManager<AppUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return TypedResults.Ok();
});

// activate the CORS policy
app.UseCors("wasm");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.Run();

class AppUser : IdentityUser {}

class AppDbContext : IdentityDbContext<AppUser> {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
}

