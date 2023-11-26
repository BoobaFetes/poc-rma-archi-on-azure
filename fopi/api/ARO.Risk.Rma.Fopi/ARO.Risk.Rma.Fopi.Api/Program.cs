using ARO.Risk.Rma.Fopi.Api;
using ARO.Risk.Rma.Fopi.Api.Controllers.Fopi;
using ARO.Risk.Rma.Fopi.Application.Fopi;
using ARO.Risk.Rma.Fopi.Application.Infrastructure;
using ARO.Risk.Rma.Fopi.Infra.Database.Fake;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// bootstrap our features
builder.Services.AddApplicationDependencies();
builder.Services.AddInfraDependencies();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(); // see appsettings.json for policies

app.MapControllers();

app.Run();
