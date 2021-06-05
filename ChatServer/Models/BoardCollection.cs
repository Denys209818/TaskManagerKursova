using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Models
{
    public class BoardCollection : List<Socket>
    {
        public string BoardKey { get; set; }
    }
}
