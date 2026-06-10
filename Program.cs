using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Aquí mapeamos la conexión con la instancia exacta de tu máquina
builder.Services.AddDbContext<AcademiaMusicaAPI.Models.AcademiasApiDbContext>(options =>
    options.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=AcademiasApiDB;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseDefaultFiles(); // Para que busque automáticamente el index.html
app.UseStaticFiles();  // Para permitir mostrar páginas web locales
app.MapControllers(); // Habilita la ruta de los controladores

app.Run();