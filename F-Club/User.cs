using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace F_Club
{
    public class User : IComparable
    {
        //Statisk tæller til at tælle sekventielt op hver gang en user oprettes, bruges til at sætte UserID
        private static int memberCount = 1;       
        //Brugeroplysninger
        private int _userID;
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _email;
        private int _balance; // Vi definerer balancen i ører og kan derfor arbejde med en INT
        private bool _balanceWarning = false;
        //Constructor
        public User(string fname, string lname, string email, string userName)
        {
            _balance = 0;
            _userID = memberCount;
            memberCount++;
            if(fname != null)
                _firstName = fname;
            if(lname != null)
                _lastName = lname;
            if(emailValidator(email) == true)
                _email = email;
            if (userNameValidator(userName))
                _userName = userName;
        }
        public bool userNameValidator(string userName)
        {
            string pattern = @"[a-z0-9_]+";
            return Regex.IsMatch(userName, pattern);
        }
        public bool emailValidator(string email)
        {
            string pattern1 = @"^[a-zA-Z0-9-\\._]+@[a-zA-Z0-9][A-Za-z0-9-].+[a-zA-Z0-9]+$";
            if (Regex.IsMatch(email, pattern1))
                return true;
            else
                throw new ArgumentException("Invalid email format (ex@mple.com");
        
        }
        public override string ToString()
        {
            string userinfo = _firstName + " " + _lastName + " " + _email + " Balance: " + ((double)Balance/100).ToString();
            return userinfo;
        }
        public int Balance { 
            get { 
                if(_balance <= 5000)
                {
                    _balanceWarning = true;
                }
                    return _balance;
                ; 
            }
            set{
                _balance = value;
                if(_balance >= 5000)
                {
                    _balanceWarning = false;
                }
            }
        }

        public override bool Equals(object obj)
        {
            int o = ((User)obj)._userID;
            return _userID == o;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            User otherUser = obj as User;
            if (otherUser != null)
                return this._userID.CompareTo(otherUser._userID);
            else
                throw new ArgumentException("Object is not a user, dumbass");
        }
        public string UserName { get { return _userName; } set { _userName = value; } }

        public bool BalanceWarning { get { return _balanceWarning; } }
    }
}
