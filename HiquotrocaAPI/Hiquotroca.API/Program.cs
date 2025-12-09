using Hiquotroca.API.Infrastructure;
using Hiquotroca.API.Application;
using Hiquotroca.API.Presentation;
using Hiquotroca.API.Presentation.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

var useInMemoryDatabase = true;
builder.Services.AddInfrastructureServices(builder.Configuration, useInMemoryDatabase);
builder.Services.AddApplicationServices();
builder.Services.AddSignalR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();


app.UseAuthentication();
app.UseAuthorization();

//Aggregate roots endpoints
app.MapControllers();
app.MapHub<ChatHub>("/chat").AllowAnonymous();

//LoopUp entitities endpoints
app.MapLookUpEntitiesEndpoints();
//Nota Critica: Utilizar minimal api endpoints e controllers normais pode criar confusão na organização do projeto.
//No entanto, como o ojectivo é apenas retornar as coleções das lookup entities, é mais rápido e simples do que criar toda a estrtutura de controllers, services, etc.
//Não obstante, deve ser alterada no futuro uma vez que se se chega a conclusões mais definitivas sobre a arquitetura da aplicação.

app.Run();
