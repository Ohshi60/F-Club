using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace F_Club
{
    class Stregsystem
    {
        private List<User> _users = new List<User>();
        private List<Product> _products = new List<Product>();
        private List<Transaction> _transactions = new List<Transaction>();


        public BuyTransaction BuyProduct(User user, Product product)
        {
            //test for om produktet er aktivt og kast en ProductNotActiveException
            BuyTransaction bt = new BuyTransaction{ 
                Product = product,
                Amount = product.Price,
                transactionUser = user,
                Date = DateTime.Now,
            };
            if (bt.Product.Active != false)
                return bt;
            else
                throw new ProductNotActiveException();
        }
        //Hjælpemetode til at udføre transaktioner, den respektive Execute metode og tilføjer transaktionen til listen af transaktioner, hvorefter den også logger vores transaktion
        public void ExecuteTransaction(Transaction t)
        {
            t.setTransactionID();
            t.Execute();
            _transactions.Add(t);
            TransactionLogger(t);
        }
        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            InsertCashTransaction deposit = new InsertCashTransaction
            {
                transactionUser = user,
                Amount = amount,
                Date = DateTime.Now,
                
            };
            ExecuteTransaction(deposit);
            return deposit;
        }
        //Metode til at hente et produkt baseret på et produktID
        public Product GetProduct(int productID)
        {
            Product found = _products.FirstOrDefault(item => item.ProductID == productID);
            if (found != null)
                return found;
            else
                throw new ArgumentException("ProductID doesnt exist ");
        }
        public User GetUser(string userName)
        {
            User found = _users.FirstOrDefault(item => item.UserName == userName);
            if (found != null)
                return found;
            else
                throw new ArgumentException("Sup bitches, der er ingen ved det navn");
        }
        public List<Transaction> GetTransactionList(User user)
        {
            List<Transaction> UserTransactions = new List<Transaction>();
            foreach(Transaction transaction in _transactions)
            {
                if (transaction.transactionUser.UserName == user.UserName)
                    UserTransactions.Add(transaction);
            }
            return UserTransactions;
        }
        public List<Product> GetActiveProducts()
        {
            List<Product> ActiveProducts = new List<Product>();
            foreach(Product product in _products)
            {
                if (product.Active == true)
                    ActiveProducts.Add(product);
            }
            return ActiveProducts;
        }
        public void LoadCatalogue()
        {
            string line = "";
            string pattern = @"^\d+;[^;]+;\d+;(0|1);";
            StreamReader reader = new StreamReader("products.csv");
            while((line = reader.ReadLine()) != null)
            {
                if(Regex.IsMatch(line,pattern))
                {
                    _products.Add(stringToProduct(line));
                }
            }
        }
        private void TransactionLogger(Transaction t)
        {
            StreamWriter writer = new StreamWriter("C:\\Users\\Yolomancer\\Desktop\\transactions.csv", true);
            writer.WriteLine(t.ToString());
            writer.Close();
        }
        public Product stringToProduct(string s)
        {

            string[] words = s.Split(';'); 
            bool b = words[3].Equals("1")? true : false;

            return new Product
            {
                ProductID = int.Parse(words[0]), 
                ProductName = RemoveHTMLfromString(words[1]), 
                Price = int.Parse(words[2]),
                Active = b,
            };
        }
        public Stregsystem()
        {
            this.LoadCatalogue();
            List<Product> activeProds = new List<Product>(GetActiveProducts());
            _users.Add(new User("Benny","Johnson","abe@kat.dk","benny1"));
            GetUser("benny1").Balance = 5000;
        }
        public string RemoveHTMLfromString(string s)
        {
            string pattern = "<[^>]*>";
            return Regex.Replace(s, pattern, "");
        }
   }  
}
