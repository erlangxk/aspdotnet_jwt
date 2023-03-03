using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Slot.Api.Services;

public class UserContext:IdentityUserContext<IdentityUser>
{

    private readonly string _connectionString;

    public UserContext(DbContextOptions<UserContext> options, IConfiguration configuration) : base(options)
    {
        _connectionString = configuration["DB:ConnStr"]!;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
    
}