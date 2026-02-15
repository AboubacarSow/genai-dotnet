using System.ClientModel;
using System.Drawing;
using System.Net.Http.Headers;
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

IChatClient client = new OpenAIClient(credential, options)
                                .GetChatClient(model)
                                .AsIChatClient();

#region  Basic completion
//var prompt = "What is Semantic Kernal? Explain it in 20 words max";
//Console.WriteLine($"user input : >> {prompt}");
//var response = await chatClient.GetResponseAsync(prompt);

//Console.WriteLine($"Assistant AI: >> {response}");

//Console.WriteLine($"Token used: in={response.Usage?.InputTokenCount} - out={response.Usage?.OutputTokenCount}");

#endregion

#region Stream Completion
// var prompt = "What is Semantic Kernal? Explain it in 200 words max";
// Console.WriteLine($"user input : >> {prompt}");
// Console.WriteLine();
// var response = chatClient.GetStreamingResponseAsync(prompt);
// Console.ForegroundColor = ConsoleColor.Blue;
// Console.Title="Chat Completion";
// Console.Write($"Assistant AI: >> ");
// await foreach(var chunck in response)
// {
//     Console.Write(chunck);
// }
// Console.WriteLine();
// Console.ResetColor();
// Console.ReadLine();
#endregion

#region  Classification
// var prompt = """
// Please classify the following sentences into categories :
// - 'complaint'
// - 'suggest'
// - 'praise'
// - 'other'

// 1) "I love the new layout!".
// 2) "You should add the night mode."
// 3) "When I try the log in, it keeps failing."
// 4) "The application is decent."
// """;
// Console.WriteLine($"user input : >> {prompt}");
// var response = await client.GetResponseAsync(prompt);
// Console.WriteLine($"Assistant AI: >> \n{response}");
// Console.WriteLine();
// Console.ReadLine();
#endregion

#region  Summarization
// var prompt = """
// Summarize the following blog in 1 concise sentences:

// "Microservices architecture is increasingly popular for building complex applications, but it comes with additional overhead. It's crucial to ensure each service is as small and focused as possible, and that the team invests in robust CI/CD pipelines to manage deployments and updates. Proper monitoring is also essential to maintain reliability as the system grows."
// """;

// Console.WriteLine($"user >>> {prompt} \n");

// ChatResponse summaryResponse = await client.GetResponseAsync(prompt);

// Console.WriteLine($"assistant >>> \n{summaryResponse}");
// Console.ReadKey();
#endregion

#region  Sentiment Analysis
var analysisPrompt = """
    You will analyze the sentiment of the following product reviews. 
    Each line is its own review. Output the sentiment of each review in a bulleted list and then provide a generate sentiment of all reviews.

    - I bought this product and it's amazing. I love it!
    - This product is terrible. I hate it.
    - I'm not sure about this product. It's okay.
    - I found this product based on the other reviews. It worked for a bit, and then it didn't.
    """;

Console.WriteLine($"user >>> {analysisPrompt}\n");

ChatResponse responseAnalysis = await client.GetResponseAsync(analysisPrompt);

Console.WriteLine($"assistant >>> \n{responseAnalysis}");
Console.WriteLine();
Console.ReadKey();
#endregion
