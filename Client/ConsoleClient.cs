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
        var responseBody = await _httpClient.GetStringAsync($"http://localhost:8888/{AppSetttings.MyNameUrl}/{name}");
        Console.WriteLine(responseBody);
    }

    public async Task GetStatus(string method)
    {
        var response = await _httpClient.GetAsync($"http://localhost:8888/{method}");
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response.StatusCode + ": " + responseString);
    }
}