using ToDoWebApp.Api;
using ToDoWebApp.Context;
using ToDoWebApp.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(builder => {
    builder.AddConsole();
});

builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSqlite<ApplicationToDoDbContext>(builder.Configuration.GetConnectionString("ToDo"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapApiRoutes();

app.Run();