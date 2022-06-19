using ConsoleClient;

var client = new ConsoleClient.ConsoleClient();

await client.GetName("Test Username");
Console.ReadLine();