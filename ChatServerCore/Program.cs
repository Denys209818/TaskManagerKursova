using App.Lib.Services;
using ChatServerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerCore
{
    class Program
    {
        static List<BoardCollection> coll = new List<BoardCollection>();
        static void Main()
        {
            Console.WriteLine("Сервер запущено!");
            ChatServer();
        }

        private static void ChatServer()
        {
            
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("91.238.103.109"), 1245);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(endPoint);

            socket.Listen(10);

            while (true)
            {
                Socket resSocket = socket.Accept();

                Task.Run(() => SocketJob(resSocket));
            }
        }

        private static void SocketJob(Socket resSocket) 
        {
            int counter = 0;
            while (true)
            {
                byte[] buffer = new byte[255];
                int byteCount = 0;
                StringBuilder builder = new StringBuilder();
                do 
                {
                    try
                    {
                        byteCount = resSocket.Receive(buffer);
                    }
                    catch { return; }
                    builder.Append(Encoding.UTF8.GetString(buffer));
                }
                while (resSocket.Available > 0);

                if (counter != 0)
                {
                    Apps.Lib.Models.TaskModel task = Base64Converter.ConvertToTask(builder.ToString());

                    var boardSockets = coll.FirstOrDefault(x => x.Id == task.BoardId).Where(x => x.Connected).ToList();
                    
                    foreach (var item in boardSockets)
                    {
                        if (item != resSocket && item.Connected)
                        {
                            item.Send(Encoding.UTF8.GetBytes(Base64Converter.ConvertToBase64(task)));
                        }
                    }
                }
                else 
                {
                    counter = 1;
                    string resStr = Encoding.UTF8.GetString(buffer);
                    int boardId = int.Parse(resStr.Trim('\0'));
                    if (coll.FirstOrDefault(x => x.Id == boardId) == null)
                    {
                        var board = new BoardCollection { Id = boardId };
                        coll.Add(board);
                    }

                    if (!coll.FirstOrDefault(x => x.Id == boardId).Contains(resSocket))
                    {
                        coll.FirstOrDefault(x => x.Id == boardId).Add(resSocket);
                    }
                }
            }  
        }
    }
}
