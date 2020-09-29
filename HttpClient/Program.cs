using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ReadData();
        }

        public static async Task ReadData()
        {
            string url = "http://127.0.0.1:1212";
            string page = "/index.html";  //"/icon.png";

            HttpClient httpClient = new HttpClient();// Това ми е все една браузъра
            //httpClient.BaseAddress = new Uri(url);

            // Това е еквивалентно да напиша www.abv.bg в адрес бара на браузъра и да натисна ентер
            HttpResponseMessage response = await httpClient.GetAsync(url + page); // Защо трябва да изпращам заявката Async? Какво означава да ми върне Task<object>.
            //Защо ми е? Какво да го правя?

            HttpRequestMessage requestMessage = response.RequestMessage; // Това ми извлича от отговора на Server-а, requesta, който съм му изпратил за този отговор

            HttpResponseHeaders responseHeaders = response.Headers; // Това ми дава Header-ите на Respons-a

            HttpContent httpContent1 = response.Content;// Това ми дава Content-a на Responsa

            Task<string> taskContent = httpContent1.ReadAsStringAsync();
            string content = taskContent.Result;
            Console.WriteLine(content);

            string data = "key=value";
            HttpContent httpContent = new StringContent(data);           
            HttpResponseMessage responseMessage = await httpClient.PostAsync(url, httpContent);




            ;

        }






    }
}
