using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    public abstract class Transaction //Superklasse til vores transaktioner - den er abstrakt fordi vi nedarver fra dem og der skal aldrig oprettes instanser af denne klasse
    {
        private static int numberOfTransactions = 1;
        private int _transactionID;
        private User _user;
        private DateTime _date;
        private int _amount;
        public int Amount { get { return _amount; } set {_amount = value ;} }
        public User transactionUser { get { return _user; } set { if (value != null) _user = value; else throw new ArgumentException("User invalid"); } }
        public int TransactionID { get { return _transactionID; } set { _transactionID = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }

        public override string ToString()
        {
            return  _transactionID.ToString() + (_amount/100).ToString() + _date.ToString();
        }
        //Vi laver en virtual Execute metode for at fortælle at vores transaction skal have en Execute metode og derfor kan vi kalde execute på den generelle klasse og den udfører logikken ift. dens respektive subklasse
        public virtual void Execute()
        {

        }
        //Hjælpemetode til at sætte transaktionsID'et når en transaktion oprettes
        public void setTransactionID()
        {
            _transactionID = numberOfTransactions;
            numberOfTransactions++;
        }
    }
}
