using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    public abstract class Transaction
    {
        private static int numberOfTransactions = 0;
        private int _transactionID;
        private User _user;
        private DateTime _date;
        private int _amount;
        public int Amount { get { return _amount; } set {_amount = value ;} }
        public User transactionUser { get { return _user; } set { if (value != null) _user = value; else throw new ArgumentException("User invalid"); } }
        public int TransactionID { get { return _transactionID; } set { numberOfTransactions++; _transactionID = numberOfTransactions; } }
        public DateTime Date { get { return _date; } set { _date = value; } }

        public override string ToString()
        {
            return  _transactionID.ToString() + (_amount/100).ToString() + _date.ToString();
        }
    }
}
