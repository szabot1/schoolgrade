using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using schoolgrade.Data;
using schoolgrade.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SchoolContext>(options =>
{
    options.UseMySql(
        "server=localhost;database=fz_schoolgrade;user=fz_schoolgrade;password=asd123",
        MySqlServerVersion.LatestSupportedServerVersion
    );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
{
    using var ctx = serviceScope.ServiceProvider.GetService<SchoolContext>();
    ctx!.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
