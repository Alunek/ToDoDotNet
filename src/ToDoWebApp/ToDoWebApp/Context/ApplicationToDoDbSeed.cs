using Microsoft.EntityFrameworkCore;

using ToDoWebApp.Domain;

namespace ToDoWebApp.Context;


internal static class ApplicationToDoDbSeed
{
    public static void ApplicationSeed(this ModelBuilder modelBuilder)
    {
        CreatePriorities(modelBuilder);
    }


    private static void CreatePriorities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Priority>().HasData(PriorityList());
    }

    private static List<Priority> PriorityList()
    {
        return new()
        {
            new() { Level = 1 },
            new() { Level = 2 },
            new() { Level = 3 },
            new() { Level = 4 },
            new() { Level = 5 },
        };
    }
}