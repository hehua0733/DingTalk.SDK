using Microsoft.VisualStudio.TestTools.UnitTesting;
using DingTalk.SDK.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.Utilitys.Tests
{
    [TestClass()]
    public class LogUtilityTests
    {
        [TestMethod()]
        public void DebugTest()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            LogUtility.Debug("FFUFUFUFUFU");
        }
    }
}