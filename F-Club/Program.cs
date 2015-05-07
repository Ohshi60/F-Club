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
            List<User> userList = new List<User>();
            User u1 = new User("Birthe", "Neger", "mail@hej.dk", "xxxpsychoxxx");
            User u2 = new User("HonningMelone", "StorePatværk", "store@bryster.dk", "jeger_klam");
           // User u3 = new User("Lone", "Ko", "jeg@pedagog.hvadså", "hejsvin");
            userList.Add(u2);
            userList.Add(u1);
            userList.Sort();
            Console.WriteLine("User:  " + u1.ToString());
            Console.WriteLine("User:  " + u2.ToString());
            u1.Balance = 51;
            u2.Balance = 20;
            Console.WriteLine("Balance of " + u1.ToString() + ":  {0}", u1.Balance/100);
            Console.WriteLine("Balance of " + u2.ToString() + ":  {0}", u2.Balance/100);
            Stregsystem s = new Stregsystem();
            StregsystemCLI cli = new StregsystemCLI(s);
            cli.Start();
            
            Console.ReadKey();
        }
    }
}
