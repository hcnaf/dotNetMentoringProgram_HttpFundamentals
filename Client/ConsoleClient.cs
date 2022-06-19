namespace ConsoleClient;

public class ConsoleClient
{
    private readonly HttpClient _httpClient;

    public ConsoleClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task GetName(string name)
    {
        var responseBody = await _httpClient.GetStringAsync($"http://localhost:8888/MyName/{name}");
        Console.WriteLine(responseBody);
    }
}