using System.IO;
using System;

namespace Inventario.Models
{
    public class WriteTxt
    {
        public void GenerarTxt()
        {
             string fileName = "Prueba";
             string route = $@"C:\{fileName}.txt";
        }

        public void EscribirTxt(string text) 
        {
            string fileName = "Prueba";
            string route = $@"C:\{fileName}.txt";

            using (StreamWriter sw = new StreamWriter(route, true))
            {
                sw.WriteLine(text);
                sw.Close(); 
            }
        }

    }
}
