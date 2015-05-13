using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace F_Club
{
    class StregsystemCommandParser //Klassen bruges til at udføre tage en kommando og udføre den passende logik
    {
        Stregsystem system;
        IStregsystemUI ui;
        Dictionary<string, Action<List<String>>> commands;

        public StregsystemCommandParser(Stregsystem s, IStregsystemUI u)
        {
            system = s;
            ui = u;
            //Vores dictionary indeholdende vores admincommands
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
        //Denne metode er humlen bag klassen - heri "oversættes" bruger input og der kaldes de passende metoder
        public void ParseCommand(string command)
        {
            string pattern = @"^:\w+";
            List<string> inputvariables = command.Split(' ').ToList();
            //Efter at have splittet vores command over i en liste af strings separeret af mellemrum, kontrollerer vi via Regex om udtrykket er af typen admincommand, ved at spørge om det første element starter med et :
            if (Regex.IsMatch(inputvariables[0], pattern))
            {
                try
                {
                    //vi gemmer det første element som en streng, og fjerner den fra listen af strenge, så vi kan give adskille kommando fra inputparametre. Derefter kalder  vi vores dictionary med vores command string som keyword og giver den vores liste som inputparameter
                string theOneAndOnlyCommand = inputvariables[0];
                inputvariables.Remove(theOneAndOnlyCommand); // Separation of the command from the parameters so we can pass the list as parameters without fear
                commands[theOneAndOnlyCommand](inputvariables);
                }
                catch (Exception)
                {
                    ui.DisplayAdminCommandNotFoundMessage();
                }

            }
            else
            {
                try
                {
                    //Vi laver en switchcase baseret på længden af vores inputvariables liste - For på den måde at regne ud hvilken metode der bruges. 1 parameter = kun brugeren, 2 parameter er brugeren+produkt ID, og 3 er vores multibuy
                    switch (inputvariables.Count)
                    {
                            //Case 1 er hvis der kun er indtastet en bruger, som så udskriver brugerinfo samt seneste 10 transaktioner
                        case 1:
                            User u = system.GetUser(inputvariables[0]);
                            if (u != null)
                            {
                                ui.DisplayUserInfo(u.UserName);
                                //Udfører logikken til at vise de seneste 10 køb(BuyTransactions)
                                List<Transaction> userLatestTransactions = new List<Transaction>(system.GetTransactionList(u))
                                    .OrderByDescending(t => t.Date)
                                    .Where(t => t is BuyTransaction)
                                    .Take(10)
                                    .ToList();

                                ui.DisplayUserTransactions(userLatestTransactions);
                            }
                            else
                                ui.DisplayUserNotFound(inputvariables[0]);
                            break;
                            //Case 2 er vores almindelige buy case, dvs brugernavn efterfulgt af et produktID. Hvis begge eksisterer samt produktet er aktivt og brugeren har nok penge på kontoen udføres et køb
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
                            //Vores multibuy - brugernavn efterfulgt af et antal efterfulgt af et produktID.
                        case 3:
                            //Da variablerne skifter placering i vores liste alt efter hvilken case vi er i, er vi nødt til at navngive produkterne inde i casen. Derfor gives det nye variabelnavn p2 og u2 for at adskille dem fra case 2
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
                //Vi prøver at fange de exceptions der nu måtte fremkomme, og udskriver en passende fejlmedddelse
                catch (InsufficientCreditsException)
                {
                    ui.DisplayInsufficientCash();
                }
                catch (ArgumentException e)
                {
                    ui.DisplayGeneralError(e.Message);
                }
                catch (ProductNotActiveException)
                {
                    ui.DisplayProductNotActive();
                }
            }
        }
        //En hjælpemetode til at udføre logikken bag :activate og :deactivate, tager udover List<String> som inputparameter også en bool'ean. På denne måde kan vi bruge samme metode til både activate og deactivate
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
        //En hjælpemetode til at udføre logikken bag AddCredits, der tager et input i form af en List<String>. 
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
        //En hjælpemetode til at udføre logikken bag :crediton og :creditoff, tager udover List<String> som inputparameter også en bool'ean. På denne måde kan vi bruge samme metode til både activate og deactivate
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
