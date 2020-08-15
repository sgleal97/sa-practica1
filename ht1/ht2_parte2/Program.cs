using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace ht2_parte2
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://api.softwareavanzado.world/index.php?webserviceClient=administrator&webserviceVersion=1.0.0&option=contact&api=soap&wsdl");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Basic c2E6dXNhYw==");
            request.AddHeader("Cookie", "__cfduid=d86e5da17dc8ca04a411b9ba10a569cf21596342219; 1bb11e6f2dacb1c375d150942d6da0cd=88h3ms5mt8bi47ff61m3lu7eri");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            Console.ReadKey();
    }
}
