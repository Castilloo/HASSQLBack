using APITienda.Repository;
using FacturasTienda.Context;

var builder = WebApplication.CreateBuilder(args);
var baseURL = builder.Configuration["Policy:Frontend"];
var connection = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(opt =>
{
    opt.Connection = connection ?? "";
});

builder.Services.AddScoped<ITiendaRepository, TiendaRepository>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowCredentials()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.UseHttpsRedirection();
app.UseCors();

app.MapControllers();

app.Run();

