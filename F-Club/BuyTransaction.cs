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
            return "Buy Transaction: " + Amount.ToString() + this.transactionUser.ToString() + Date.ToShortDateString() + TransactionID.ToString();
        }
        public void Execute()
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
