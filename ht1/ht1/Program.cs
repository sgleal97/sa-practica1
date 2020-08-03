using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ht1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instanciar web service
            webservice.administratorcontact100Client WS = new webservice.administratorcontact100Client();

            //Leer contactos
            var Contactos = WS.readList(0,0,null,null,null,null,null);
            int i = 0;
            Console.WriteLine("Tamanio:" + Contactos.Length);
            while (i<Contactos.Length) {
                Console.WriteLine("Contacto: " + Contactos[i].name);
                i++;
            }
            Console.WriteLine("\n");

            //Escribir un contacto
            string nombre = "";
            Console.WriteLine("Ingrese un contacto: ");
            nombre = Console.ReadLine();
            Console.WriteLine("Nombre de contacto: " + nombre);
            WS.create(nombre, 0, null, 0);
            Console.ReadKey();
        }
    }
}
