using APITienda.Repository;
using FacturasTienda.Context;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var baseURL = builder.Configuration["Policy:Frontend"];
var connection = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = builder.Configuration["Swagger:Title"] ?? "API Tienda",
        Version = builder.Configuration["Swagger:Version"] ?? "v1"
    });
});

builder.Services.Configure<DatabaseSettings>(opt =>
{
    opt.Connection = connection ?? "";
});

builder.Services.AddScoped<ITiendaRepository, TiendaRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy
            .WithOrigins(baseURL!) // asegúrate que no sea null
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

