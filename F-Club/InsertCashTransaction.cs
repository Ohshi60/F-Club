using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    class InsertCashTransaction : Transaction //Subklasse til vores transaction klasse - ligesom vores anden klasse har den override på ToString og Execute
    {
        public override string ToString()
        {
            return "Deposit: " + this.Amount.ToString() + transactionUser.ToString() + Date.ToShortDateString() + TransactionID.ToString();
        }
        public override void Execute()
        {
                transactionUser.Balance += Amount;
        }   
    }
}
