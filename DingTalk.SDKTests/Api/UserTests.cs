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
    public class UserTests
    {
       

        [TestMethod()]
        public void getTest()
        {

            var u = new Entities.UserInfo()
            {
                name = "测试",
                department = new int[] { 1 },
                mobile = "15111111111",
                extattr = new Entities.UserInfo.Extattr
                {
                    attrs = new Entities.UserInfo.Extattr.NameValuePair[] {
                        new Entities.UserInfo.Extattr.NameValuePair { name = "hello", value = "hi" } }
                }
            };


            var userid = DD.User.create(u);
            Assert.IsNotNull(userid);



            var user = DD.User.get(userid);
            Assert.IsNotNull(user);
            Assert.IsNotNull(user.extattr.attrs);
            Assert.IsTrue(user.extattr.attrs.Length > 0);
            Assert.AreEqual(user.extattr.attrs[0].value, "hi");
            Assert.AreEqual(user.name, "测试");



            user.email = "test@sina.com";
            var isupdated = DD.User.update(user);
            Assert.IsTrue(isupdated);

            user = DD.User.get(userid);
            Assert.AreEqual(user.email, "test@sina.com");

            var isdeleted = DD.User.delete(userid);
            Assert.IsTrue(isdeleted);



            string[] ids = new string[10];
            for (int i = 0; i < ids.Length; i++)
            {
                u = new Entities.UserInfo()
                {
                    name = "测试" + i,
                    department = new int[] { 1 },
                    mobile = "1511111112" + i,
                    extattr = new Entities.UserInfo.Extattr
                    {
                        attrs = new Entities.UserInfo.Extattr.NameValuePair[] {
                        new Entities.UserInfo.Extattr.NameValuePair { name = "hello", value = "hi" } }
                    }
                };
                userid = DD.User.create(u);
                Assert.IsNotNull(userid);
                ids[i] = userid;


            }


            isdeleted = DD.User.batchdelete(ids);
            Assert.IsTrue(isdeleted);
        }

        [TestMethod()]
        public void listTest()
        {

            var list = DD.Department.list();

            var dept = list.Single(x => x.name == "软件部");

            var i = DD.User.list(dept.id);
            Assert.IsNotNull(i);
            Assert.AreEqual(i.Length, 3);
        }
    }
}