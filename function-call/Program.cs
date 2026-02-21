using System.ClientModel;
using function_call.Functions;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;

IConfiguration configuration = new ConfigurationBuilder()
                                            .AddUserSecrets<Program>()
                                            .Build();
var apiToken = configuration
                .GetRequiredSection("GithubModels:Token").Value!;
var credential = new ApiKeyCredential(apiToken);
var endpoint_url = "https://models.github.ai/inference";

//var availableModels = await ListModelsAsync(endpoint_url,apiToken);
var options = new OpenAIClientOptions
{
    Endpoint = new Uri(endpoint_url)
};
var model = "openai/gpt-4o-mini";

IChatClient client = new ChatClientBuilder(new OpenAIClient(credential, options)
                                .GetChatClient(model)
                                .AsIChatClient())
                                .UseFunctionInvocation()
                                .Build();
var httpClient = new HttpClient();                            
var weather = new WeatherService(httpClient, configuration);
var chatOptions = new ChatOptions
{
    Tools = [AIFunctionFactory.Create( async (string location, string unit) =>
    {
        // Here you would call a weather API to get the weather for the location
        var result = await weather.GetCurrentWeatherAsync(location);

        return $"Currently in {location}, it is {result.Current.Temp_c}°C ({result.Current.Temp_f}°F) with {result.Current.Condition.Text}. " +
                $"Humidity is at {result.Current.Humidity}%.";
    },
    "get_current_weather", // name of the function
    "Get the current weather in a given location")] //description of the function
};


List<ChatMessage> chatHistory = [new(ChatRole.System, """
    You are a hiking enthusiast who helps people discover fun hikes in their area. 
    You are upbeat and friendly.
    """)];

// Weather conversation relevant to the registered function.
chatHistory.Add(new(ChatRole.User, """
    I live in Istanbul and I'm looking for a moderate intensity hike. 
    What's the current weather like? 
    """));
Console.WriteLine($"{chatHistory.Last().Role} >> {chatHistory.Last()}");

var response = await client.GetResponseAsync(chatHistory, chatOptions);

chatHistory.Add(new(ChatRole.Assistant, response.Text));
Console.ForegroundColor = ConsoleColor.Blue;
Console.Write($"{chatHistory.Last().Role.ToString().ToUpper()} >> ");
Console.ResetColor();
Console.WriteLine($"{chatHistory.Last()}");
Console.ReadKey();
