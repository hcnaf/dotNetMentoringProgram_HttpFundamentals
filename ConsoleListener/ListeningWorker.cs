using System.Net;
using System.Web;

namespace ConsoleListener
{
    public class ListeningWorker
    {
        private const string MyNameMethod = "MyName";
        public async Task Start()
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8888/");
            listener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;

                var url = ParseRequest(request);
                var responseString = GetMyName(url);

                var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;

                await using (var output = response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                }
            }
        }

        private static string[] ParseRequest(HttpListenerRequest request)
        {
            var url = request.RawUrl?.Split('/');
            if (url is null || url.Length < 2)
            {
                return new[] { string.Empty };
            }

            return url[1..];
        }

        private static string GetMyName(string[] url)
        {
            var method = url[0];

            if (method.Equals(MyNameMethod, StringComparison.InvariantCultureIgnoreCase))
            {
                var name = HttpUtility.UrlDecode(url[1]);
                return name;
            }

            return "Not found";
        }
    }
}
