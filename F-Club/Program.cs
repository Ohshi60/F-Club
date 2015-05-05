using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    class Program
    {
        static void Main(string[] args)
        {
            User u1 = new User("Birthe", "Neger", "mail@hej.dk");
            User u2 = new User("HonningMelone", "StorePatværk", "store@bryster.dk");
            Console.WriteLine("User:  " + u1.ToString());
            Console.ReadKey();
        }
    }
}
