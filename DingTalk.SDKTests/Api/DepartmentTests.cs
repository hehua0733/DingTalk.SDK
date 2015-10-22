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
    public class DepartmentTests
    {

        [TestMethod()]
        public void createTest()
        {
            var id = DD.Department.create("测试部门" + DateTime.Now.Ticks.ToString("x"), 1);
            Assert.IsTrue(id > 0);
            Console.WriteLine(id);

            var a = DD.Department.list();
            Assert.IsNotNull(a.Count > 0);
            var dept = a.SingleOrDefault(x => x.id == id);

            //a.Where(x => x.name.Contains("测试部门")).ToList().ForEach(x =>

            //  {

            //      var del = DD.Department.delete(x.id);
            //      Assert.IsTrue(del);
            //  }

            //);
            Assert.IsNotNull(dept);

            dept.name = "修改后的测试部门";
            var isupdate = DD.Department.update(dept);

            a = DD.Department.list();
            dept = a.SingleOrDefault(x => x.id == id);

            Assert.IsNotNull(dept);
            Assert.AreEqual(dept.name, "修改后的测试部门");

            var isdeleted = DD.Department.delete(id);
            Assert.IsTrue(isdeleted);

        }
 

    }
}