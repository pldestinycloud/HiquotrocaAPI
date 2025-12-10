using System.Net;
using System.Text.Json;
using Hiquotroca.API.Application.Wrappers;

namespace Hiquotroca.API.Presentation.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Passa o controlo para o próximo middleware no pipeline
                // É aqui que o pedido vai para os Controllers, Services, etc.
                await _next(context);
            }
            catch (Exception ex)
            {
                // Se acontecer uma exceção não tratada em qualquer lugar abaixo, ela sobe até aqui
                _logger.LogError(ex, "Ocorreu um erro inesperado no servidor.");

                // Tratamos a resposta para devolver um JSON amigável
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Criar uma resposta padronizada
            // Estamos a usar a tua estrutura BaseResult/Error para consistência
            var response = BaseResult.Failure(new Error(ErrorCode.Exception, "Ocorreu um erro interno no servidor. Por favor contacte o suporte."));

            // Serializar para JSON (camelCase é o padrão em APIs)
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, jsonOptions);

            return context.Response.WriteAsync(json);
        }
    }
}