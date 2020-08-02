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
            webservice.administratorcontact100Client WS = new webservice.administratorcontact100Client();
            var Contactos = WS.readList(0,0,null,null,null,null,null);
            int i = 0;
            Console.WriteLine("Tamanio:" + Contactos.Length);
            while (i<Contactos.Length) {
                Console.WriteLine("Contacto: " + Contactos[i].name);
                i++;
            }
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}
