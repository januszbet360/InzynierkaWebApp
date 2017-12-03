using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataDownloader
{
    public sealed class ApiHost
    {
        private static readonly ApiHost _instance = new ApiHost();

        private const string URI = "http://api.football-data.org/";

        HttpClient _host;

        public HttpClient Host
        {
            get
            {
                return _host;
            }
        }

        public static ApiHost Instance
        {
            get
            {
                return _instance;
            }
        }

        private ApiHost()
        {
            _host = new HttpClient();
            _host.BaseAddress = new Uri(URI);
            _host.DefaultRequestHeaders.Accept.Clear();
            _host.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _host.DefaultRequestHeaders.Add("x-auth-token", Constants.API_TOKEN);
            _host.DefaultRequestHeaders.Add("x-response-control", "minified");
        }
    }
}
