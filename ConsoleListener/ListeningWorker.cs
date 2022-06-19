using System.Net;
using System.Web;

namespace ConsoleListener
{
    public class ListeningWorker
    {
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
                var responseString = GetResponse(url, request, response);

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

        private static string GetResponse(string[] url, HttpListenerRequest request, HttpListenerResponse response)
        {
            var method = url[0];

            if (method.Equals(AppSetttings.MyNameUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                if (url.Length < 2)
                {
                    response.StatusCode = 404;
                    return "Name is empty.";
                }

                var name = HttpUtility.UrlDecode(url[1]);
                response.StatusCode = 200;
                return name;
            }
            if (method.Equals(AppSetttings.InformationUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                response.StatusCode = 100;
                return "Info";
            }
            if (method.Equals(AppSetttings.SuccessUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                response.StatusCode = 200;
                return "Success";
            }
            if (method.Equals(AppSetttings.RedirectionUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                response.StatusCode = 300;
                return "Redirect";
            }
            if (method.Equals(AppSetttings.ClientErrorUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                response.StatusCode = 400;
                return "Error on client";
            }
            if (method.Equals(AppSetttings.ServerErrorUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                response.StatusCode = 500;
                return "Error on server";
            }
            if (method.Equals(AppSetttings.MyNameByHeaderUrl))
            {
                var nameFromHeader = request.Headers.Get(AppSetttings.NameHeader);
                if (nameFromHeader is not null)
                {
                    response.StatusCode = 200;
                    return nameFromHeader;
                }
            }

            response.StatusCode = 404;
            return "Not found";
        }
    }
}
