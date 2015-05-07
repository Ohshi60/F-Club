﻿using System;
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
        public void BuyProduct(User user, Product product)
        {
            BuyTransaction bt = new BuyTransaction{ 
                Amount = product.Price,
                transactionUser = user,
                Date = DateTime.Now,
            };
            bt.Execute();
            _transactions.Add(bt);
        }
        public void AddCreditsToAccount(User user, int amount)
        {
            InsertCashTransaction deposit = new InsertCashTransaction
            {
                transactionUser = user,
                Amount = amount,
                Date = DateTime.Now,
            };
            deposit.Execute();
            _transactions.Add(deposit);
        }
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
        }
        public string RemoveHTMLfromString(string s)
        {
            string pattern = "<[^>]*>";
            return Regex.Replace(s, pattern, "");
        }
   }  
}