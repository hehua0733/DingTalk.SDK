using Microsoft.VisualStudio.TestTools.UnitTesting;
using DingTalk.SDK.DD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DingTalk.SDK.Entities;
using DingTalk.SDK.Extension;
using DingTalk.SDK.Exceptions;

namespace DingTalk.SDK.DD.Tests
{
    [TestClass()]
    public class MediaTests
    {
        [TestMethod()]
        public void UploadTest()
        {
            try
            {
                var result = Media.Upload(AppDomain.CurrentDomain.BaseDirectory + "\\Newtonsoft.Json.xml");

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.created_at);
                Assert.IsNotNull(result.media_id);

                Console.WriteLine("file uploaded");
                DDFileMessage file = new DDFileMessage(result.media_id);

                foreach (string item in System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,"*.xml1"))
                {
                    System.IO.File.Delete(item);
                }


                file.file.Download(AppDomain.CurrentDomain.BaseDirectory + "\\Newtonsoft.Json.xml1");

                Assert.IsTrue(System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Newtonsoft.Json.xml1"));
            }
            catch (ErrorJsonResultException e)
            {
                Console.WriteLine(e.Result.errcode);
                Console.WriteLine(e.Result.errmsg);
                Assert.Fail();
            }
        }
    }
}