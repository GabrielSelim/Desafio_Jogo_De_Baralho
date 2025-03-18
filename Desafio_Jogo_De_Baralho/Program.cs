using Desafio_Jogo_De_Baralho.Interfaces;
using Desafio_Jogo_De_Baralho.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HttpClient and services
builder.Services.AddHttpClient<IClienteAPIService, ClienteApiService>();
builder.Services.AddScoped<IJogoService, JogoServico>();

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