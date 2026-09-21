using TaskTrackerApp.Repositories;
using TaskTrackerApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITaskRepository, JsonTaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
