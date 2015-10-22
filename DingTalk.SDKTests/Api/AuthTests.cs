using Microsoft.VisualStudio.TestTools.UnitTesting;
using DingTalk.SDK.DD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.DD.Tests
{
    [TestClass()]
    public class AuthTests
    {
        [TestMethod()]
        public void GetTokenTest()
        {
            var token = Auth.GetToken();

            Auth.GetToken();
            Auth.GetToken();
            Auth.GetToken();


            Assert.IsNotNull(token);
            Assert.AreNotEqual(token,string.Empty);
            Console.WriteLine(token);
        }
    }
}