using Microsoft.EntityFrameworkCore;
using PimMedicationManagement.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURAÇÃO DO BANCO DE DADOS (MYSQL) ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// ----------------------------------------------

// --- CONFIGURAÇÃO DO CORS (PERMITE O VUE.JS ACESSAR A API) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
// -------------------------------------------------------------

builder.Services.AddControllers();

// Configuração do Swagger para .NET 8
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MedFlow API", Version = "v1" });
});

var app = builder.Build();

// Configura o Swagger no pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MedFlow API v1"));
}

app.UseHttpsRedirection();


app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();