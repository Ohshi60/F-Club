using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace F_Club
{
    class StregsystemCommandParser
    {
        Stregsystem system;
        IStregsystemUI ui;
        Dictionary<string, Action<List<String>>> commands;
        public StregsystemCommandParser(Stregsystem s, IStregsystemUI u)
        {
            system = s;
            ui = u;
            commands = new Dictionary<string, Action<List<string>>>
            {
                {":quit" , _ => ui.Close()},
                {":q" , _ => ui.Close()},
                {":activate" , ID => activationHelper(ID, true)},
                {":deactivate" , ID => activationHelper(ID, false)},
                {":crediton" , ID => creditHelper(ID, true)},
                {":creditoff" , ID => creditHelper(ID, false)},
                {":addcredits", DepositHelper},
            };
        }

        public void ParseCommand(string command)
        {
            string pattern = @"^:\w+";
            List<string> inputvariables = command.Split(' ').ToList();
            if (Regex.IsMatch(inputvariables[0], pattern))
            {
                string theOneAndOnlyCommand = inputvariables[0];
                inputvariables.Remove(theOneAndOnlyCommand); // Seperation of the command from the parameters so we can pass the list as parameters without fear
                if (commands[theOneAndOnlyCommand] != null)
                    commands[theOneAndOnlyCommand](inputvariables);
                else
                    ui.DisplayAdminCommandNotFoundMessage();
            }
            else
            {
                switch (inputvariables.Count)
                {
                    case 1:
                        User u = system.GetUser(inputvariables[0]);
                        if (u != null)
                        {
                            ui.DisplayUserInfo(u.UserName);
                            List<Transaction> userLatestTransactions = new List<Transaction>(system.GetTransactionList(u));
                            ui.DisplayUserTransactions(userLatestTransactions);
                        }
                        else
                            ui.DisplayUserNotFound(inputvariables[0]);
                        break;
                    case 2:
                        User u1 = system.GetUser(inputvariables[0]);
                        Product p1 = system.GetProduct(int.Parse(inputvariables[1]));
                        if ((u1 != null && p1 != null))
                        {
                            BuyTransaction t = system.BuyProduct(u1, p1);
                            system.ExecuteTransaction(t);
                            ui.DisplayUserBuysProduct(t);
                            if (u1.BalanceWarning)
                                ui.DisplaySaldoWarning();
                        }
                        break;
                    case 3:
                        //Lortet gider ikke lade mig fucking navngive mit eget lort fuck dig
                        Product p2 = system.GetProduct(int.Parse(inputvariables[2]));
                        User u2 = system.GetUser(inputvariables[0]);
                        int numberOfthings = int.Parse(inputvariables[1]);
                        if ((u2 != null && p2 != null))
                        {
                            for(int i = 0;i < numberOfthings; i++)
                            {
                                BuyTransaction t = system.BuyProduct(u2, p2);
                                system.ExecuteTransaction(t);
 
                            }
                            if (u2.BalanceWarning)
                                ui.DisplaySaldoWarning();
                        }
                        break;
                    default :
                        ui.DisplayTooManyArgumentsError();
                        break;

                }
            }
        }

        private void activationHelper(List<String> inputs, bool active)
        {
            try
            {
                int ID = int.Parse(inputs[0]);
                Product p = system.GetProduct(ID);
                p.Active = active;
            }
            catch (Exception)
            {
                ui.DisplayGeneralError("You screwed up - partner");
            }
        }
        private void DepositHelper(List<String> inputs)
        {
            try
            {
                User u = system.GetUser(inputs[0]);
                int amount = (int.Parse(inputs[1])) * 100;
                system.AddCreditsToAccount(u, amount);
                                        #warning Eventuel tilføj en ui kommando der fortæller brugeren om transaktionen
            }
            catch (Exception)
            {
                ui.DisplayGeneralError("Either the user was not found or the amount specified wasnt a valid number - try again fucker");
            }
        }

        private void creditHelper(List<String> inputs, bool active)
        {
            try
            {
                int ID = int.Parse(inputs[0]);
                Product p = system.GetProduct(ID);
                p.CanBeBoughtOnCredit = active;
            }
            catch (Exception)
            {
                ui.DisplayGeneralError("Something went wrong with the activation of the product - Are you sure you got the right productID?");
            }

        }
    }
}
