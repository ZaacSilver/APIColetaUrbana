using agendamento_coleta_api.dbcontext;
using agendamento_coleta_api.repository;
using agendamento_coleta_api.service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDb"); // Nome do banco em mem�ria
});

builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>(); // Registrar o reposit�rio necess�rio
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>(); // Registrar o servi�o necess�rio

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Your API", Version = "v1" });
});

var app = builder.Build();

// Configurar o pipeline de solicita��o HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
