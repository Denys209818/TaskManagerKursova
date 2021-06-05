using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {
        static void Main()
        {

        }

        private static void ChatServer() 
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1245);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(endPoint);

            socket.Listen(10);

            while (true) 
            {
                Socket resSocket = socket.Accept();

                List<BoardCollection> coll = new List<BoardCollection>();

                byte[] buffer = new byte[255];
                int byteCount = 0;
                StringBuilder builder = new StringBuilder();
                while (resSocket.Available > 0) 
                {
                    byteCount = resSocket.Receive(buffer);
                    builder.Append(Encoding.UTF8.GetString(buffer));
                }
                
                Base64Converter


                if (coll.FirstOrDefault(x => x.BoardKey == "") != null) 
                {
                
                }
            }
        }
    }
}
