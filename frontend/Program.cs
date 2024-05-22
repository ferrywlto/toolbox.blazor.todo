using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;
using Toolbox.Blazor.Todo;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();

var http = app.Services.GetRequiredService<HttpClient>();
try {

    var config = await http.GetFromJsonAsync<AppConfiguration>("config.js");
    Console.WriteLine($"backend url: {config!.BackendUrl}");
    AppState.Configuration.BackendUrl = config!.BackendUrl;
} catch (Exception e)
{
    Console.WriteLine(e);
}

// AppState.Configuration.BackendUrl = config!.BackendUrl;
 
await app.RunAsync();

namespace Toolbox.Blazor.Todo 
{

    public static class AppState
    {
        public static AppConfiguration Configuration { get; set; } = new();
    }

    public class AppConfiguration
    {
        public string BackendUrl { get; set; } = string.Empty;
    }
    // [JsonSerializable(typeof(TodoItem[]))]
    // internal partial class AppJsonSerializerContext : JsonSerializerContext
    // {

    // }
}



