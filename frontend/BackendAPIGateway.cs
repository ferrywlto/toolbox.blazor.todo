using System.Net.Http.Json;

public class BackendAPIGateway
{
    private readonly HttpClient http;
    private readonly string url;

    private string ApiEndpoint(string path) => url + path;
    public BackendAPIGateway(HttpClient http, IConfiguration config)
    {
        this.http = http;

        var local_backend_url = config["local_backend_url"];
        url = string.IsNullOrEmpty(local_backend_url)
            ? "/backend/"
            : $"{local_backend_url}/";
    }

    public async Task<List<TodoItem>> GetTodoItems()
    {
        try 
        {
            return await http.GetFromJsonAsync<List<TodoItem>>(ApiEndpoint("todos")) ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }
}