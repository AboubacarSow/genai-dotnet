using System.Net.Http.Json;
using function_call.Models;
using Microsoft.Extensions.Configuration;

namespace function_call.Functions;

public interface IWeatherService
{
    
    Task<WeatherModel> GetCurrentWeatherAsync(string location);
}

public class WeatherService(HttpClient httpClient, IConfiguration configuration) : IWeatherService
{
    private readonly string  api_url ="http://api.weatherapi.com/v1/current.json?";
    public async Task<WeatherModel> GetCurrentWeatherAsync(string location)
    {
        var apiKey = configuration["Weather:API_KEY"];

        var response = await httpClient.GetFromJsonAsync<WeatherModel>($"{api_url}key={apiKey}&q={location}");
        
        return response!;
    }
}