using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using RestSharp;

namespace ht2_parte1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creacion de token
            string url = "https://api.softwareavanzado.world/index.php?option=token&api=oauth2";
            string result = GetPost(url);
            RootToken objeto = JsonConvert.DeserializeObject<RootToken>(result);
            string token = "Bearer " + objeto.access_token;
            //Menu
            string opcion = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Seleccione una opcion:");
                Console.WriteLine("1. Listar contactos");
                Console.WriteLine("2. Ingresar nuevo contacto");
                Console.WriteLine("3. Salir");
                opcion = Console.ReadLine();
                if (opcion.Equals("1"))
                {
                    string respuesta = getContacto(token);
                    if(respuesta.Equals(""))
                    {
                        Console.WriteLine("Este contacto no existe");
                    }
                    else
                    {
                        RootContacto contacto = JsonConvert.DeserializeObject<RootContacto>(respuesta);
                        foreach (var item in contacto._embedded.item)
                        {
                            Console.WriteLine(item.name + "");
                        }
                    }
                    
                }
                else if (opcion.Equals("2"))
                {
                    setContacto(token);
                }
                else if (opcion.Equals("3"))
                {
                    Console.WriteLine("Programa terminado...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("La opcion seleccionada no existe");
                }
                Console.ReadKey();
            }
        }

        public static string GetPost(string url)
        {
            //Clase getToken para serializar el JSON a enviar 
            getToken oP = new getToken() { grant_type = "client_credentials", client_id = "sa", client_secret = "fb5089840031449f1a4bf2c91c2bd2261d5b2f122bd8754ffe23be17b107b8eb103b441de3771745" };
            string result = "";
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "post";
            oRequest.ContentType = "application/json; charset=UTF-8";

            //Escribir el JSON
            using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
            {
                //string json = "{\"grant_type\": \"client_credentials\", \"client_id\": \"sa\"," +
                //    " \"client_secret\": \"fb5089840031449f1a4bf2c91c2bd2261d5b2f122bd8754ffe23be17b107b8eb103b441de3771745\"}";
                string json = JsonConvert.SerializeObject(oP);
                oSW.Write(json);
                oSW.Flush();
                oSW.Close();
            }

            WebResponse oResponse = oRequest.GetResponse();
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                result = oSR.ReadToEnd().Trim();
            }
            Console.WriteLine(result);
            Console.ReadKey();
            return result;
        }

        public static string getContacto(string token)
        {
            //Ingresar contacto
            string contacto = "";
            Console.WriteLine("Ingrese el nombre del contacto: ");
            contacto = Console.ReadLine();

            string json = "";
            var client = new RestClient("https://api.softwareavanzado.world/index.php?webserviceClient=administrator&webserviceVersion=1.0.0&option=contact&api=hal");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", token);
            request.AddHeader("Cookie", "__cfduid=d86e5da17dc8ca04a411b9ba10a569cf21596342219; 1bb11e6f2dacb1c375d150942d6da0cd=mav28gon7b7h182fcm44lcbkum");
            //request.AddParameter("application/x-www-form-urlencoded", "", ParameterType.RequestBody);
            request.AddParameter("filter[search]", contacto);
            IRestResponse response = client.Execute(request);
            json = response.Content + "";
            return json;
        }

        public static void setContacto(string token)
        {
            //Escribir un contacto
            string nombre = "";
            Console.WriteLine("Ingrese un contacto: ");
            nombre = Console.ReadLine();
            var client = new RestClient("https://api.softwareavanzado.world/index.php?webserviceClient=administrator&webserviceVersion=1.0.0&option=contact&api=hal");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", token);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Cookie", "__cfduid=d86e5da17dc8ca04a411b9ba10a569cf21596342219; 1bb11e6f2dacb1c375d150942d6da0cd=l9mqb25e1vtsb6jmro5b3cjiee");
            request.AddParameter("name", nombre);
            IRestResponse response = client.Execute(request);
            Console.WriteLine("Nombre de contacto: " + nombre);
        }

        public class getToken
        {
            public string grant_type { get; set; }
            public string client_id { get; set; }
            public string client_secret { get; set; }
        }

        //Clases para obtener token oAuth2
        public class Profile
        {
            public int id { get; set; }
            public object name { get; set; }
            public object username { get; set; }
            public object email { get; set; }
            public object registerDate { get; set; }
            public object lastVisitDate { get; set; }
            public List<object> authorisedGroups { get; set; }
        }

        public class RootToken
        {
            public string access_token { get; set; }
            public string expires_in { get; set; }
            public string token_type { get; set; }
            public string scope { get; set; }
            public string expireTimeFormatted { get; set; }
            public string created { get; set; }
            public Profile profile { get; set; }
        }

        //Clases para obtener contactos

        public class Item
        {
            //public Links2 _links { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int ordering { get; set; }
            public int access { get; set; }
            public string language { get; set; }
            public int featured { get; set; }
        }

        public class Embedded
        {
            public List<Item> item { get; set; }
        }

        public class RootContacto
        {
            //public Links _links { get; set; }
            public int page { get; set; }
            public int pageLimit { get; set; }
            public int limitstart { get; set; }
            public int totalItems { get; set; }
            public int totalPages { get; set; }
            public Embedded _embedded { get; set; }
        }


    }
}
