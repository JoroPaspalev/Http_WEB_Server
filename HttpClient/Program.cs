using System;
using System.Net;
using System.Net.Http;
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
            string url = "https://www.abv.bg";

            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.GetAsync(url);

            string data = "key=value";

            HttpContent httpContent = new StringContent(data);

            HttpResponseMessage responseMessage = await httpClient.PostAsync(url, httpContent);




            ;

        }






    }
}
