using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace web
{
    
    class Program
    {

        //static int port = 8005;
        //static Socket handler;
        
        static void Main(string[] args)
        {
            
            Server server = new Server();
            server.start();
  
        }
    }
}