using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerApp.Entities;

namespace ServerApp.AppContexts;

internal class AppDbContext : IdentityDbContext<EntityAppUser> {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
}