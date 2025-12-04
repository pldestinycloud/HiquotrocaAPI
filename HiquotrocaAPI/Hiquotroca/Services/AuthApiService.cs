using System.Net.Http.Json;
using Hiquotroca.Dtos;

namespace Hiquotroca.Services;

public class AuthApiService
{
    private readonly HttpClient _http;

    public AuthApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<(bool ok, string message)> RegisterAsync(RegisterRequest dto)
    {
        try
        {
            var resp = await _http.PostAsJsonAsync("api/Auth/register", dto);

            var result = await resp.Content.ReadFromJsonAsync<ApiResponse>();

            return (resp.IsSuccessStatusCode, result?.Message ?? "Erro inesperado.");
        }
        catch
        {
            return (false, "Erro de ligação ao servidor.");
        }
    }

    public async Task<(bool ok, string message)> LoginAsync(LoginRequest dto)
    {
        try
        {
            var resp = await _http.PostAsJsonAsync("api/Auth/login", dto);
            var result = await resp.Content.ReadFromJsonAsync<ApiResponse>();

            return (resp.IsSuccessStatusCode, result?.Message ?? "Erro inesperado.");
        }
        catch
        {
            return (false, "Erro de ligação ao servidor.");
        }
    }
}

public class ApiResponse
{
    public string? Message { get; set; } = string.Empty;
}

