using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UsersService.Models;

namespace UsersService;

public partial class UsersdbContext : DbContext
{
    protected readonly IConfiguration Configuration;
    public DbSet<User> Users { get; set; } = null!;
    public UsersdbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
    }
}
