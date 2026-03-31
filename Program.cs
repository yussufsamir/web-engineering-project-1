using fitness_tracker.Services;
using Microsoft.EntityFrameworkCore;
using fitness_tracker.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IAthleteService, AthleteService>();
builder.Services.AddScoped<IWodService, WodService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

app.MapFallback(context =>
{
    context.Response.StatusCode = 404;
    return context.Response.WriteAsync("Page Not Found");
});



app.MapControllers();

app.Run();