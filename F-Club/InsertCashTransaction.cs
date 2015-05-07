using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    class InsertCashTransaction : Transaction //InsertCash aka deposit. Y U NO CALL IT DEPOSIT?? Im going to the bank to INSERT CASH MOTHERFUCKER >:(
    {
        public override string ToString()
        {
            return "Deposit: " + this.Amount.ToString() + transactionUser.ToString() + Date.ToShortDateString() + TransactionID.ToString();
        }
        public void Execute()
        {
                transactionUser.Balance += Amount;
        }   
    }
}
