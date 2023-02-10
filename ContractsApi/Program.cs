using ContractsApi.Interfaces;
using ContractsApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<ConnectionFactory>(provider =>
//new ConnectionFactory(builder.Configuration["SQLiteConnectionString"]));
//builder.Services.AddSingleton<IContracts, SQLiteService>();

builder.Services.AddSingleton<ConnectionFactory>(provider =>
                new ConnectionFactory(builder.Configuration["OracleConnectionString"]));
builder.Services.AddSingleton<IContracts, OracleService>();

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
