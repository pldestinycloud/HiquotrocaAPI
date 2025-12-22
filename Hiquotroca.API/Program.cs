using Hiquotroca.API.Infrastructure;
using Hiquotroca.API.Application;
using Hiquotroca.API.Presentation.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Hiquotroca.API.Presentation.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting up the api at {Time}", DateTime.UtcNow);

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration.ReadFrom.Configuration(context.Configuration);
        configuration.Enrich.FromLogContext();
    });

    var useInMemoryDatabase = false;
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

    //if (app.Environment.IsDevelopment())
    //{
        app.UseSwagger();
        app.UseSwaggerUI();
    //}

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();

    app.UseCors();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

    //Aggregate roots endpoints
    app.MapControllers();
    app.MapHub<ChatHub>("/chat")
       .RequireCors("OpenPolicy")
       .RequireAuthorization();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
