using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace F_Club
{
    class User
    {
        private int memberCount = 0;       
        private int _userID;
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _email;
        private int _balance; // Vi definerer balancen i ører og kan derfor arbejde med en INT

        public User(string fname, string lname, string email)
        {
            _balance = 0;
            memberCount++;
            _userID = memberCount;
            if(fname != null)
                _firstName = fname;
            if(lname != null)
                _lastName = lname;
            if(emailValidator(email) == true)
                _email = email;
        }

        public bool emailValidator(string email)
        {
            string pattern1 = @"^[a-zA-Z0-9-\\._]+@[a-zA-Z0-9][A-Za-z0-9-].+[a-zA-Z0-9]+$";
            if(Regex.IsMatch(email, pattern1) == true)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            string userinfo = _firstName + " " + _lastName + " " + _email;
            return userinfo;
        }
    }
}
