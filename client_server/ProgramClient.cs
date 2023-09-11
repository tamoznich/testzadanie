using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client_server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region TCP
            //const string ip = "127.0.0.1"; //выбрал айпи, чтобы реализовывалось на одном компе
            //const int port = 8080; //выбрал порт

            //var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);//сделал класс эндпоинт,котрый показывает куда смогут подключаться, сделал точку 

            //var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//набор параметров для тсп протокола

            //Console.WriteLine("Введите слова");
            //var message = Console.ReadLine(); //что мы будем отправлять

            //var data = Encoding.UTF8.GetBytes(message);//кодируем сообщение
            //tcpSocket.Connect(tcpEndPoint);// делаем подключение нашему сокету, обратаная задача слушанья в сервере
            //tcpSocket.Send(data); // отправляем наше сообщение

            //var buffer = new byte[200];
            //var size = 0;
            //var answer = new StringBuilder(); // ответ с сервера

            //do //цикл с пост условием
            //{
            //    size = tcpSocket.Receive(buffer);
            //    answer.Append(Encoding.UTF8.GetString(buffer, 0, size));//собственно, раскодирую полученные байты в куски сообщений. Использую кодировку UTF8
            //}
            //while (tcpSocket.Available > 0); //считывает, пока есть данные

            //Console.WriteLine(answer.ToString());//вывожу ответ сервера

            //tcpSocket.Shutdown(SocketShutdown.Both);//закрываем соединение двух сокетов
            //tcpSocket.Close();

            //Console.ReadLine();
            #endregion

            #region UDP
            const string ip = "127.0.0.1"; //выбрал айпи, чтобы реализовывалось на одном компе
            const int port = 8082;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);//сделал переменную класса эндпоинт,котрый показывает куда смогут подключаться, сделал точку 

            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//набор параметров для udp протокола
            udpSocket.Bind(udpEndPoint);    
            
            while (true)
            {
                Console.WriteLine("ВВедите сообщение");
                var message = Console.ReadLine();

                var serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);//сделал переменную класса эндпоинт,вручную прописал куда подключаться
                udpSocket.SendTo(Encoding.UTF8.GetBytes(message), serverEndPoint); //отправляем данные на конкретный заранее определенный порт
                
                var buffer = new byte[256]; // байтовый массив, буфер
                var size = 0; //сколько реально байт в сообщении получили
                var data = new StringBuilder(); // будет собирать полученные данные
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);// создал экзэмпляр адреса эндпоинт, в который будет записан адрес клиента, который мы будем прослушивать

                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);

                Console.WriteLine(data);
                Console.ReadLine();
                #endregion
            }
        }
    }
}
