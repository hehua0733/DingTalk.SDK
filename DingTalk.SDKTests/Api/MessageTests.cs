using Microsoft.VisualStudio.TestTools.UnitTesting;
using DingTalk.SDK.DD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DingTalk.SDK.Entities;
using DingTalk.SDK.Exceptions;
using DingTalk.SDK.Extension;
namespace DingTalk.SDK.DD.Tests
{
    [TestClass()]
    public class MessageTests
    {
        [TestMethod()]
        public void send_to_conversationTest()
        {
            try
            {
                var msg = Message.send_to_conversation("manager4559", "01436135118217", "5944441",

                  new DDTextMessage("Hello world")

                  );

                Assert.IsNotNull(msg);
                Console.WriteLine(msg.receiver);
            }
            catch (ErrorJsonResultException e)
            {
                Console.WriteLine(e.Result.errmsg);
                Console.WriteLine(e.Result.errcode);
                // Assert.Fail();
            }


        }

        [TestMethod()]
        public void sendTest()
        {
            try
            {
                //var msg = Message.send("manager4559", "", "5944441", new DDTextMessage("Hello world"));
                //Assert.IsNotNull(msg);
                //Console.WriteLine(msg.receiver);

                var msg2 = Message.send("manager4559", "", "5944441", new
                    DDOAMessage()
                {
                    oa = new DDOAMessage.OA
                    {
                        message_url = "http://www.baidu.com",
                        head = new DDOAMessage.OA.Head
                        {
                            bgcolor = "55FF00FF",
                            text = "HI"
                        },
                        body = new DDOAMessage.OA.Body
                        {
                            content = "内容内容内容内容",
                            title = "标题标题标题标题",
                            file_count = "10"
                        }
                    }
                });

                Assert.IsNotNull(msg2);
                Console.WriteLine(msg2.receiver);
            }
            catch (ErrorJsonResultException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine((int)e.Result.errcode);
                Assert.Fail();
            }
        }
    }
}