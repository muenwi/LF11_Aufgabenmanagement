using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ServerApp.AppContexts;

internal class AppDbContext : IdentityDbContext<AppUser> {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
}