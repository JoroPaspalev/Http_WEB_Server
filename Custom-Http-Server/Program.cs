using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Custom_Http_Server
{
    class Program
    {
        private const string NEW_ROW = "\r\n";

        static void Main(string[] args)
        {
            //Browser address: 127.0.0.1:1212 OR localhost:1212


            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

            TcpListener tcpListener = new TcpListener(ipAddress, 1212);

            Console.WriteLine("Starting TcpListener ...");

            tcpListener.Start();

            while (true)
            {
                Console.WriteLine("TcpListener listen on:  " + tcpListener.LocalEndpoint);

                Console.WriteLine("Waiting for connection ...");

                var client = tcpListener.AcceptTcpClient();

                using (NetworkStream stream = client.GetStream())
                {
                    Console.WriteLine(client.Client.RemoteEndPoint);

                    byte[] buffer = new byte[10000];

                    int countOfReadedBytes = stream.Read(buffer, 0, buffer.Length);

                    string request = Encoding.UTF8.GetString(buffer, 0, countOfReadedBytes);

                    Console.WriteLine("Http Request is: " + NEW_ROW);
                    Console.WriteLine(request);
                    Console.WriteLine(new string('-', 70));

                    string response;
                    if (request.Contains("icon"))
                    {
                        byte[] imageArray = File.ReadAllBytes(@"..\..\..\pictures\icon.png");

                        response = $"HTTP/1.1 200 OKttt" + NEW_ROW +
                        $"Content-Type: image/png" + NEW_ROW +
                        $"Content-Length: " + imageArray.Length + NEW_ROW +
                        NEW_ROW;

                        Console.WriteLine(response);

                        byte[] responseAsBytes = Encoding.UTF8.GetBytes(response);

                        byte[] readyResponse = new byte[imageArray.Length + responseAsBytes.Length];
                        responseAsBytes.CopyTo(readyResponse, 0);
                        imageArray.CopyTo(readyResponse, responseAsBytes.Length);

                        stream.Write(readyResponse);
                        continue;

                    }
                    else if (request.Contains("deep.png"))
                    {
                        byte[] pngAsBytes = File.ReadAllBytes(@"..\..\..\pictures\deep.png");

                        response = $"HTTP/1.1 200 OK" + NEW_ROW +
                        $"Content-Type: image/png" + NEW_ROW +
                        //$"Accept-Ranges: bytes" + NEW_ROW +
                        $"Content-Length: " + pngAsBytes.Length + NEW_ROW
                        + NEW_ROW;

                        byte[] responseAsBytes = Encoding.UTF8.GetBytes(response);
                        byte[] readyResponse = new byte[pngAsBytes.Length + responseAsBytes.Length];
                        responseAsBytes.CopyTo(readyResponse, 0);
                        pngAsBytes.CopyTo(readyResponse, responseAsBytes.Length);

                        stream.Write(readyResponse);
                        continue;
                    }
                    else if (request.Contains("sunflowers.jpg"))
                    {
                        byte[] sunflowers = File.ReadAllBytes(@"..\..\..\pictures\sunflowers.jpg");

                        response = $"HTTP/1.1 200 OK" + NEW_ROW +
                       $"Content-Type: image/jpeg" + NEW_ROW +
                       $"Accept-Ranges: bytes" + NEW_ROW +
                       $"Content-Length: " + sunflowers.Length + NEW_ROW +
                       NEW_ROW;

                        byte[] responseAsBytes = Encoding.UTF8.GetBytes(response);

                        byte[] readyResponse = new byte[sunflowers.Length + responseAsBytes.Length];

                        responseAsBytes.CopyTo(readyResponse, 0);
                        sunflowers.CopyTo(readyResponse, responseAsBytes.Length);

                        stream.Write(readyResponse);
                        continue;
                    }
                    else if (request.Contains("population.html"))
                    {
                        string populationHtml = File.ReadAllText(@"..\..\..\population.html");

                        response = $"HTTP/1.1 200 OK" + NEW_ROW +
                      $"Content-type: text/html; charset=utf-8 {NEW_ROW}" +
                      $"{NEW_ROW}" +
                      $"{populationHtml}{NEW_ROW}";
                    }
                    else if (request.Contains(".pdf"))
                    {
                        byte[] pdfAsBytes = File.ReadAllBytes(@"..\..\..\pdf\advices.pdf");

                        response = $"HTTP/1.1 200 OK" + NEW_ROW +
                      $"Content-type: application/pdf {NEW_ROW}" +
                      "Set-Cookie: lang=bg; domain=127.0.0.1; path=/pdf;" + NEW_ROW +
                      //"Content-Disposition: attachment; filename=advices.pdf" +
                      "Content-Lenght: " + pdfAsBytes.Length + NEW_ROW +
                      NEW_ROW;

                        byte[] responseAsBytes = Encoding.UTF8.GetBytes(response);

                        byte[] readyResponse = new byte[responseAsBytes.Length + pdfAsBytes.Length];
                        responseAsBytes.CopyTo(readyResponse, 0);
                        pdfAsBytes.CopyTo(readyResponse, responseAsBytes.Length);

                        stream.Write(readyResponse);
                        continue;
                    }
                    else if (request.Contains("index.html"))
                    {
                        string htmlFile = File.ReadAllText(@"..\..\..\index.html", Encoding.UTF8);

                      response = $"HTTP/1.1 200 OK" + NEW_ROW +
                      $"Content-type: text/html; charset=utf-8 {NEW_ROW}" +
                      "Content-Length: " + htmlFile.Length + NEW_ROW +
                      "Set-Cookie: sid=123456789; path=/index.html" + NEW_ROW +
                      NEW_ROW +
                      $"{htmlFile}{NEW_ROW}";
                    }
                    else
                    {
                        string htmlFile = File.ReadAllText(@"..\..\..\index.html", Encoding.UTF8);

                        response = $"HTTP/1.1 200 OK" + NEW_ROW +
                       $"Content-type: text/html; charset=utf-8 {NEW_ROW}" +                      
                       //$"Set-Cookie: sid=4444444; Expires={DateTime.UtcNow.AddSeconds(50).ToString("R")}" + NEW_ROW +
                      // $"Set-Cookie: sid=5555555555555; Max-Age={60}" + NEW_ROW +
                       "Set-Cookie: lang=bg" + NEW_ROW +                       
                       "Set-Cookie: secure" + NEW_ROW +
                       $"{NEW_ROW}" +
                       $"{htmlFile}{NEW_ROW}";
                    }

                    Console.WriteLine("Http Response is: " + NEW_ROW);
                    Console.WriteLine(response);
                    Console.WriteLine(new string('+', 70) + NEW_ROW);

                    byte[] responseBuffer = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBuffer);
                }
            }
        }
    }
}
