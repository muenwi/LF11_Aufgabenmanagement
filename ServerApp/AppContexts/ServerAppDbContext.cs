using Microsoft.EntityFrameworkCore;
using ServerApp.Entities;

namespace ServerApp.AppContexts;

public class ServerAppDbContext : DbContext
{
    public ServerAppDbContext(DbContextOptions<ServerAppDbContext> options) : base(options){}

    public DbSet<EntityTask> Tasks { get; set; }
    public DbSet<EntityTask2Role> Tasks2Roles { get; set; }
}
