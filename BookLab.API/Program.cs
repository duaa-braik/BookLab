using BookLab.API.Extensions;
using BookLab.API.Middlewares;
using BookLab.Application.Configurations;
using BookLab.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookLabDbContext>(
                options => 
                options.UseSqlServer(builder.Configuration["ConnectionString"]));

builder.Services.AddServices();

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddFluentValidation();

builder.Services.ConfigureBadRequestResponse();

builder.Services.ConfigureAppSettings(builder.Configuration);

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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
