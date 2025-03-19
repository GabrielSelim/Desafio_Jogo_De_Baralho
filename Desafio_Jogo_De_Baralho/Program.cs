using Desafio_Jogo_De_Baralho.Interfaces;
using Desafio_Jogo_De_Baralho.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar Kestrel para escutar na porta 5000
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Jogo De Baralho API V1");
        c.RoutePrefix = string.Empty; // Para acessar o Swagger na raiz
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();