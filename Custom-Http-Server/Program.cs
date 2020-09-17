using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Custom_Http_Server
{
    class Program
    {
        private const string NEW_ROW = "\n\r";

        static void Main(string[] args)
        {
            //Browser address: 127.0.0.1:1234 OR localhost:1234

            string htmlFile = File.ReadAllText(@"..\..\..\test.html", Encoding.UTF8);

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
                    byte[] buffer = new byte[10000];

                    int countOfReadedBytes = stream.Read(buffer, 0, buffer.Length);

                    string request = Encoding.UTF8.GetString(buffer, 0, countOfReadedBytes);

                    Console.WriteLine("Http Request is: " + NEW_ROW);
                    Console.WriteLine(request);
                    Console.WriteLine(new string('-', 70));

                    string response;

                    if (request.Contains(".png"))
                    {
                        response = $"HTTP/1.1 200 OK" + NEW_ROW +
                        $"Content-Type: image/png" + NEW_ROW +
                        $"Accept-Ranges: bytes" + NEW_ROW +
                        $"X-Frame-Options: SAMEORIGIN" + NEW_ROW +
                        $"Last-Modified: Fri, 09 Mar 2018 12:11:54 GMT" + NEW_ROW +
                        $"Date: Thu, 17 Sep 2020 13:32:02 GMT" + NEW_ROW +
                        $"Content-Length: 5263";
                    }
                    else if (request.Contains(".jpg"))
                    {
                        response = $"HTTP/1.1 200 OK" + NEW_ROW +
                       $"Content-Type: image/jpeg" + NEW_ROW +
                       $"Accept-Ranges: bytes" + NEW_ROW +
                       $"X-Frame-Options: SAMEORIGIN" + NEW_ROW +
                       $"Last-Modified: Fri, 09 Mar 2018 12:11:54 GMT" + NEW_ROW +
                       $"Date: Thu, 17 Sep 2020 13:32:02 GMT" + NEW_ROW +
                       $"Content-Length: 14616";
                    }
                    else
                    {
                        response = $"HTTP/1.1 200 OK" + NEW_ROW +
                       $"Content-type: text/html; charset=utf-8 {NEW_ROW}" +
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
