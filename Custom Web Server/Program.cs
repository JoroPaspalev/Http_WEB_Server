using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Custom_Web_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 1212);
            tcpListener.Start();

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                using (var stream = client.GetStream())
                {
                    byte[] buffer = new byte[100000];
                    int length = stream.Read(buffer, 0, buffer.Length);

                    
                    string requestString = Encoding.UTF8.GetString(buffer, 0, length);

                    Console.WriteLine(requestString);

                    Console.WriteLine(new string('-', 70));

                    string html = "<h1> Joro is the best programmer </h1>";

                    string response = "HTTP/1.1 200 OK" + "\r\n"
                        + "Content-Type: text/html" + "\r\n"
                        + "Content-Lenght:" + html.Length + "\r\n"
                        + "\r\n"
                        + html + "\r\n";

                    byte[] responseAsByteArray = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseAsByteArray);
                }
            }





        }
    }
}
