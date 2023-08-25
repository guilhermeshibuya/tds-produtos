using Products.Data;
using Products.Models;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Adicionar controladores
builder.Services.AddControllers();
// Injeção de dependecia para o contexto da base de dados
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapear controladores da API
app.MapControllers();

app.UseAuthorization();

app.Run();
