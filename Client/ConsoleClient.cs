using System.Net;

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
        var responseBody = await _httpClient.GetStringAsync($"{AppSetttings.ListenerUrl}/{AppSetttings.MyNameUrl}/{name}");
        Console.WriteLine(responseBody);
    }

    public async Task GetStatus(string method)
    {
        var response = await _httpClient.GetAsync($"{AppSetttings.ListenerUrl}/{method}");
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response.StatusCode + ": " + responseString);
    }

    public async Task MyNameByHeader(string name)
    {
        _httpClient.DefaultRequestHeaders.Add(AppSetttings.NameHeader, name);
        var response = await _httpClient.GetAsync($"{AppSetttings.ListenerUrl}/{AppSetttings.MyNameByHeaderUrl}");
        _httpClient.DefaultRequestHeaders.Remove("X-MyName");
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(response.StatusCode + ": Name from header - " + responseString);
    }

    public async Task MyNameByCookies(string name)
    {
        var cookieContainer = new CookieContainer();
        using (var handler = new HttpClientHandler { CookieContainer = cookieContainer })
        {
            using (var client = new HttpClient(handler) { BaseAddress = new Uri(AppSetttings.ListenerUrl) })
            {
                cookieContainer.Add(new Uri(AppSetttings.ListenerUrl), new Cookie("MyName", name));
                var response = await client.GetAsync($"{AppSetttings.ListenerUrl}/{AppSetttings.MyNameByCookiesUrl}");
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response.StatusCode + ": " + responseString);
            }
        }
    }
}