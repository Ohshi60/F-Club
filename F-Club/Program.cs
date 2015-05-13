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
            Stregsystem s = new Stregsystem();
            StregsystemCLI cli = new StregsystemCLI(s);
            StregsystemCommandParser parser = new StregsystemCommandParser(s, cli);
            cli.Start(parser);
            
        }
    }
}
