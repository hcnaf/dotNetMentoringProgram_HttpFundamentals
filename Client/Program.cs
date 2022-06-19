using ConsoleClient;

var client = new ConsoleClient.ConsoleClient();

await client.GetName("Test Username");
//await client.GetStatus(AppSetttings.InformationUrl); //why not works ??
//await client.GetStatus(AppSetttings.SuccessUrl);
//await client.GetStatus(AppSetttings.RedirectionUrl);
//await client.GetStatus(AppSetttings.ClientErrorUrl);
//await client.GetStatus(AppSetttings.ServerErrorUrl);
//await client.MyNameByHeader("Test UserName in header");
await client.MyNameByCookies("Test Username from cookies.");
Console.ReadLine();