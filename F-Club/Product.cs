using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Club
{
    class Product //Produktklassen der indeholder private variabler og properties inklusiv en ToString vi har formateret for at få det udskrevet pænt når vi lister vores produkter 
    {
        private int _productID;
        private string _productName;
        private int _price; //Igen regner vi ører!!
        private bool _active;
        private bool _canBeBoughtOnCredit = false;

        public override string ToString()
        {
            return String.Format("Product ID: {0,-5} | {1,-40}  | Pris {2,-5}kr",_productID,_productName,_price/100 );
        }
        public int Price{get{return _price;} set{_price = value;} }
        public int ProductID { get{return _productID;} set{if(value < 1) throw new ArgumentException("ProductID cant be less than 1");else _productID = value; }}
        public string ProductName { get { return _productName; } set { if (value != null) _productName = value; else throw new ArgumentException("Product must have a name"); } }
        public bool Active { get { return _active; } set { _active = value; } }
        public bool CanBeBoughtOnCredit { get { return _canBeBoughtOnCredit; } set { _canBeBoughtOnCredit = value; } }
    }
}
