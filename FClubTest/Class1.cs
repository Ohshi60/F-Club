using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F_Club;


namespace FClubTest
{
    using NUnit.Framework;
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void ValidatesEmail_StringEmail_True()
        {
            //arrange
            User u = new User("Benny", "Andersen", "kylle@rylle.dk", "etellerandet");
            //act
            bool actual = u.emailValidator(u.Email);
            //assert
            Assert.IsTrue(actual);
        }
    }
}