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
            const string ip = "127.0.0.1"; //выбрал айпи, чтобы реализовывалось на одном компе
            const int port = 8080; //выбрал порт

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);//сделал класс эндпоинт,котрый показывает куда смогут подключаться, сделал точку 

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//набор параметров для тсп протокола

            Console.WriteLine("Введите слова");
            var message = Console.ReadLine(); //что мы будем отправлять

            var data = Encoding.UTF8.GetBytes(message);//кодируем сообщение
            tcpSocket.Connect(tcpEndPoint);// делаем подключение нашему сокету, обратаная задача слушанья в сервере
            tcpSocket.Send(data); // отправляем наше сообщение

            var buffer = new byte[200];
            var size = 0;
            var answer = new StringBuilder(); // ответ с сервера

            do //цикл с пост условием
            {
                size = tcpSocket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));//собственно, раскодирую полученные байты в куски сообщений. Использую кодировку UTF8
            }
            while (tcpSocket.Available > 0); //считывает, пока есть данные

            Console.WriteLine(answer.ToString());//вывожу ответ сервера

            tcpSocket.Shutdown(SocketShutdown.Both);//закрываем соединение двух сокетов
            tcpSocket.Close();

            Console.ReadLine();
        }
    }
}
