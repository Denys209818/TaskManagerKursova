using System;
using System.Collections.Generic;
using System.Text;

namespace App.Lib.Services
{
    public static class FreePort
    {
        private static Random random = new Random();
        private static List<int> ports = new List<int>() { 1245 };
        public static int GetFreePort() 
        {
            

            int a = 0;
            do
            {
                a = random.Next(1000, 9999);
            }
            while (ports.Contains(a));


            return a;
        }
    }
}
