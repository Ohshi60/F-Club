using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    interface IStregsystemUI
    {
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(string productname);
        void DisplayUserInfo(string userName);
        void DisplayTooManyArgumentsError();
        void DisplayAdminCommandNotFoundMessage();
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(int count, BuyTransaction transaction);
        void Close();
        void DisplayInsufficientCash();
        void DisplayGeneralError(string errorString);
        void DisplayActiveProducts();
        void DisplaySaldoWarning();
        void DisplayUserTransactions(List<Transaction> transactions);
    }
}
