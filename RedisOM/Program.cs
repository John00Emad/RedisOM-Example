using Bogus;
using Redis.OM;
using RedisOM.HostedService;
using RedisOM.RedisContext;
using RedisOM.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<RedisDbContext>();
builder.Services.AddSingleton<NotificationRepository>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<TripLocationsRepository>();

builder.Services.AddTransient<Faker>();

builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration["RedisConnectionString"]));
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration["RedisConnectionMultiplexerString"]));
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
