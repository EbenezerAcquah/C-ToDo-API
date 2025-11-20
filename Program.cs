using TodoAPI.AppDataContext;
using TodoAPI.Interface;
using TodoAPI.Middleware;
using TodoAPI.Models;
using TodoAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITodoServices, TodoServices>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));

var dbSettings = builder.Configuration.GetSection("DbSettings").Get<DbSettings>();
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(dbSettings?.ConnectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler(options => { });
app.UseAuthorization();

app.MapControllers();

app.Run();