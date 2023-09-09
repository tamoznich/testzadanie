using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server_client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region TCP
            //const string ip = "127.0.0.1"; //выбрал айпи, чтобы реализовывалось на одном компе
            //const int port = 8080;

            //var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);//сделал переменную класса эндпоинт,котрый показывает куда смогут подключаться, сделал точку 

            //var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//набор параметров для тсп протокола
            //tcpSocket.Bind(tcpEndPoint); //сказал сокету что ему слушать - режим ожидания
            //tcpSocket.Listen(2); //сколько клиентов может в очередь залезть

            //while (true) //пишу процесс бесконечного прослушивания
            //{
            //    var listener = tcpSocket.Accept(); //для каждого клиента будет свой listener, который стирается, когда клиент получил всю запрашиваемую инфу
            //    var buffer = new byte[256]; // байтовый массив, буфер
            //    var size = 0; //сколько реально байт в сообщении получили
            //    var data = new StringBuilder(); // будет собирать полученные данные

            //    do //цикл с пост условием
            //    {
            //        size = listener.Receive(buffer);
            //        data.Append(Encoding.UTF8.GetString(buffer, 0,size));//собственно, раскодирую полученные байты в куски сообщений. Использую кодировку UTF8
            //    }
            //    while (listener.Available > 0); //считывает, пока есть данные

            //    Console.WriteLine(data);//вывожу на консоль

            //    listener.Send(Encoding.UTF8.GetBytes("Все верно")); //кодируем и отправляем ответ listener

            //    listener.Shutdown(SocketShutdown.Both); //выключил соединение и у клиента и у сервера
            //    listener.Close(); //закрыл соединение(listener)
            #endregion

            const string ip = "127.0.0.1"; //выбрал айпи, чтобы реализовывалось на одном компе
            const int port = 8081;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);//сделал переменную класса эндпоинт,котрый показывает куда смогут подключаться, сделал точку 

            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//набор параметров для udp протокола
            udpSocket.Bind(udpEndPoint);

            while (true)
            {
                var buffer = new byte[256]; // байтовый массив, буфер
                var size = 0; //сколько реально байт в сообщении получили
                var data = new StringBuilder(); // будет собирать полученные данные
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);// создал экзэмпляр адреса эндпоинт, в который будет записан адрес клиента, который мы будем прослушивать

                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);// ы предыдущей строке пришлось делаит ьприведение класса, потому что recieve только такое принимает
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);
                
                udpSocket.SendTo(Encoding.UTF8.GetBytes("Все получено"), senderEndPoint); //кодируем и отправляем ответ 

                Console.WriteLine(data);
            }


        }
    }
    }

