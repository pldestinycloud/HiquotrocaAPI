using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Hiquotroca.API.Infrastructure.Email;
using Hiquotroca.API.Infrastructure.Persistence;
using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Infrastructure;
using Hiquotroca.API.Application;
using Hiquotroca.API.Application.Interfaces;


var builder = WebApplication.CreateBuilder(args);

var useInMemoryDatabase = true;
builder.Services.AddInfrastructureServices(builder.Configuration, useInMemoryDatabase);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
