using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerCore.Models
{
    public class BoardCollection : List<Socket>
    {
        public int Id { get; set; }
    }
}
