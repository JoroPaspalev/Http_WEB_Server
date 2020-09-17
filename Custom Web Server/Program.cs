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
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 1212); // Това казва - аз съм TcpListener и ще слушам за входящи заявки (в случая на локалната машина - IPAddress.Loopback) на порт 1212. Всичко което дойде на този порт аз ще го взема и обработя. С този код само го инициализираме
            tcpListener.Start(); // .Start() - казва на операционната система - дай ми порт 1212 за мен, моята програма започва да работи на този порт

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

                    string html = @"<html>
< header >
    < link rel = ""stylesheet"" href = ""https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css "" />
<---- Как да накарам Browser да ми зареди Bootstrap-a??? ---->
</ header >

< body class=""container"">
<h1>Wellcome to Yalp!</h1>
<div>
<form>
  I am looking for
  <input type = ""text"" class=""form-control""> 
in Boston<button class=""btn btn-primary"">Find food</button>  
</form>
<br>

<span>this is span</span><span>this is span</span><span>this is span</span>
<div>this is div</div><div>this is div</div><div>this is div</div><div>this is div</div>
<div class=""panel panel-default"">
<div class=""panel-heading"">Recommended restaurants</div>
<div class=""panel-body"">Yummi Burger(4.5 stars) <button class=""btn btn-outline-primary"">Get Directions</button></div>
  </div>
<div><p>Top restaurants</p></div>
<ul class=""list-group"">
  <li class=""list-group-item"">Joe's Donuts</li>
  <li class=""list-group-item"">Pizza Planet</li>
</ul>
</div>

<div class=""container"">
  <div class=""row"">
    <div class=""col-xs-6 well"">text</div>
    <div class=""col-xs-6 well"">text</div>
  </div>
  <div class=""row"">
    <div class=""col-*-*"">text</div>
    <div class=""col-*-*"">text</div>
    <div class=""col-*-*"">text</div>
  </div>
  <div class=""row"">
    ...
  </div>
 </div>

</body>
</html>";

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
