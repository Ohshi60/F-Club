using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    class StregsystemCLI : IStregsystemUI
    {
        Stregsystem system;
        public void Start()
        {
            List<Product> activeProducts = new List<Product>(system.GetActiveProducts());
            foreach(Product product in activeProducts)
            {
                Console.WriteLine(product.ToString());
            }
        }

        public StregsystemCLI(Stregsystem s)
        {
            system = s;
        }
        
        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine("Username {0} not found - are you sure you spelled it right???", username);
        }

        public void DisplayProductNotFound(string productname)
        {
            Console.WriteLine("Product {0} not found - are you sure you entered the right ID?", productname);
        }

        public void DisplayUserInfo(string userName)
        {
            Console.WriteLine(system.GetUser(userName).ToString());
        }

        public void DisplayTooManyArgumentsError()
        {
            Console.WriteLine("Too many arguments - whatever da fuq that means");
        }

        public void DisplayAdminCommandNotFoundMessage()
        {
            Console.WriteLine("Admin Command not found - try again or contact the sysadmin");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction.ToString());
        }

        public void DisplayUserBuysProduct(int count)
        {
            Console.WriteLine("Et eller andet - hvem fanden ved hvad fanden du har tænkt");
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void DisplayInsufficientCash()
        {
            Console.WriteLine("Not enough funds for the purchase - Deposit more money on your account or go home you poor bastard");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine(errorString);
        }


        public void DisplayActiveProducts()
        {
            throw new NotImplementedException();
        }
    }
}
