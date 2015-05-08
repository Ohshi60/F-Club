using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    class StregsystemCommandParser
    {
        Stregsystem system;
        IStregsystemUI ui;

        public StregsystemCommandParser(Stregsystem s, IStregsystemUI u)
        {
            system = s;
            ui = u;
        }

        public void ParseCommand(string command)
        {
            string[] commands = command.Split(' ');
            User u1 = system.GetUser(commands[0]);
            Product p1 = system.GetProduct(int.Parse(commands[1]));
            if(u1 != null && p1 != null)
            {
                BuyTransaction t = system.BuyProduct(u1, p1);
                system.ExecuteTransaction(t);
                ui.DisplayUserBuysProduct(t);
            }
        }
    }
}
