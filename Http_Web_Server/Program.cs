using System;
using System.Linq;
using System.Text;

namespace URL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Uri uri = new Uri("https://www.google.com/search/assets?q=bicycle&oq=bicycle#page-4");

            Console.WriteLine(uri.AbsoluteUri); // Пълния URL адрес

            Console.WriteLine(uri.Scheme); // Scheme - Http, https, ftp, file, mailto ... 

            Console.WriteLine(uri.Authority); // Authority = (Domain name + Port) --> www.abv.bg:8080

            Console.WriteLine(uri.DnsSafeHost); // Host = Domain name OR I.P. Address - едно от двете е --> www.abv.bg OR 212.34.78.97

            Console.WriteLine(uri.Port); // Port

            Console.WriteLine(uri.PathAndQuery); //Path + Query string  на едно място

            Console.WriteLine(uri.AbsolutePath); //Path - Само пътя без Query string

            Console.WriteLine(uri.Query); //Query string 

            Console.WriteLine(uri.Fragment); //нещо което стои след # в URL-a




            //string url = "https://developer.mozilla.org/en-US/docs/Web/API/URL/URL";

            string url = "http://www.cwi.nl:80/%7Eguido/Python.html";

            string[] urlParts = url.Split("://")
                .ToArray();

            string scheme = urlParts[0];

            string[] afterScheme = urlParts[1].Split("/").ToArray();

            string netloc = afterScheme[0];

            string path = string.Empty;

            for (int i = 1; i < afterScheme.Length; i++)
            {
                path += "/" + afterScheme[i];
            }

            Console.WriteLine(string.Join("<--> ", urlParts));


            ;




        }
    }
}
