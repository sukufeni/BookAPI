using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alura.ListaLeitura.App
{
    public static class HtmlHelper
    {
        public static string loadHtml(string form)
        {
            string htmlPath = $@"C:\Users\Bruno Vieira\Google Drive\programacao\C#\git\BookAPI\Alura.ListaLeitura\Alura.ListaLeitura.App\View\{form}.html";

            try
            {
                using (var arquivo = File.OpenText(htmlPath))
                {
                    return arquivo.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
    }
}
