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
            // 1.
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 1212); // Това казва - аз съм TcpListener и ще слушам за входящи заявки (в случая на локалната машина - IPAddress.Loopback) на порт 1212. Всичко което дойде на този порт аз ще го взема и обработя. С този код само го инициализираме
            
            //2.
            tcpListener.Start(); // .Start() - казва на операционната система - дай ми порт 1212 за мен, моята програма започва да работи на този порт

            while (true)
            {
                // 3.
                TcpClient client = tcpListener.AcceptTcpClient(); // Този метод .AcceptTcpClient() чака някой клиент да се обърне към него (Както Console.Readline стои и чака да се въведе нещо на конзолата) т.е. да се въведе URL в браузъра и ентер. Метода улавя информациата на този клиент и ни я връща като променлива - в случая client. Каква ми е ползата от тази информация? - Тя ми дава всички нужно за да мога да комуникирам с браузъра!!!
                
                // 4.
                using (NetworkStream stream = client.GetStream()) // Вземаме си Stream-а към този клиент. В него можем да пишем и четем
                {
                    byte[] buffer = new byte[100000];
                    int length = stream.Read(buffer, 0, buffer.Length); //Чета от стрийма Request-a и го записвам в buffer-а
                    
                    string requestString = Encoding.UTF8.GetString(buffer, 0, length);// от byte масива трябва по някакъв начин да си направя string за да мога да го чета - става с Encoding

                    Console.WriteLine(requestString);

                    Console.WriteLine(new string('-', 70));

                    string html = @"<html>
<header>
    <link rel = ""stylesheet"" href = ""https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css ""/>
</header >

<body class=""container"">
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
