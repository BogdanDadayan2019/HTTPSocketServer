using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace web
{

    class Server

    {
        static int port = 9300;
        
        public void start()
        {
           
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);



            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);


                // начинаем прослушивание
                listenSocket.Listen(10);

            Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();

                    Thread clientThread = new Thread(new ParameterizedThreadStart(ClientThreadFunc));

                    clientThread.Start(handler);
                }
            }
            catch
            {

            }
        }
        private void ClientThreadFunc(object handler) // сюда код
        {
            Socket socket = (Socket)handler;
            while (true)
            { 
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байтов
                byte[] data = new byte[256]; // буфер для получаемых данных


                do // парсинг и ответ клиенту html 
                {
                    bytes = socket.Receive(data);
                    builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    Console.WriteLine(builder);
                    socket.Send(Encoding.UTF8.GetBytes("hello"));
                }
                while (socket.Available > 0);
            }

        }
    }
}

