using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    class BuyTransaction : Transaction
    {
        private Product _product;

        public Product Product { get { return _product; } set { _product = value; } }

        public int DepositAmount { get { return this.Amount; } set { Amount = -(_product.Price); } }
        public override string ToString()
        {
            return "Buy Transaction: " + (Amount/100).ToString() + " kr" + " " +this.transactionUser.UserName+ " " + Date.ToShortDateString() + "Product: " + this.Product.ProductName;
        }
        public override void Execute()
        {
            if ((transactionUser.Balance - Amount) < 0)
            {
                throw new ArgumentException("Insufficient funds");
            }
            else
                transactionUser.Balance -= Amount;
        }
    }
}
