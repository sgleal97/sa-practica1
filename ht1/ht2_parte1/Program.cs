using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ht2_parte1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creacion de token
            string url = "https://api.softwareavanzado.world/index.php?option=token&api=oauth2";
            string result = GetPost(url);
            Root objeto = JsonConvert.DeserializeObject<Root>(result);

            //Menu
            string opcion = "";
            while (false)
            {
                Console.Clear();
                Console.WriteLine("Seleccione una opcion:");
                Console.WriteLine("1. Listar contactos");
                Console.WriteLine("2. Ingresar nuevo contacto");
                Console.WriteLine("3. Salir");
                opcion = Console.ReadLine();
                if (opcion.Equals("1"))
                {
                    Program read = new Program();
                    read.getContacto();
                }
                else if (opcion.Equals("2"))
                {
                    Program write = new Program();
                    write.setContacto();
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

            return result;
        }

        public void getContacto()
        {
            //Instanciar web service
            ServiceReference1.administratorcontact100Client WS = new ServiceReference1.administratorcontact100Client();

            //Leer contactos
            var Contactos = WS.readList(0, 0, null, null, null, null, null);
            int i = 0;
            Console.WriteLine("Tamanio:" + Contactos.Length);
            while (i < Contactos.Length)
            {
                Console.WriteLine("Contacto: " + Contactos[i].name);
                i++;
            }
            Console.WriteLine("\n");
        }

        public void setContacto()
        {
            //Instanciar web service
            ServiceReference1.administratorcontact100Client WS = new ServiceReference1.administratorcontact100Client();
            //Escribir un contacto
            string nombre = "";
            Console.WriteLine("Ingrese un contacto: ");
            nombre = Console.ReadLine();
            Console.WriteLine("Nombre de contacto: " + nombre);
            //WS.create(nombre, 0, null, 0);
        }

        public class getToken
        {
            public string grant_type { get; set; }
            public string client_id { get; set; }
            public string client_secret { get; set; }
        }

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

        public class Root
        {
            public string access_token { get; set; }
            public string expires_in { get; set; }
            public string token_type { get; set; }
            public string scope { get; set; }
            public string expireTimeFormatted { get; set; }
            public string created { get; set; }
            public Profile profile { get; set; }
        }

    }
}
