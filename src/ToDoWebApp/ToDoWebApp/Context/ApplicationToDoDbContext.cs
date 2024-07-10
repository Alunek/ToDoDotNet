using Microsoft.EntityFrameworkCore;

using ToDoWebApp.Domain;

namespace ToDoWebApp.Context;


internal class ApplicationToDoDbContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
    public DbSet<Priority> Priorities { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;


    public ApplicationToDoDbContext(DbContextOptions<ApplicationToDoDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplicationSeed();
    }
}