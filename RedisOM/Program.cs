using Redis.OM;
using RedisOM.HostedService;
using RedisOM.RedisContext;
using RedisOM.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<RedisDBContext>();
builder.Services.AddSingleton<NotificationRepository>();

builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration["RedisConnectionString"]));
builder.Services.AddHostedService<IndexCreationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
