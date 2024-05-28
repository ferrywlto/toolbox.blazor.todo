using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbox.Blazor.Todo.Frontend;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var config = builder.Configuration.Build();
builder.Services.AddSingleton<IConfiguration>(sp => config);
builder.Services.AddHttpClient<BackendAPIGateway>((sp, client) => 
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton(sp => {
    var backendAPIGateway = sp.GetRequiredService<BackendAPIGateway>();
    return new TodoItemStore(backendAPIGateway);
});

var app = builder.Build();
await app.RunAsync();
