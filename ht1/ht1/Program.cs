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

            //Menu
            string opcion = "";
            while (true) {
                Console.Clear();
                Console.WriteLine("Seleccione una opcion:");
                Console.WriteLine("1. Listar contactos");
                Console.WriteLine("2. Ingresar nuevo contacto");
                Console.WriteLine("3. Salir");
                opcion = Console.ReadLine();
                if (opcion.Equals("1")){
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

        public void getContacto() {
            //Instanciar web service
            webservice.administratorcontact100Client WS = new webservice.administratorcontact100Client();

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

        public void setContacto() {
            //Instanciar web service
            webservice.administratorcontact100Client WS = new webservice.administratorcontact100Client();
            //Escribir un contacto
            string nombre = "";
            Console.WriteLine("Ingrese un contacto: ");
            nombre = Console.ReadLine();
            Console.WriteLine("Nombre de contacto: " + nombre);
            //WS.create(nombre, 0, null, 0);
        }
    }
}
